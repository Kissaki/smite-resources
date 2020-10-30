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
            //DownloadGodIcons(basePath: "data");
            GenerateGodIconSprite(basePath: "data");
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

        private static void GenerateGodIconSprite(string basePath)
        {
            var diSource = new DirectoryInfo(Path.Combine(basePath, "godicon"));
            var files = diSource.EnumerateFiles("*.jpg");

            var colcount = 20;
            var rows = (files.Count() + colcount - 1) / colcount;
            using var bitmap = new Bitmap(width: 128 * colcount, height: 128 * rows);
            using var canvas = Graphics.FromImage(bitmap);
            canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;

            var i = 0;
            foreach (var fi in files)
            {
                using var image = Image.FromFile(fi.FullName);
                // Apparently Discordia is different
                //if (image.Width != 128 || image.Height != 128) throw new InvalidOperationException($"Unexpected god icon size difference");
                canvas.DrawImage(image, destRect: new Rectangle(x: (i % colcount) * 128, y: (i / colcount) * 128, width: 128, height: 128), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
                ++i;
            }

            canvas.Save();
            bitmap.Save(Path.Combine(basePath, "godicons.jpg"), ImageFormat.Jpeg);
        }

        private static void DownloadGodIcons(string basePath)
        {
            var gods = new DataStore().ReadGods();
            var dir = new DirectoryInfo(Path.Combine(basePath, "godicon"));
            dir.Create();
            using var c = new HttpClient();
            foreach (var god in gods.Where(x => x.GodIconUrl != null).OrderBy(x => x.GodIconUrl!.PathAndQuery))
            {
                var iconUri = god.GodIconUrl!;
                using var req = new HttpRequestMessage(HttpMethod.Get, requestUri: iconUri);

                var filename = Path.GetFileName(iconUri.AbsoluteUri);
                var fi = new FileInfo(Path.Combine(dir.FullName, filename));
                if (fi.Exists)
                {
                    req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("image/*"));
                    req.Headers.IfModifiedSince = fi.LastWriteTime;
                    var existingModifiedUnixTime = new DateTimeOffset(fi.LastWriteTimeUtc);
                    var etag = $@"""{existingModifiedUnixTime.ToUnixTimeSeconds()}""";
                    req.Headers.IfNoneMatch.Add(new EntityTagHeaderValue(etag, isWeak: false));
                }

                var res = c.SendAsync(req).Result;
                if (res == null)
                {
                    Console.Error.WriteLine($"WARNING: Failed to download icon {req.RequestUri}. Response object is null.");
                    continue;
                }
                if (!res.IsSuccessStatusCode)
                {
                    if (res.StatusCode == HttpStatusCode.NotModified)
                    {
                        Console.WriteLine($"{fi.Name} ✓ {res.StatusCode}");
                        continue;
                    }
                    Console.WriteLine($"{fi.Name} x {res.StatusCode}");
                    Console.Error.WriteLine($"WARNING: Failed to download icon {req.RequestUri}. {res.StatusCode} {res.Content.ReadAsStringAsync().Result}");
                    continue;
                }
                using var fs = fi.OpenWrite();
                using var s = res.Content.ReadAsStreamAsync().Result;
                s.CopyTo(fs);
                fs.Close();
                s.Close();
                fi.LastWriteTimeUtc = DateTimeOffset.FromUnixTimeSeconds(long.Parse(res.Headers.ETag.Tag.Trim('"'), CultureInfo.InvariantCulture)).UtcDateTime;
                Console.WriteLine($"{fi.Name} ✓ {res.StatusCode}");
            }
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
