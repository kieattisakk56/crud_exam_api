using ProjectApi.Domain.Entities;
using ProjectApi.Domain.Interfaces;

namespace ProjectApi.Infrastructure.Persistence.Repositories;

public class PostCommentRepository : Repository<PostComment>, IPostCommentRepository
{
    public PostCommentRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
