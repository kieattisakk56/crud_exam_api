using ProjectApi.Domain.Entities;
using ProjectApi.Domain.Interfaces;
using ProjectApi.Infrastructure.Persistence;

namespace ProjectApi.Infrastructure.Persistence.Repositories;

public class PostRepository : Repository<Post>, IPostRepository
{
    public PostRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
