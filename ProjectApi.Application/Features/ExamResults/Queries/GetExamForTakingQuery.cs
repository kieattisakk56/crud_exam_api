using MediatR;
using ProjectApi.Domain.Common;

namespace ProjectApi.Application.Features.ExamResults.Queries;

public class ExamChoiceViewDto
{
    public int ChoiceNumber { get; set; }
    public string ChoiceText { get; set; } = string.Empty;
}

public class ExamQuestionViewDto
{
    public int Id { get; set; }
    public int QuestionNumber { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public List<ExamChoiceViewDto> Choices { get; set; } = new();
    // CorrectAnswer is intentionally NOT exposed
}

public class GetExamForTakingQuery : IRequest<List<ExamQuestionViewDto>> { }

public class GetExamForTakingQueryHandler : IRequestHandler<GetExamForTakingQuery, List<ExamQuestionViewDto>>
{
    private readonly IUnitOfWork _uow;
    public GetExamForTakingQueryHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<List<ExamQuestionViewDto>> Handle(GetExamForTakingQuery request, CancellationToken ct)
    {
        var items = await _uow.ExamQuestions.GetAllWithChoicesAsync(ct);
        return items.Select(q => new ExamQuestionViewDto
        {
            Id = q.Id,
            QuestionNumber = q.QuestionNumber,
            QuestionText = q.QuestionText,
            Choices = q.Choices.Select(c => new ExamChoiceViewDto
            {
                ChoiceNumber = c.ChoiceNumber,
                ChoiceText = c.ChoiceText
            }).ToList()
        }).ToList();
    }
}
