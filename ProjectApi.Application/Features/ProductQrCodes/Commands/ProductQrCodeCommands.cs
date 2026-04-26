using MediatR;
using ProjectApi.Domain.Common;
using ProjectApi.Domain.Entities;

namespace ProjectApi.Application.Features.ProductQrCodes.Commands;

public class CreateProductQrCodeCommand : IRequest<int>
{
    public string Code { get; set; } = string.Empty;
}

public class CreateProductQrCodeCommandHandler : IRequestHandler<CreateProductQrCodeCommand, int>
{
    private readonly IUnitOfWork _uow;
    public CreateProductQrCodeCommandHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<int> Handle(CreateProductQrCodeCommand request, CancellationToken ct)
    {
        // Check uniqueness
        var existing = await _uow.ProductQrCodes.FindAsync(p => p.Code == request.Code, ct);
        if (existing.Count > 0) throw new InvalidOperationException("รหัสสินค้านี้มีอยู่แล้ว");

        var item = new ProductQrCode { Code = request.Code };
        await _uow.ProductQrCodes.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}

public class DeleteProductQrCodeCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class DeleteProductQrCodeCommandHandler : IRequestHandler<DeleteProductQrCodeCommand, bool>
{
    private readonly IUnitOfWork _uow;
    public DeleteProductQrCodeCommandHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<bool> Handle(DeleteProductQrCodeCommand request, CancellationToken ct)
    {
        var item = await _uow.ProductQrCodes.GetByIdAsync(request.Id, ct);
        if (item == null) return false;
        _uow.ProductQrCodes.Delete(item);
        await _uow.SaveChangesAsync(ct);
        return true;
    }
}
