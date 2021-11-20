using KCode.SMITEAPI;
using KCode.SMITEClient.AuthConfig;
using KCode.SMITEClient.Data;
using KCode.SMITEClient.HtmlGenerating;

namespace KCode.SMITEClient
{
    internal static class Program
    {
        public static void Main()
        {
            var auth = AuthConfigReader.Read();

            using var c = new RequestClient();
            c.Configure(auth.DevId, auth.AuthKey);
            var downloader = new JsonDownloader(c, "data");
            RunWith(c, downloader);

            Console.WriteLine("Done. Waiting for ENTER to exit…");
            Console.ReadLine();
        }

        private static void RunWith(RequestClient c, JsonDownloader downloader)
        {
            //Console.WriteLine(c.SendPingAsync().Result);
            //Console.WriteLine(c.TestSessionAsync().Result);

            downloader.Update(filenameOrRelPath: "gods.json", c => c.GetGodsAsync());
            var godIcons = new GodIcons(basePath: "data");
            //godIcons.DownloadGodIcons();
            godIcons.GenerateGodIconSprite();
            GodsHtml.GenerateGodsHtml(targetFile: "smitegods.html", new DataStore().ReadGods()!);

            DownloadAllGodSkinsForStoredGods(downloader);
            GenerateGodsWithSkinsFromStore();
            GodsSkinsHtml.GenerateGodsHtml("god-skins.html", new DataStore().ReadGodsWithSkins()!);

            GodSkinThemeHtml.Generate("god-skin-themes.html", new DataStore().ReadGodsWithSkins()!, new DataStore().ReadGodSkinThemes());

            downloader.Update(filenameOrRelPath: "items.json", c => c.GetItemsAsync());
            ItemsHtml.Generate(targetFile: "smiteitems.html", items: new DataStore().ReadItems()!);
        }

        private static void DownloadAllGodSkinsForStoredGods(JsonDownloader d)
        {
            foreach (var godId in new DataStore().ReadGods().Select(x => x.Id))
            {
                d.Update(filenameOrRelPath: $"godskins/godskins-{godId}.json", c => c.GetGodSkinsAsync(godId));
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
