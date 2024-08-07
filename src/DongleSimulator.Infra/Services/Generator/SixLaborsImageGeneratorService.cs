using DongleSimulator.Domain.Services.Generator;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace DongleSimulator.Infra.Services.Generator;

public class SixLaborsImageGeneratorService : IImageGeneratorService
{
    private readonly HttpClient _httpClient;

    public SixLaborsImageGeneratorService(HttpClient httpClient) => _httpClient = httpClient;
    
    public async Task<Stream> Generate(IList<string> sourceUrl, string templateUrl, int replaceCount = 1)
    {
        if (replaceCount < 1 || replaceCount > 2) throw new Exception();
        
        var templateStream = await _httpClient.GetStreamAsync(templateUrl);
        using var template = Image.Load<Rgba32>(templateStream);
        
        var firstSourceStream = await _httpClient.GetStreamAsync(sourceUrl[0]);
        
        var greenArea = FindColoredArea(template, IsGreen);
        if (greenArea is not { Width: > 0, Height: > 0 }) throw new NotImplementedException();
        
        using var firstSource = Image.Load<Rgba32>(firstSourceStream);
        using var sourceResized = firstSource.Clone(x => x.Resize(new ResizeOptions
        {
            Size = new Size(greenArea.Width, greenArea.Height),
            Mode = ResizeMode.Crop
        }));

        ReplaceArea(template, sourceResized, greenArea, IsGreen);

        if (replaceCount == 2)
        {
            var secondSourceStream = await _httpClient.GetStreamAsync(sourceUrl[1]);
            
            var purpleArea = FindColoredArea(template, IsPurple);
            if (purpleArea is not { Width: > 0, Height: > 0 }) throw new NotImplementedException();

            using var secondSource = Image.Load<Rgba32>(secondSourceStream);
            using var sourceResizedPurple = secondSource.Clone(x => x.Resize(new ResizeOptions
            {
                Size = new Size(purpleArea.Width, purpleArea.Height),
                Mode = ResizeMode.Crop
            }));

            ReplaceArea(template, sourceResizedPurple, purpleArea, IsPurple);
        }

        var outputStream = new MemoryStream();

        await template.SaveAsPngAsync(outputStream);
        outputStream.Seek(0, SeekOrigin.Begin);
        
        return outputStream;
    }

    private static void ReplaceArea(Image<Rgba32> template, Image<Rgba32> source, Rectangle area, Func<Rgba32, bool> isColor)
    {
        for (var y = 0; y < area.Height; y++)
        {
            for (var x = 0; x < area.Width; x++)
            {
                var srcX = x * source.Width / area.Width;
                var srcY = y * source.Height / area.Height;

                if (srcX < 0 || srcX >= source.Width || srcY < 0 || srcY >= source.Height) continue;

                var sourcePixel = source[srcX, srcY];

                var destX = area.X + x;
                var destY = area.Y + y;

                if (destX < 0 || destX >= template.Width || destY < 0 || destY >= template.Height) continue;

                var pixel = template[destX, destY];
                if (isColor(pixel)) template[destX, destY] = sourcePixel;
            }
        }
    }
    
    private static Rectangle FindColoredArea(Image<Rgba32> image, Func<Rgba32, bool> isColor)
    {
        var minX = image.Width;
        var minY = image.Height;
        var maxX = 0;
        var maxY = 0;

        for (var y = 0; y < image.Height; y++)
        {
            for (var x = 0; x < image.Width; x++)
            {
                var pixel = image[x, y];
                if (!isColor(pixel)) continue;
                minX = Math.Min(minX, x);
                minY = Math.Min(minY, y);
                maxX = Math.Max(maxX, x);
                maxY = Math.Max(maxY, y);
            }
        }

        return new Rectangle(minX, minY, maxX - minX + 1, maxY - minY + 1);
    }

    private static bool IsGreen(Rgba32 pixel) => pixel is { R: <= 150, G: >= 180, B: <= 150 };
    private static bool IsPurple(Rgba32 pixel) => pixel is { R: >= 128, G: <= 0, B: >= 128 };
}