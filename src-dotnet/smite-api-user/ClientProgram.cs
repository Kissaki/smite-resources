using KCode.SMITEAPI;
using KCode.SMITEAPI.ResultTypes;
using KCode.SMITEClient.AuthConfig;
using KCode.SMITEClient.Data;
using KCode.SMITEClient.HtmlGenerating;
using System.Linq.Expressions;
using System.Net.Http;

namespace KCode.SMITEClient;

internal static class Program
{
    public static void Main()
    {
        var auth = AuthConfigReader.Read();
        DownloadData(auth);
        CheckRemoteImage().Wait();
        GenerateFiles();

        Console.WriteLine("Done. Waiting for ENTER to exit…");
        Console.ReadLine();
    }

    private static void DownloadData(Auth auth)
    {
        using var c = new RequestClient();
        c.Configure(auth.DevId, auth.AuthKey);

        //Console.WriteLine(c.SendPingAsync().Result);
        //Console.WriteLine(c.TestSessionAsync().Result);

        var downloader = new JsonDownloader(c, "data");

        downloader.Update("gods.json", c => c.GetGodsAsync());
        var godIcons = new GodIcons(basePath: "data");
        godIcons.DownloadGodIcons();

        DownloadAllGodSkinsForStoredGods(downloader);

        downloader.Update(filenameOrRelPath: "items.json", c => c.GetItemsAsync());
    }

    private static void GenerateFiles()
    {
        var godIcons = new GodIcons(basePath: "data");
        godIcons.GenerateGodIconSprite();

        GodsHtml.GenerateGodsHtml(targetFile: "smitegods.html", new DataStore().ReadGods()!);
        GenerateGodsWithSkinsFromStore();
        GodsSkinsHtml.GenerateGodsHtml("god-skins.html", new DataStore().ReadGodsWithSkins()!);

        GodSkinThemeHtml.Generate("god-skin-themes.html", new DataStore().ReadGodsWithSkins()!, new DataStore().ReadGodSkinThemes());

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

    private static async Task CheckRemoteImage()
    {
        using var c = new HttpClient();
        var missing = new List<string>();
        var invalid = new List<Uri>();
        foreach (var god in new DataStore().ReadGods())
        {
            await Check(c, god, god => god.GodIconUrl, missing, invalid);
            await Check(c, god, god => god.GodCardUrl, missing, invalid);
            await Check(c, god, god => god.GodAbility1Url, missing, invalid);
            await Check(c, god, god => god.GodAbility2Url, missing, invalid);
            await Check(c, god, god => god.GodAbility3Url, missing, invalid);
            await Check(c, god, god => god.GodAbility4Url, missing, invalid);
            await Check(c, god, god => god.GodAbility5Url, missing, invalid);
            await Task.Delay(1000);
        }

        var missingHtml = missing.Count == 0 ? "" : $"<h1>Missing</h1><ul><li>{string.Join("</li><li>", missing)}</li></ul>";
        var invalidHtml = invalid.Count == 0 ? "" : $"<h1>Invalid</h1><ul><li>{string.Join("</li><li>", invalid)}</li></ul>";
        File.WriteAllText("invalid-urls.html", $"{missingHtml}{invalidHtml}");

        static async Task Check(HttpClient c, GodResult god, Expression<Func<GodResult, Uri?>> accessor, List<string> missing, List<Uri> invalid)
        {
            var fn = accessor.Compile();
            var uri = fn.Invoke(god);
            if (uri == null)
            {
                missing.Add($"Missing {nameof(god.GodIconUrl)} for god {god.Id} {god.Name}");
                Console.WriteLine($"INFO: Image MISSING {uri.AbsolutePath}");
                return;
            }

            var err = await CheckUri(c, uri);
            if (err != null)
            {
                invalid.Add(uri);
                Console.WriteLine($"INFO: Image INVALID {uri.AbsolutePath}");
                return;
            }

            Console.WriteLine($"INFO: Image OK {uri.AbsolutePath}");
        }

        static async Task<string?> CheckUri(HttpClient c, Uri uri)
        {
            var res = await c.SendAsync(new HttpRequestMessage(HttpMethod.Head, uri));
            return res.StatusCode == System.Net.HttpStatusCode.OK ? null : $"Err status code {res.StatusCode}";
        }
    }
}
