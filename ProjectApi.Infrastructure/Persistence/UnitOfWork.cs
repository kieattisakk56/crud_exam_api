using ProjectApi.Domain.Common;
using ProjectApi.Domain.Interfaces;
using ProjectApi.Infrastructure.Persistence.Repositories;

namespace ProjectApi.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public IPersonRepository Persons { get; private set; }
    public IApprovalDocumentRepository ApprovalDocuments { get; private set; }
    public IEmployeeProfileRepository EmployeeProfiles { get; private set; }
    public IQueueTicketRepository QueueTickets { get; private set; }
    public IProductBarcodeRepository ProductBarcodes { get; private set; }
    public IProductQrCodeRepository ProductQrCodes { get; private set; }
    public IExamQuestionRepository ExamQuestions { get; private set; }
    public IPostRepository Posts { get; private set; }
    public ICommentRepository Comments { get; private set; }
    public IExamResultRepository ExamResults { get; private set; }

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        Persons = new PersonRepository(_dbContext);
        ApprovalDocuments = new ApprovalDocumentRepository(_dbContext);
        EmployeeProfiles = new EmployeeProfileRepository(_dbContext);
        QueueTickets = new QueueTicketRepository(_dbContext);
        ProductBarcodes = new ProductBarcodeRepository(_dbContext);
        ProductQrCodes = new ProductQrCodeRepository(_dbContext);
        ExamQuestions = new ExamQuestionRepository(_dbContext);
        Posts = new PostRepository(_dbContext);
        Comments = new CommentRepository(_dbContext);
        ExamResults = new ExamResultRepository(_dbContext);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
