using MediatR;
using ProjectApi.Domain.Common;
using ProjectApi.Domain.Entities;
using System.Text.Json;

namespace ProjectApi.Application.Features.ExamResults.Commands;

public class SubmitExamCommand : IRequest<SubmitExamResult>
{
    public string ExamineeName { get; set; } = string.Empty;
    public List<ExamAnswerItem> Answers { get; set; } = new();
}

public class ExamAnswerItem
{
    public int QuestionId { get; set; }
    public int SelectedAnswer { get; set; }
}

public class SubmitExamResult
{
    public int Score { get; set; }
    public int TotalQuestions { get; set; }
    public string ExamineeName { get; set; } = string.Empty;
    public int Id { get; set; }
}

public class SubmitExamCommandHandler : IRequestHandler<SubmitExamCommand, SubmitExamResult>
{
    private readonly IUnitOfWork _uow;
    public SubmitExamCommandHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<SubmitExamResult> Handle(SubmitExamCommand request, CancellationToken ct)
    {
        var questions = await _uow.ExamQuestions.GetAllAsync(ct);
        int score = 0;

        foreach (var answer in request.Answers)
        {
            var question = questions.FirstOrDefault(q => q.Id == answer.QuestionId);
            if (question != null && question.CorrectAnswer == answer.SelectedAnswer)
                score++;
        }

        var result = new ExamResult
        {
            ExamineeName = request.ExamineeName,
            Score = score,
            TotalQuestions = questions.Count,
            AnswersJson = JsonSerializer.Serialize(request.Answers)
        };

        await _uow.ExamResults.AddAsync(result, ct);
        await _uow.SaveChangesAsync(ct);

        return new SubmitExamResult { Score = score, TotalQuestions = questions.Count, ExamineeName = request.ExamineeName, Id = result.Id };
    }
}
