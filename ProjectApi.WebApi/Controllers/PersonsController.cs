using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Application.Features.Persons.Commands;
using ProjectApi.Application.Features.Persons.Queries;
using ProjectApi.WebApi.Common;

namespace ProjectApi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IMediator _mediator;
    public PersonsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetPersonsQuery());
        return Ok(ApiResponse.Success(result));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(ApiResponse.Success(new { id }));
    }
}
