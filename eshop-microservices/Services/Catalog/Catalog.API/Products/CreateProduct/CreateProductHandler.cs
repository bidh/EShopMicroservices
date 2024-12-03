using MediatR;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, 
        List<string> Category, 
        string Description, 
        string ImageFile, 
        decimal Price) : IRequest<CreateProductResult>;
    //needs to inherit IRequest to trigger CreateProductCommandHandler

    public record CreateProductResult(Guid Id);

    //IRequestHandler is a MediatR interface that is used to handle requests
    //CreateProductCommand is the request that is being handled
    //CreateProductResult is the result of the request
    internal class CreateProductCommandHandler 
        : IRequestHandler<CreateProductCommand,CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
