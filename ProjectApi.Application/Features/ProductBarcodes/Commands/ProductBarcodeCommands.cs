using MediatR;
using ProjectApi.Domain.Common;
using ProjectApi.Domain.Entities;

namespace ProjectApi.Application.Features.ProductBarcodes.Commands;

public class CreateProductBarcodeCommand : IRequest<int>
{
    public string Code { get; set; } = string.Empty;
}

public class CreateProductBarcodeCommandHandler : IRequestHandler<CreateProductBarcodeCommand, int>
{
    private readonly IUnitOfWork _uow;
    public CreateProductBarcodeCommandHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<int> Handle(CreateProductBarcodeCommand request, CancellationToken ct)
    {
        var existing = await _uow.ProductBarcodes.FindAsync(p => p.Code == request.Code, ct);
        if (existing.Count > 0) throw new InvalidOperationException("รหัสสินค้านี้มีอยู่แล้ว");

        var item = new ProductBarcode { Code = request.Code };
        await _uow.ProductBarcodes.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}

public class DeleteProductBarcodeCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class DeleteProductBarcodeCommandHandler : IRequestHandler<DeleteProductBarcodeCommand, bool>
{
    private readonly IUnitOfWork _uow;
    public DeleteProductBarcodeCommandHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<bool> Handle(DeleteProductBarcodeCommand request, CancellationToken ct)
    {
        var item = await _uow.ProductBarcodes.GetByIdAsync(request.Id, ct);
        if (item == null) return false;
        _uow.ProductBarcodes.Delete(item);
        await _uow.SaveChangesAsync(ct);
        return true;
    }
}
