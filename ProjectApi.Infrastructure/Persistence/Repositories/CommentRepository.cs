using ProjectApi.Domain.Entities;
using ProjectApi.Domain.Interfaces;
using ProjectApi.Infrastructure.Persistence;

namespace ProjectApi.Infrastructure.Persistence.Repositories;

public class CommentRepository : Repository<Comment>, ICommentRepository
{
    public CommentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
