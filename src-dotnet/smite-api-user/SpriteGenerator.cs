using System.Drawing;
using System.Drawing.Drawing2D;

namespace KCode.SMITEClient;

internal class SpriteGenerator
{
    public static Bitmap GenerateForBitmaps(IEnumerable<FileInfo> files, int squareItemSize, int? maxColCount = null)
    {
        var filesCount = files.Count();
        if (filesCount == 0) throw new ArgumentException($"{nameof(files)} must not be empty");

        var colCount = maxColCount ?? filesCount;
        //var rowCount = (filesCount + colCount - 1) / colCount;
        var rowCount = (filesCount / colCount) + (filesCount % colCount > 0 ? 1 : 0);
        var bitmap = new Bitmap(width: squareItemSize * colCount, height: squareItemSize * rowCount);

        using var canvas = Graphics.FromImage(bitmap);
        canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;

        var i = 0;
        foreach (var fi in files)
        {
            var destRect = new Rectangle(x: (i % colCount) * squareItemSize, y: (i / colCount) * squareItemSize, width: squareItemSize, height: squareItemSize);

            using var image = Image.FromFile(fi.FullName);
            // Apparently Discordia is different (512 instead of 128)
            if (image.Width != squareItemSize || image.Height != squareItemSize)
            {
                Console.WriteLine($"WARN: Image {fi.Name} is not of square size {squareItemSize} but {image.Width}x{image.Height}");
            }
            var srcRect = new Rectangle(0, 0, image.Width, image.Height);
            canvas.DrawImage(image, destRect: destRect, srcRect, GraphicsUnit.Pixel);
            ++i;
        }

        canvas.Save();
        return bitmap;
    }
}
