using MediatR;
using ProjectApi.Domain.Common;
using ProjectApi.Domain.Entities;

namespace ProjectApi.Application.Features.ProductBarcodes.Queries;

public class ProductBarcodeDto { public int Id { get; set; } public string Code { get; set; } = string.Empty; }

public class GetProductBarcodesQuery : IRequest<List<ProductBarcodeDto>> { }

public class GetProductBarcodesQueryHandler : IRequestHandler<GetProductBarcodesQuery, List<ProductBarcodeDto>>
{
    private readonly IUnitOfWork _uow;
    public GetProductBarcodesQueryHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<List<ProductBarcodeDto>> Handle(GetProductBarcodesQuery request, CancellationToken ct)
    {
        var items = await _uow.ProductBarcodes.GetAllAsync(ct);
        return items.OrderBy(p => p.Id).Select(p => new ProductBarcodeDto { Id = p.Id, Code = p.Code }).ToList();
    }
}
