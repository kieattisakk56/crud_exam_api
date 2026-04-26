using ProjectApi.Domain.Entities;
using ProjectApi.Domain.Interfaces;

namespace ProjectApi.Infrastructure.Persistence.Repositories;

public class ProductQrCodeRepository : Repository<ProductQrCode>, IProductQrCodeRepository
{
    public ProductQrCodeRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
