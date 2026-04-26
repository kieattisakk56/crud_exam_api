using MediatR;
using ProjectApi.Domain.Common;
using ProjectApi.Domain.Entities;

namespace ProjectApi.Application.Features.ApprovalDocuments.Queries;

public class ApprovalDocumentDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public int Status { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public string? ApprovedBy { get; set; }
}

public class GetApprovalDocumentsQuery : IRequest<List<ApprovalDocumentDto>> { }

public class GetApprovalDocumentsQueryHandler : IRequestHandler<GetApprovalDocumentsQuery, List<ApprovalDocumentDto>>
{
    private readonly IUnitOfWork _uow;
    public GetApprovalDocumentsQueryHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<List<ApprovalDocumentDto>> Handle(GetApprovalDocumentsQuery request, CancellationToken ct)
    {
        var items = await _uow.ApprovalDocuments.GetAllAsync(ct);
        return items.OrderBy(d => d.Id).Select(d => new ApprovalDocumentDto
        {
            Id = d.Id, Title = d.Title, Description = d.Description,
            Reason = d.Reason, Status = d.Status, ApprovedAt = d.ApprovedAt, ApprovedBy = d.ApprovedBy
        }).ToList();
    }
}
