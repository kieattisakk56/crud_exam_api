using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Application.Features.ProductQrCodes.Commands;
using ProjectApi.Application.Features.ProductQrCodes.Queries;
using ProjectApi.WebApi.Common;

namespace ProjectApi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductQrCodesController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductQrCodesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetProductQrCodesQuery());
        return Ok(ApiResponse.Success(result));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductQrCodeCommand command)
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
        var result = await _mediator.Send(new DeleteProductQrCodeCommand { Id = id });
        return result
            ? Ok(ApiResponse.Success())
            : Ok(ApiResponse.Fail(404, "Product QR code not found.", "ไม่พบรหัสสินค้า"));
    }
}
