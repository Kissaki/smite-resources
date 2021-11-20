using KCode.SMITEAPI;
using KCode.SMITEClient.AuthConfig;
using KCode.SMITEClient.Data;
using KCode.SMITEClient.HtmlGenerating;
using KCode.SMITEClient.Menus;
using System.Collections.Immutable;
using System.Net;
using System.Net.Http;

namespace KCode.SMITEClient;

internal static class Program
{
    public static void Main()
    {
        var menu = new MenuLogic();
        menu.Run();

        if (menu.SendPing || menu.TestAuth || menu.DownloadData)
        {
            using var c = new RequestClient();
            if (menu.SendPing)
            {
                Console.WriteLine(c.SendPingAsync().Result);
            }
            if (menu.TestAuth || menu.DownloadData)
            {
                var auth = AuthConfigReader.Read();
                c.Configure(auth.DevId, auth.AuthKey);
                if (menu.TestAuth)
                {
                    Console.WriteLine(c.TestSessionAsync().Result);
                }
                if (menu.DownloadData)
                {
                    var downloader = new JsonDownloader(c, "data");
                    DownloadData(downloader);
                }
            }
        }
        if (menu.GenerateData)
        {
            GenerateGodsWithSkinsFromStore();
        }
        if (menu.DownloadIcons)
        {
            var godIcons = new GodIcons(basePath: "data");
            godIcons.DownloadGodIcons();
        }
        if (menu.CheckRemoteImages)
        {
            CheckRemoteImage().Wait();
        }
        if (menu.CombineIcons)
        {
            var godIcons = new GodIcons(basePath: "data");
            godIcons.GenerateGodIconSprite();
        }
        if (menu.GenerateHtml)
        {
            GodsHtml.GenerateGodsHtml(targetFile: "smitegods.html", new DataStore().ReadGods()!);
            GodsSkinsHtml.GenerateGodsHtml("god-skins.html", new DataStore().ReadGodsWithSkins()!);

            GodSkinThemeHtml.Generate("god-skin-themes.html", new DataStore().ReadGodsWithSkins()!, new DataStore().ReadGodSkinThemes());

            ItemsHtml.Generate(targetFile: "smiteitems.html", items: new DataStore().ReadItems()!);
        }

        Console.WriteLine("Done. Waiting for ENTER to exit…");
        Console.ReadLine();
    }

    private static void DownloadData(JsonDownloader downloader)
    {

        downloader.Update("gods.json", c => c.GetGodsAsync());

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

    private static async Task CheckRemoteImage()
    {
        using var c = new HttpClient();
        var invalid = new List<(Uri, string)>();

        var godUrls = GetRemoteUrls();
        var missing = godUrls
            .Where(x => x.url == null || x.url.OriginalString.Length == 0)
            // Ignore all Diamond. Legendary, Shadow card URLs, as none are present
            .Where(x => !x.context.Contains("skin Diamond") && !x.context.Contains("skin Legendary") && !x.context.Contains("skin Shadow"))
            .Select(x => $"Missing image url value {x.context} for god {x.god.Name} (id {x.god.Id})")
            .ToImmutableArray();
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

        static IEnumerable<(GodJsonModel god, Uri? url, string context)> GetRemoteUrls()
        {
            var ds = new DataStore();
            foreach (var god in ds.ReadGodsWithSkins())
            {
                yield return (god, god.GodIconUrl, nameof(god.GodIconUrl));
                yield return (god, god.GodCardUrl, nameof(god.GodCardUrl));
                yield return (god, god.GodAbility1Url, nameof(god.GodAbility1Url));
                yield return (god, god.GodAbility2Url, nameof(god.GodAbility2Url));
                yield return (god, god.GodAbility3Url, nameof(god.GodAbility3Url));
                yield return (god, god.GodAbility4Url, nameof(god.GodAbility4Url));
                yield return (god, god.GodAbility5Url, nameof(god.GodAbility5Url));
                foreach (var skin in god.Skins)
                {
                    yield return (god, skin.CardUri, $"skin {skin.Name} {skin.Id1} {nameof(skin.CardUri)}");
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
