using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Application.Features.PostComments.Commands;
using ProjectApi.Application.Features.PostComments.Queries;
using ProjectApi.WebApi.Common;

namespace ProjectApi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostCommentsController : ControllerBase
{
    private readonly IMediator _mediator;
    public PostCommentsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetPostCommentsQuery());
        return Ok(ApiResponse.Success(result));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePostCommentCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(ApiResponse.Success(new { id }));
    }
}
