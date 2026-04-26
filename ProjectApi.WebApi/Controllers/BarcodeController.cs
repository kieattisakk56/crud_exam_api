using Microsoft.AspNetCore.Mvc;
using QRCoder;
using BarcodeStandard;
using SkiaSharp;

namespace ProjectApi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BarcodeController : ControllerBase
{
    private const int MaxDataLength = 100;

    /// <summary>
    /// Generate a Code39 barcode image from the given data string.
    /// GET /api/barcode/code39?data=XXXX-XXXX-XXXX-XXXX
    /// </summary>
    [HttpGet("code39")]
    public IActionResult GenerateCode39([FromQuery] string data)
    {
        if (string.IsNullOrWhiteSpace(data))
            return BadRequest("Data parameter is required.");

        if (data.Length > MaxDataLength)
            return BadRequest($"Data must not exceed {MaxDataLength} characters.");

        try
        {
            var barcode = new Barcode { IncludeLabel = true };
            using var image = barcode.Encode(BarcodeStandard.Type.Code39, data.Replace("-", ""), SKColors.Black, SKColors.White, 400, 80);
            using var pngData = image.Encode(SKEncodedImageFormat.Png, 100);

            Response.Headers["Cache-Control"] = "public, max-age=3600";
            return File(pngData.ToArray(), "image/png");
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to generate barcode: {ex.Message}");
        }
    }

    /// <summary>
    /// Generate a QR Code image from the given data string.
    /// GET /api/barcode/qrcode?data=XXXXX-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX
    /// </summary>
    [HttpGet("qrcode")]
    public IActionResult GenerateQRCode([FromQuery] string data, [FromQuery] int size = 10)
    {
        if (string.IsNullOrWhiteSpace(data))
            return BadRequest("Data parameter is required.");

        if (data.Length > MaxDataLength * 2)
            return BadRequest($"Data must not exceed {MaxDataLength * 2} characters.");

        if (size < 1 || size > 50)
            return BadRequest("Size must be between 1 and 50.");

        try
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);

            var qrCodeBytes = qrCode.GetGraphic(size);

            Response.Headers["Cache-Control"] = "public, max-age=3600";
            return File(qrCodeBytes, "image/png");
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to generate QR code: {ex.Message}");
        }
    }
}
