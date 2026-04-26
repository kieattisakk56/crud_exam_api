using MediatR;
using ProjectApi.Domain.Common;
using ProjectApi.Domain.Entities;

namespace ProjectApi.Application.Features.ProductQrCodes.Queries;

public class ProductQrCodeDto { public int Id { get; set; } public string Code { get; set; } = string.Empty; }

public class GetProductQrCodesQuery : IRequest<List<ProductQrCodeDto>> { }

public class GetProductQrCodesQueryHandler : IRequestHandler<GetProductQrCodesQuery, List<ProductQrCodeDto>>
{
    private readonly IUnitOfWork _uow;
    public GetProductQrCodesQueryHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<List<ProductQrCodeDto>> Handle(GetProductQrCodesQuery request, CancellationToken ct)
    {
        var items = await _uow.ProductQrCodes.GetAllAsync(ct);
        return items.OrderBy(p => p.Id).Select(p => new ProductQrCodeDto { Id = p.Id, Code = p.Code }).ToList();
    }
}
