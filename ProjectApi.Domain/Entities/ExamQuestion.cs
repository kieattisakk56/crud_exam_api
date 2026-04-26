using ProjectApi.Domain.Common;

namespace ProjectApi.Domain.Entities;

/// <summary>
/// IT 08: Exam Question Management — supports 1-to-many choices
/// CorrectAnswer is the ChoiceNumber (1-based) of the correct choice
/// </summary>
public class ExamQuestion : BaseEntity
{
    public int QuestionNumber { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public int CorrectAnswer { get; set; } = 1;
    public ICollection<ExamQuestionChoice> Choices { get; set; } = new List<ExamQuestionChoice>();
}
