using MediatR;
using ProjectApi.Domain.Common;
using ProjectApi.Domain.Entities;

namespace ProjectApi.Application.Features.ApprovalDocuments.Commands;

public class CreateApprovalDocumentCommand : IRequest<int>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class CreateApprovalDocumentCommandHandler : IRequestHandler<CreateApprovalDocumentCommand, int>
{
    private readonly IUnitOfWork _uow;
    public CreateApprovalDocumentCommandHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<int> Handle(CreateApprovalDocumentCommand request, CancellationToken ct)
    {
        var doc = new ApprovalDocument { Title = request.Title, Description = request.Description, Status = 0 };
        await _uow.ApprovalDocuments.AddAsync(doc, ct);
        await _uow.SaveChangesAsync(ct);
        return doc.Id;
    }
}
