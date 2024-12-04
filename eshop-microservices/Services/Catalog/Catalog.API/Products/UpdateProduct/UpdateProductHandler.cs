namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) 
        : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);
    public class UpdateProductCommandHandler 
        (IDocumentSession sesstion, ILogger<UpdateProductCommandHandler> logger)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation($"UpdateProductCommandHandler.Handle called with {command}");

            var product = await sesstion.LoadAsync<Product>(command.Id, cancellationToken) ?? throw new ProductNotFoundException();
            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;  
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;

            sesstion.Update(product);
            await sesstion.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}
