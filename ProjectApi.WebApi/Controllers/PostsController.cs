using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Application.Features.Posts.Queries;
using ProjectApi.WebApi.Common;

namespace ProjectApi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IMediator _mediator;
    public PostsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetPostsQuery());
        return Ok(ApiResponse.Success(result));
    }
}
