using MediatR;
using ProjectApi.Domain.Common;

namespace ProjectApi.Application.Features.ApprovalDocuments.Commands;

public class RejectDocumentsCommand : IRequest<bool>
{
    public List<int> Ids { get; set; } = new();
    public string Reason { get; set; } = string.Empty;
}

public class RejectDocumentsCommandHandler : IRequestHandler<RejectDocumentsCommand, bool>
{
    private readonly IUnitOfWork _uow;
    public RejectDocumentsCommandHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<bool> Handle(RejectDocumentsCommand request, CancellationToken ct)
    {
        foreach (var id in request.Ids)
        {
            var doc = await _uow.ApprovalDocuments.GetByIdAsync(id, ct);
            if (doc != null)
            {
                doc.Status = 2;
                doc.Reason = request.Reason;
                doc.ApprovedAt = DateTime.UtcNow;
                doc.ApprovedBy = "admin";
                _uow.ApprovalDocuments.Update(doc);
            }
        }
        await _uow.SaveChangesAsync(ct);
        return true;
    }
}
