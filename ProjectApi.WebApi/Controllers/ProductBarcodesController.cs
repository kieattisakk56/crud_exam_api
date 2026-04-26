using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Application.Features.ProductBarcodes.Commands;
using ProjectApi.Application.Features.ProductBarcodes.Queries;
using ProjectApi.WebApi.Common;
using BarcodeStandard;
using SkiaSharp;

namespace ProjectApi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductBarcodesController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductBarcodesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _mediator.Send(new GetProductBarcodesQuery());
        var result = items.Select(p => new
        {
            p.Id,
            p.Code,
            BarcodeBase64 = GenerateBarcodeBase64(p.Code)
        }).ToList();
        return Ok(ApiResponse.Success(result));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductBarcodeCommand command)
    {
        try
        {
            var id = await _mediator.Send(command);
            return Ok(ApiResponse.Success(new { id }));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ApiResponse.Fail(400, ex.Message, "รหัสสินค้าซ้ำหรือข้อมูลไม่ถูกต้อง"));
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteProductBarcodeCommand { Id = id });
        return result
            ? Ok(ApiResponse.Success())
            : Ok(ApiResponse.Fail(404, "Product barcode not found.", "ไม่พบรหัสสินค้า"));
    }

    private static string GenerateBarcodeBase64(string code)
    {
        var barcode = new Barcode { IncludeLabel = true };
        using var image = barcode.Encode(BarcodeStandard.Type.Code39, code.Replace("-", ""),
            SKColors.Black, SKColors.White, 400, 80);
        using var pngData = image.Encode(SKEncodedImageFormat.Png, 100);
        return Convert.ToBase64String(pngData.ToArray());
    }
}
