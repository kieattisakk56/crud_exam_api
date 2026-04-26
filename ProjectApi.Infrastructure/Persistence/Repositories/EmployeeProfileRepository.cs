using ProjectApi.Domain.Entities;
using ProjectApi.Domain.Interfaces;

namespace ProjectApi.Infrastructure.Persistence.Repositories;

public class EmployeeProfileRepository : Repository<EmployeeProfile>, IEmployeeProfileRepository
{
    public EmployeeProfileRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
