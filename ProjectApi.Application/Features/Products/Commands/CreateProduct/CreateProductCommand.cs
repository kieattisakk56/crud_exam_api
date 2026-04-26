using MediatR;

namespace ProjectApi.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    // Inject Repository or DbContext here
    public CreateProductCommandHandler()
    {
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Example: Add to database and return ID
        // var product = new Product { Name = request.Name, Price = request.Price };
        // _context.Products.Add(product);
        // await _context.SaveChangesAsync(cancellationToken);
        
        return await Task.FromResult(1); // Return dummy ID
    }
}
