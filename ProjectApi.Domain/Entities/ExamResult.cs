using ProjectApi.Domain.Common;

namespace ProjectApi.Domain.Entities;

/// <summary>
/// IT 10: Exam Result
/// Stores exam submission results for each examinee
/// </summary>
public class ExamResult : BaseEntity
{
    public string ExamineeName { get; set; } = string.Empty;
    public int Score { get; set; }
    public int TotalQuestions { get; set; }
    public string AnswersJson { get; set; } = string.Empty; // JSON array of selected answers
}
