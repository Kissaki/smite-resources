using KCode.SMITEAPI;
using KCode.SMITEClient.AuthConfig;
using KCode.SMITEClient.Data;
using KCode.SMITEClient.HtmlGenerating;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Net;
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

    private static async Task CheckRemoteImage()
    {
        using var c = new HttpClient();
        var invalid = new List<(Uri, string)>();

        var godUrls = GetRemoteUrls();
        var missing = godUrls.Where(x => x.url == null).Select(x => $"Missing image url value for god {x.god.Name} (id {x.god.Id})").ToImmutableArray();
        var urls = godUrls.Select(x => x.url).Where(x => x != null && x.OriginalString.Length > 0).Cast<Uri>().ToImmutableArray();

        var i = 0;
        foreach (var url in urls)
        {
            Console.WriteLine($"{i}/{urls.Length}...");
            var err = CheckUri(c, url);
            if (err != null) invalid.Add((url, err));
            await Task.Delay(50);
            ++i;
        }
        Console.WriteLine($"Remote image check concluded with {missing.Length} missing and {invalid.Count} invalid.");

        Write(missing, invalid);

        static IEnumerable<(GodJsonModel god, Uri? url)> GetRemoteUrls()
        {
            var ds = new DataStore();
            foreach (var god in ds.ReadGodsWithSkins())
            {
                yield return (god, god.GodIconUrl);
                yield return (god, god.GodCardUrl);
                yield return (god, god.GodAbility1Url);
                yield return (god, god.GodAbility2Url);
                yield return (god, god.GodAbility3Url);
                yield return (god, god.GodAbility4Url);
                yield return (god, god.GodAbility5Url);
                foreach (var skin in god.Skins)
                {
                    yield return (god, skin.CardUri);
                }
            }
        }

        static string? CheckUri(HttpClient c, Uri uri)
        {
            var res = c.Send(new HttpRequestMessage(HttpMethod.Head, uri));
            Console.WriteLine($"{(res.StatusCode == HttpStatusCode.OK ? "OK" : "ERR")} {uri}");
            return res.StatusCode == HttpStatusCode.OK ? null : $"Err status code {res.StatusCode}";
        }

        static void Write(ImmutableArray<string> missing, List<(Uri, string)> invalid)
        {
            var filename = "invalid-urls.html";
            Console.WriteLine($"Writing result to {filename}");
            var missingHtml = missing.Length == 0 ? "none" : $"<ul><li>{string.Join("</li><li>", missing)}</li></ul>";
            var invalidHtml = invalid.Count == 0 ? "none" : $"<ul><li>{string.Join("</li><li>", invalid.Select(x => $"{x.Item2} {x.Item1}"))}</li></ul>";
            File.WriteAllText(filename, $"<h1>Image Check Issue Results</h1><h2>Missing</h2>{missingHtml}<h2>Invalid</h2>{invalidHtml}");
        }
    }
}
