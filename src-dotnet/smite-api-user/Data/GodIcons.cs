﻿using System.Diagnostics;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace KCode.SMITEClient.Data
{
    internal static class GodIcons
    {
        public static void DownloadGodIcons(string targetDir)
        {
            var di = new DirectoryInfo(targetDir);
            di.Create();

            var gods = new DataStore().ReadGods();
            using var c = new HttpClient();
            foreach (var god in gods.Where(x => x.GodIconUrl != null).OrderBy(x => x.GodIconUrl!.PathAndQuery))
            {
                var iconUri = god.GodIconUrl!;
                var fileurl = Path.GetFileName(iconUri.AbsoluteUri);

                var fi = new FileInfo(Path.Combine(di.FullName, fileurl));
                if (fi.Exists && fi.LastWriteTime > DateTime.Today)
                {
                    Console.WriteLine($"DEBUG: Not checking for updated god icon as the local file was already updated today {fi.Name}");
                    continue;
                }

                using var req = new HttpRequestMessage(HttpMethod.Get, requestUri: iconUri);
                if (fi.Exists)
                {
                    req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("image/*"));
                    req.Headers.IfModifiedSince = fi.LastWriteTime;

                    //var existingModifiedUnixTime = new DateTimeOffset(fi.LastWriteTimeUtc);
                    //var etag = $@"""{existingModifiedUnixTime.ToUnixTimeSeconds()}""";
                    //req.Headers.IfNoneMatch.Add(new EntityTagHeaderValue(etag, isWeak: false));
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
                //fi.LastWriteTimeUtc = DateTimeOffset.FromUnixTimeSeconds(long.Parse(res.Headers.ETag.Tag.Trim('"'), CultureInfo.InvariantCulture)).UtcDateTime;
                Console.WriteLine($"{fi.Name} ✓ {res.StatusCode}");
            }
        }

        public static void GenerateGodIconSprites(IOrderedEnumerable<FileInfo> files, string targetFilePathNoExtension)
        {
            var spriteBase = targetFilePathNoExtension;
            var squareItemSize = 128;
            Console.WriteLine($"Generating sprite for {files.Count()} files of square size {squareItemSize}...");
            using var bitmap = SpriteGenerator.GenerateForBitmaps(files, squareItemSize);
            bitmap.Save(spriteBase + ".png", ImageFormat.Png);
            bitmap.Save(spriteBase + ".jpg", ImageFormat.Jpeg);

            Console.WriteLine("Writing sprite data files...");
            File.WriteAllText(spriteBase + ".spriteorder", string.Join("\n", files.Select(x => x.Name)) + "\n");
            File.WriteAllText(spriteBase + ".json", @"{""images"":[{" + string.Join("},{", files.Select((x, i) => $@"""name"":""{x.Name}"",""x"":{i * squareItemSize}")) + "}]}");

            Console.WriteLine("Converting sprites to additional image formats...");
            var pWebp = Process.Start("ffmpeg.exe", $@"-hide_banner -y -i ""{spriteBase}.png"" ""{spriteBase}.webp""");
            pWebp.WaitForExit();
            if (pWebp.ExitCode != 0) Console.WriteLine($"ERROR: Sprite webp conversion with ffmpeg.exe failed with exit code {pWebp.ExitCode}");

            var pAvif = Process.Start("avifenc.exe", $@"""{spriteBase}.png"" ""{spriteBase}.avif""");
            pAvif.WaitForExit();
            if (pAvif.ExitCode != 0) Console.WriteLine($"ERROR: Sprite avif conversion with avifenc.exe failed with exit code {pAvif.ExitCode}");
        }
    }
}