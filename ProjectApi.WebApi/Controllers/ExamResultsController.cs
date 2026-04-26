using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Application.Features.ExamResults.Commands;
using ProjectApi.Application.Features.ExamResults.Queries;
using ProjectApi.WebApi.Common;

namespace ProjectApi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExamResultsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ExamResultsController(IMediator mediator) => _mediator = mediator;

    [HttpGet("questions")]
    public async Task<IActionResult> GetQuestions()
    {
        var result = await _mediator.Send(new GetExamForTakingQuery());
        return Ok(ApiResponse.Success(result));
    }

    [HttpPost("submit")]
    public async Task<IActionResult> SubmitExam([FromBody] SubmitExamCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.ExamineeName))
            return Ok(ApiResponse.Fail(400, "Examinee name is required.", "กรุณาระบุชื่อผู้สอบ"));

        var result = await _mediator.Send(command);
        return Ok(ApiResponse.Success(result));
    }
}
