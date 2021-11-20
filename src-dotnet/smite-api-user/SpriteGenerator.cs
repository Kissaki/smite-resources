using System.Drawing;
using System.Drawing.Drawing2D;

namespace KCode.SMITEClient;

internal static class SpriteGenerator
{
    public static Bitmap GenerateForBitmaps(IEnumerable<FileInfo> files, int squareItemSize)
    {
        var filesCount = files.Count();
        if (filesCount == 0) throw new ArgumentException($"{nameof(files)} must not be empty");

        var bitmap = new Bitmap(width: squareItemSize * filesCount, height: squareItemSize);

        using var canvas = Graphics.FromImage(bitmap);
        canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;

        var x = 0;
        foreach (var fi in files)
        {
            using var image = Image.FromFile(fi.FullName);

            if (image.Width != squareItemSize || image.Height != squareItemSize)
            {
                Console.WriteLine($"WARN: Image {fi.Name} is not of square size {squareItemSize} but {image.Width}x{image.Height} and will be resized (with interpolation)");
            }

            Draw(canvas, image, x, squareItemSize);

            x += squareItemSize;
        }

        canvas.Save();
        return bitmap;

        static void Draw(Graphics canvas, Image image, int x, int squareItemSize)
        {
            var srcRect = new Rectangle(0, 0, image.Width, image.Height);
            var destRect = new Rectangle(x: x, y: 0, width: squareItemSize, height: squareItemSize);
            canvas.DrawImage(image, destRect: destRect, srcRect, GraphicsUnit.Pixel);
        }
    }
}
