using ProjectApi.Domain.Entities;
using ProjectApi.Domain.Interfaces;

namespace ProjectApi.Infrastructure.Persistence.Repositories;

public class ProductBarcodeRepository : Repository<ProductBarcode>, IProductBarcodeRepository
{
    public ProductBarcodeRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
