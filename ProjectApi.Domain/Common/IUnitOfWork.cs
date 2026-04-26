using ProjectApi.Domain.Interfaces;

namespace ProjectApi.Domain.Common;

public interface IUnitOfWork : IDisposable
{
    IPersonRepository Persons { get; }
    IApprovalDocumentRepository ApprovalDocuments { get; }
    IEmployeeProfileRepository EmployeeProfiles { get; }
    IQueueTicketRepository QueueTickets { get; }
    IProductBarcodeRepository ProductBarcodes { get; }
    IProductQrCodeRepository ProductQrCodes { get; }
    IExamQuestionRepository ExamQuestions { get; }
    IPostRepository Posts { get; }
    ICommentRepository Comments { get; }
    IExamResultRepository ExamResults { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
