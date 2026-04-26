using ProjectApi.Domain.Common;

namespace ProjectApi.Domain.Entities;

public class ExamQuestionChoice : BaseEntity
{
    public int ExamQuestionId { get; set; }
    public int ChoiceNumber { get; set; }
    public string ChoiceText { get; set; } = string.Empty;
}
