using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Application.Features.ApprovalDocuments.Commands;
using ProjectApi.Application.Features.ApprovalDocuments.Queries;
using ProjectApi.WebApi.Common;

namespace ProjectApi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApprovalDocumentsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ApprovalDocumentsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetApprovalDocumentsQuery());
        return Ok(ApiResponse.Success(result));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateApprovalDocumentCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(ApiResponse.Success(new { id }));
    }

    [HttpPut("approve")]
    public async Task<IActionResult> Approve([FromBody] ApproveDocumentsCommand command)
    {
        await _mediator.Send(command);
        return Ok(ApiResponse.Success());
    }

    [HttpPut("reject")]
    public async Task<IActionResult> Reject([FromBody] RejectDocumentsCommand command)
    {
        await _mediator.Send(command);
        return Ok(ApiResponse.Success());
    }
}
