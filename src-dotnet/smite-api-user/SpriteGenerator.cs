using System.Drawing;
using System.Drawing.Drawing2D;

namespace KCode.SMITEClient;

internal static class SpriteGenerator
{
    // There is a limit to how wide it can be, so we use 32 item columns - which seems like a reasonable value - also for humans viewing it.
    public const int LineItemCount = 32;

    /// <param name="squareItemSize">The width and height in pixels of each sprite image block</param>
    /// <exception cref="ArgumentException"></exception>
    public static Bitmap GenerateForBitmaps(IEnumerable<FileInfo> files, int squareItemSize)
    {
        var filesCount = files.Count();
        if (filesCount == 0) throw new ArgumentException($"{nameof(files)} must not be empty");

        var cols = Math.Min(filesCount, LineItemCount);
        // With int division fixup (31/32 = 0)
        var rows = filesCount / LineItemCount + (filesCount % LineItemCount == 0 ? 0 : 1);
        var width = cols * squareItemSize;
        var height = rows * squareItemSize;
        var bitmap = new Bitmap(width, height);

        using var canvas = Graphics.FromImage(bitmap);
        canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;

        int x = 0, y = 0;
        foreach (var fi in files)
        {
            using var image = Image.FromFile(fi.FullName);

            if (image.Width != squareItemSize || image.Height != squareItemSize)
            {
                Console.WriteLine($"WARN: Image {fi.Name} is not of square size {squareItemSize} but {image.Width}x{image.Height} and will be resized through interpolation");
            }

            Draw(canvas, image, x, y, squareItemSize);

            x += squareItemSize;
            if (x >= width)
            {
                x = 0;
                y += squareItemSize;
            }
        }

        canvas.Save();
        return bitmap;

        static void Draw(Graphics canvas, Image image, int x, int y, int squareItemSize)
        {
            var srcRect = new Rectangle(0, 0, image.Width, image.Height);
            var destRect = new Rectangle(x, y, width: squareItemSize, height: squareItemSize);
            canvas.DrawImage(image, destRect: destRect, srcRect: srcRect, srcUnit: GraphicsUnit.Pixel);
        }
    }
}
