using ProjectApi.Domain.Entities;
using ProjectApi.Domain.Interfaces;

namespace ProjectApi.Infrastructure.Persistence.Repositories;

public class PersonRepository : Repository<Person>, IPersonRepository
{
    public PersonRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
