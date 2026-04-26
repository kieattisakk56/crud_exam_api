using MediatR;

namespace ProjectApi.Application.Features.Products.Queries.GetProducts;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

public class GetProductsQuery : IRequest<List<ProductDto>>
{
}

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
{
    // Inject Repository or DbContext here
    public GetProductsQueryHandler()
    {
    }

    public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        // Example: Fetch from database
        // return await _context.Products.Select(p => new ProductDto { ... }).ToListAsync(cancellationToken);
        
        var dummyData = new List<ProductDto>
        {
            new ProductDto { Id = 1, Name = "Sample Product", Price = 99.99m }
        };

        return await Task.FromResult(dummyData);
    }
}
