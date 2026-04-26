using Microsoft.EntityFrameworkCore;
using ProjectApi.Domain.Entities;
using ProjectApi.Domain.Interfaces;

namespace ProjectApi.Infrastructure.Persistence.Repositories;

public class ExamQuestionRepository : Repository<ExamQuestion>, IExamQuestionRepository
{
    public ExamQuestionRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<IReadOnlyList<ExamQuestion>> GetAllWithChoicesAsync(CancellationToken ct = default)
    {
        return await _dbContext.ExamQuestions
            .Where(e => !e.IsDeleted)
            .Include(e => e.Choices.Where(c => !c.IsDeleted).OrderBy(c => c.ChoiceNumber))
            .OrderBy(e => e.QuestionNumber)
            .ToListAsync(ct);
    }

    public async Task<ExamQuestion?> GetByIdWithChoicesAsync(int id, CancellationToken ct = default)
    {
        return await _dbContext.ExamQuestions
            .Where(e => e.Id == id && !e.IsDeleted)
            .Include(e => e.Choices.Where(c => !c.IsDeleted).OrderBy(c => c.ChoiceNumber))
            .FirstOrDefaultAsync(ct);
    }

    public void DeleteChoice(ExamQuestionChoice choice)
    {
        _dbContext.ExamQuestionChoices.Remove(choice);
    }
}
