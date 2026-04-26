using ProjectApi.Domain.Entities;
using ProjectApi.Domain.Interfaces;

namespace ProjectApi.Infrastructure.Persistence.Repositories;

public class ExamResultRepository : Repository<ExamResult>, IExamResultRepository
{
    public ExamResultRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
