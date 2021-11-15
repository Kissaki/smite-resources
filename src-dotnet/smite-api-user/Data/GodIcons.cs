using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace KCode.SMITEClient.Data
{
    internal class GodIcons
    {
        private readonly DirectoryInfo _dataDirPath;
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
                using var req = new HttpRequestMessage(HttpMethod.Get, requestUri: iconUri);

                var fileurl = Path.GetFileName(iconUri.AbsoluteUri);
                var fi = new FileInfo(Path.Combine(_dataDirPath.FullName, fileurl));
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
            var files = _dataDirPath.EnumerateFiles("*.jpg");

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
    }
}
