using MediatR;
using ProjectApi.Domain.Common;
using ProjectApi.Domain.Entities;

namespace ProjectApi.Application.Features.ExamQuestions.Commands;

public class CreateExamQuestionCommand : IRequest<int>
{
    public string QuestionText { get; set; } = string.Empty;
    public List<string> Choices { get; set; } = new();
    public int CorrectAnswer { get; set; } = 1;
}

public class CreateExamQuestionCommandHandler : IRequestHandler<CreateExamQuestionCommand, int>
{
    private readonly IUnitOfWork _uow;
    public CreateExamQuestionCommandHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<int> Handle(CreateExamQuestionCommand request, CancellationToken ct)
    {
        if (request.Choices.Count < 2)
            throw new InvalidOperationException("ต้องมีตัวเลือกอย่างน้อย 2 ข้อ");
        if (request.CorrectAnswer < 1 || request.CorrectAnswer > request.Choices.Count)
            throw new InvalidOperationException("เฉลยต้องอยู่ในช่วงตัวเลือกที่มี");

        var all = await _uow.ExamQuestions.GetAllAsync(ct);
        int maxNum = all.Count > 0 ? all.Max(q => q.QuestionNumber) : 0;

        var item = new ExamQuestion
        {
            QuestionNumber = maxNum + 1,
            QuestionText = request.QuestionText,
            CorrectAnswer = request.CorrectAnswer,
            Choices = request.Choices.Select((text, i) => new ExamQuestionChoice
            {
                ChoiceNumber = i + 1,
                ChoiceText = text
            }).ToList()
        };

        await _uow.ExamQuestions.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}

public class DeleteExamQuestionCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class DeleteExamQuestionCommandHandler : IRequestHandler<DeleteExamQuestionCommand, bool>
{
    private readonly IUnitOfWork _uow;
    public DeleteExamQuestionCommandHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<bool> Handle(DeleteExamQuestionCommand request, CancellationToken ct)
    {
        var item = await _uow.ExamQuestions.GetByIdWithChoicesAsync(request.Id, ct);
        if (item == null) return false;

        foreach (var choice in item.Choices.ToList())
            _uow.ExamQuestions.DeleteChoice(choice);

        _uow.ExamQuestions.Delete(item);
        await _uow.SaveChangesAsync(ct);

        var remaining = (await _uow.ExamQuestions.GetAllAsync(ct)).OrderBy(q => q.QuestionNumber).ToList();
        for (int i = 0; i < remaining.Count; i++)
        {
            remaining[i].QuestionNumber = i + 1;
            _uow.ExamQuestions.Update(remaining[i]);
        }
        await _uow.SaveChangesAsync(ct);
        return true;
    }
}
