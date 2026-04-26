using ProjectApi.Domain.Common;
using ProjectApi.Domain.Entities;

namespace ProjectApi.Domain.Interfaces;

public interface IExamQuestionRepository : IRepository<ExamQuestion>
{
    Task<IReadOnlyList<ExamQuestion>> GetAllWithChoicesAsync(CancellationToken cancellationToken = default);
    Task<ExamQuestion?> GetByIdWithChoicesAsync(int id, CancellationToken cancellationToken = default);
    void DeleteChoice(ExamQuestionChoice choice);
}
