using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Application.Features.QueueTickets.Commands;
using ProjectApi.Application.Features.QueueTickets.Queries;
using ProjectApi.WebApi.Common;

namespace ProjectApi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QueueTicketsController : ControllerBase
{
    private readonly IMediator _mediator;
    public QueueTicketsController(IMediator mediator) => _mediator = mediator;

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrent()
    {
        var ticketNumber = await _mediator.Send(new GetCurrentQueueQuery());
        return Ok(ApiResponse.Success(new { ticketNumber }));
    }

    [HttpPost("issue")]
    public async Task<IActionResult> IssueTicket()
    {
        var ticketNumber = await _mediator.Send(new IssueTicketCommand());
        return Ok(ApiResponse.Success(new { ticketNumber, issuedAt = DateTime.UtcNow }));
    }

    [HttpPost("clear")]
    public async Task<IActionResult> ClearQueue()
    {
        await _mediator.Send(new ClearQueueCommand());
        return Ok(ApiResponse.Success(new { ticketNumber = "00" }));
    }
}
