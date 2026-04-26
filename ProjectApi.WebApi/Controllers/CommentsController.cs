using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Application.Features.Comments.Commands;
using ProjectApi.Application.Features.Comments.Queries;
using ProjectApi.WebApi.Common;

namespace ProjectApi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly IMediator _mediator;
    public CommentsController(IMediator mediator) => _mediator = mediator;

    [HttpGet("post/{postId}")]
    public async Task<IActionResult> GetByPostId(int postId)
    {
        var result = await _mediator.Send(new GetCommentsByPostIdQuery(postId));
        return Ok(ApiResponse.Success(result));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCommentCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(ApiResponse.Success(new { id }));
    }
}
