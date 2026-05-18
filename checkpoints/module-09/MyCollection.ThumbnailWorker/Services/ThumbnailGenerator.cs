using SkiaSharp;

namespace MyCollection.ThumbnailWorker.Services;

public class ThumbnailGenerator
{
    private const int ThumbnailWidth = 200;
    private const int ThumbnailHeight = 200;
    private const int WebpQuality = 80;

    public async Task CreateThumbnailAsync(string sourcePath, string destinationPath)
    {
        if (!File.Exists(sourcePath))
        {
            throw new FileNotFoundException("The source image could not be found.", sourcePath);
        }

        Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);

        using var sourceStream = File.OpenRead(sourcePath);
        using var sourceBitmap = SKBitmap.Decode(sourceStream);

        if (sourceBitmap is null)
        {
            throw new InvalidOperationException($"Could not decode image '{sourcePath}'.");
        }

        using var outputBitmap = new SKBitmap(ThumbnailWidth, ThumbnailHeight);
        using var canvas = new SKCanvas(outputBitmap);
        canvas.Clear(SKColors.White);

        var scale = Math.Min(
            (float)ThumbnailWidth / sourceBitmap.Width,
            (float)ThumbnailHeight / sourceBitmap.Height);

        var scaledWidth = sourceBitmap.Width * scale;
        var scaledHeight = sourceBitmap.Height * scale;
        var left = (ThumbnailWidth - scaledWidth) / 2;
        var top = (ThumbnailHeight - scaledHeight) / 2;

        var destinationRectangle = new SKRect(left, top, left + scaledWidth, top + scaledHeight);

        canvas.DrawBitmap(sourceBitmap, destinationRectangle);
        canvas.Flush();

        using var image = SKImage.FromBitmap(outputBitmap);
        using var encodedData = image.Encode(SKEncodedImageFormat.Webp, WebpQuality);

        await using var outputStream = File.Create(destinationPath);
        encodedData.SaveTo(outputStream);
        await outputStream.FlushAsync();
    }
}
