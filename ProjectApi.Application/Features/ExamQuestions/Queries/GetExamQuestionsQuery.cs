using MediatR;
using ProjectApi.Domain.Common;

namespace ProjectApi.Application.Features.ExamQuestions.Queries;

public class ExamChoiceDto
{
    public int ChoiceNumber { get; set; }
    public string ChoiceText { get; set; } = string.Empty;
}

public class ExamQuestionDto
{
    public int Id { get; set; }
    public int QuestionNumber { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public List<ExamChoiceDto> Choices { get; set; } = new();
    public int CorrectAnswer { get; set; }
}

public class GetExamQuestionsQuery : IRequest<List<ExamQuestionDto>> { }

public class GetExamQuestionsQueryHandler : IRequestHandler<GetExamQuestionsQuery, List<ExamQuestionDto>>
{
    private readonly IUnitOfWork _uow;
    public GetExamQuestionsQueryHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<List<ExamQuestionDto>> Handle(GetExamQuestionsQuery request, CancellationToken ct)
    {
        var items = await _uow.ExamQuestions.GetAllWithChoicesAsync(ct);
        return items.Select(q => new ExamQuestionDto
        {
            Id = q.Id,
            QuestionNumber = q.QuestionNumber,
            QuestionText = q.QuestionText,
            CorrectAnswer = q.CorrectAnswer,
            Choices = q.Choices.Select(c => new ExamChoiceDto
            {
                ChoiceNumber = c.ChoiceNumber,
                ChoiceText = c.ChoiceText
            }).ToList()
        }).ToList();
    }
}
