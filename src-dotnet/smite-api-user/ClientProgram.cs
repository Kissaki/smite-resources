using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using KCode.SMITEAPI;
using KCode.SMITEClient.Data;
using KCode.SMITEClient.HtmlGenerating;
using YamlDotNet.RepresentationModel;

namespace KCode.SMITEClient
{
    class Program
    {
        public static void Main()
        {
            var fi = new FileInfo(".auth.yaml");
            if (!fi.Exists) throw new InvalidOperationException("Missing auth config file");

            var yaml = new YamlStream();
            using var f = File.OpenText(fi.FullName);
            yaml.Load(f);

            var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;
            var devId = int.Parse(mapping.Children["devid"].ToString(), provider: null);
            var authKey = mapping.Children["authkey"].ToString();

            using var c = new RequestClient();
            c.Configure(devId, authKey);
            //Console.WriteLine(c.SendPingAsync().Result);
            //Console.WriteLine(c.TestSessionAsync().Result);

            DownloadGods(c, basePath: "data");
            var godIcons = new GodIcons(basePath: "data");
            //godIcons.DownloadGodIcons();
            godIcons.GenerateGodIconSprite();
            GenerateGodsHtml();

            DownloadAllGodSkinsForStoredGods(c, basePath: "data");
            GenerateGodsWithSkinsFromStore();
            GenerateGodSkinsHtml();

            GenerateGodSkinThemeHtml();

            DownloadItems(c, basePath: "data");
            GenerateItemsHtml();

            Console.WriteLine("Done. Waiting for ENTER to exit…");
            Console.ReadLine();
        }


        private static void DownloadGods(RequestClient c, string basePath) => new JsonDownloader(c, basePath).Update(filenameOrRelPath: "gods.json", c => c.GetGodsAsync());
        private static void DownloadItems(RequestClient c, string basePath) => new JsonDownloader(c, basePath).Update(filenameOrRelPath: "items.json", c => c.GetItemsAsync());
        private static void DownloadGodSkins(RequestClient c, string basePath, int godId) => new JsonDownloader(c, basePath).Update(filenameOrRelPath: $"godskins/godskins-{godId}.json", c => c.GetGodSkinsAsync(godId));

        private static void GenerateGodsHtml() => GodsHtml.GenerateGodsHtml(targetFile: "smitegods.html", new DataStore().ReadGods()!);
        private static void GenerateGodSkinsHtml() => GodsSkinsHtml.GenerateGodsHtml("god-skins.html", new DataStore().ReadGodsWithSkins()!);
        private static void GenerateItemsHtml() => ItemsHtml.Generate(targetFile: "smiteitems.html", items: new DataStore().ReadItems()!);

        private static void GenerateGodSkinThemeHtml() => GodSkinThemeHtml.Generate("god-skin-themes.html", new DataStore().ReadGodsWithSkins()!, new DataStore().ReadGodSkinThemes());

        private static void DownloadAllGodSkinsForStoredGods(RequestClient c, string basePath)
        {
            foreach (var godId in new DataStore().ReadGods().Select(x => x.Id))
            {
                DownloadGodSkins(c, basePath, godId: godId);
            }
        }

        private static void GenerateGodsWithSkinsFromStore()
        {
            var ds = new DataStore();
            var gods = ds.ReadGodsAsWithSkins();
            if (gods == null) throw new InvalidOperationException("Could not read gods with skins data");
            foreach (var god in gods)
            {
                god.Skins = ds.ReadGodSkins(god.Id);
            }
            ds.WriteGodsWithSkins(gods);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private static void DownloadPlayer(RequestClient c, int playerId, string dataDirPath)
        {
            var playerPath = Path.Combine(dataDirPath, "player", playerId.ToString(provider: null));
            var d = new JsonDownloader(c, playerPath);

            d.Update(filenameOrRelPath: "player.json", c => c.GetPlayerAsync(playerId));
            d.Update(filenameOrRelPath: "godranks.json", c => c.GetPlayerGodRanksAsync(playerId));
            d.Update(filenameOrRelPath: "friends.json", c => c.GetPlayerFriendsAsync(playerId));
            d.Update(filenameOrRelPath: "achievements.json", c => c.GetPlayerAchievementsAsync(playerId));
            d.Update(filenameOrRelPath: "status.json", c => c.GetPlayerStatusAsync(playerId));
            d.Update(filenameOrRelPath: "matchhistory.json", c => c.GetPlayerMatchHistoryAsync(playerId));
        }
    }
}
