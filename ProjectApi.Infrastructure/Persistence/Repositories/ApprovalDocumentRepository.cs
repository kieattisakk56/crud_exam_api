using ProjectApi.Domain.Entities;
using ProjectApi.Domain.Interfaces;

namespace ProjectApi.Infrastructure.Persistence.Repositories;

public class ApprovalDocumentRepository : Repository<ApprovalDocument>, IApprovalDocumentRepository
{
    public ApprovalDocumentRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
