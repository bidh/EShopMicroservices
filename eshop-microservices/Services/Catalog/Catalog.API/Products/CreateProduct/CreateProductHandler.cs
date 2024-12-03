using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, 
        List<string> Category, 
        string Description, 
        string ImageFile, 
        decimal Price) : ICommand<CreateProductResult>;
    //needs to inherit IRequest to trigger CreateProductCommandHandler

    public record CreateProductResult(Guid Id);

    //IRequestHandler is a MediatR interface that is used to handle requests
    //CreateProductCommand is the request that is being handled
    //CreateProductResult is the result of the request
    internal class CreateProductCommandHandler 
        : ICommandHandler<CreateProductCommand,CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            return new CreateProductResult(product.Id);
        }
    }
}
