using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace KCode.SMITEClient.Data
{
    internal class GodIcons
    {
        private readonly string _combinedImagePath;
        private readonly DirectoryInfo _dataDirPath;

        public GodIcons(string basePath)
        {
            _combinedImagePath = Path.Combine(basePath, "godicons.jpg");
            _dataDirPath = new DirectoryInfo(Path.Combine(basePath, "godicon"));
            _dataDirPath.Create();
        }

        public void DownloadGodIcons()
        {
            var gods = new DataStore().ReadGods();
            using var c = new HttpClient();
            foreach (var god in gods.Where(x => x.GodIconUrl != null).OrderBy(x => x.GodIconUrl!.PathAndQuery))
            {
                var iconUri = god.GodIconUrl!;
                var fileurl = Path.GetFileName(iconUri.AbsoluteUri);

                var fi = new FileInfo(Path.Combine(_dataDirPath.FullName, fileurl));
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

        public void GenerateGodIconSprite()
        {
            var files = _dataDirPath.EnumerateFiles("*.jpg").OrderBy(x => x.Name);
            using var bitmap = SpriteGenerator.GenerateForBitmaps(files, squareItemSize: 128, maxColCount: null);
            bitmap.Save(_combinedImagePath, ImageFormat.Jpeg);

            var indexPath = Path.ChangeExtension(_combinedImagePath, ".json");
            File.WriteAllText(indexPath, string.Join("\n", files.Select(x => x.Name)) + "\n");

            //ConvertSpriteTo("webp");
            ConvertSpriteTo("avif");
        }

        private void ConvertSpriteTo(string fileExtension)
        {
            SixLabors.ImageSharp.Formats.IImageEncoder encoder = fileExtension switch
            {
                "webp" => new Shorthand.ImageSharp.WebP.WebPEncoder(),
                "avif" => new Shorthand.ImageSharp.AVIF.AVIFEncoder(),
                _ => throw new ArgumentException($"Unrecognized value {fileExtension}", nameof(fileExtension)),
            };

            var srcImg = SixLabors.ImageSharp.Image.Load(_combinedImagePath);
            using var outStream = File.OpenWrite(Path.ChangeExtension(_combinedImagePath, fileExtension));
            srcImg.Save(outStream, encoder);
        }
    }
}
