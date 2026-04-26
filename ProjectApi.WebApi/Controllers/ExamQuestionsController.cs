using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Application.Features.ExamQuestions.Commands;
using ProjectApi.Application.Features.ExamQuestions.Queries;
using ProjectApi.WebApi.Common;

namespace ProjectApi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExamQuestionsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ExamQuestionsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetExamQuestionsQuery());
        return Ok(ApiResponse.Success(result));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateExamQuestionCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(ApiResponse.Success(new { id }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteExamQuestionCommand { Id = id });
        return result
            ? Ok(ApiResponse.Success())
            : Ok(ApiResponse.Fail(404, "Question not found.", "ไม่พบข้อสอบ"));
    }
}
