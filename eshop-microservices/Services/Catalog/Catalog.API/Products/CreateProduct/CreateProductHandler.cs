using Microsoft.Extensions.Logging;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, 
        List<string> Category, 
        string Description, 
        string ImageFile, 
        decimal Price) : ICommand<CreateProductResult>;
    //needs to inherit IRequest to trigger CreateProductCommandHandler

    public record CreateProductResult(Guid Id);

    public class CreateroductComandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateroductComandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");            
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image file is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

    //IRequestHandler is a MediatR interface that is used to handle requests
    //CreateProductCommand is the request that is being handled
    //CreateProductResult is the result of the request
    internal class CreateProductCommandHandler
        (IDocumentSession session) 
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

            //save to the database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}
