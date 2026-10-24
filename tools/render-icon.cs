#:package Svg.Skia@5.2.3

// Renders the Evolutionary logo SVG into PNG assets of different sizes.
// Usage: dotnet run tools/render-icon.cs  (from the repository root)

using SkiaSharp;
using Svg.Skia;

Render("brand/evolutionary-finch.svg", "brand/evolutionary-finch-64.png", 64, 64);
Render("brand/evolutionary-finch.svg", "brand/evolutionary-finch-128.png", 128, 128);
Render("brand/evolutionary-finch.svg", "brand/evolutionary-finch-256.png", 256, 256);

static void Render(string svgPath, string pngPath, int width, int height)
{
    using var svg = new SKSvg();
    var picture = svg.Load(svgPath);
    if (picture is null)
    {
        throw new InvalidOperationException($"Could not load '{svgPath}'.");
    }

    using var stream = File.OpenWrite(pngPath);
    var scaleX = width / 128f;
    var scaleY = height / 128f;

    picture.ToImage(
        stream,
        SKColors.Empty,
        SKEncodedImageFormat.Png,
        100,
        scaleX,
        scaleY,
        SKImageInfo.PlatformColorType,
        SKAlphaType.Unpremul,
        SKColorSpace.CreateSrgb());

    Console.WriteLine($"{pngPath} ({width}x{height}) written.");
}
