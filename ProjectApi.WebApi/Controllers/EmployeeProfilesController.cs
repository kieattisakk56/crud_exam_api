using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Application.Features.EmployeeProfiles.Commands;
using ProjectApi.WebApi.Common;

namespace ProjectApi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeProfilesController : ControllerBase
{
    private readonly IMediator _mediator;
    public EmployeeProfilesController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeProfileCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(ApiResponse.Success(new { id }));
    }
}
