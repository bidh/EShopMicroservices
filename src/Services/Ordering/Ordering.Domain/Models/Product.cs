namespace Ordering.Domain.Models
{
    // it is anemic domain models because it only contains properties
    public class Product : Entity<ProductId>
    {
        public string Name { get; private set; } = default!;
        public decimal Price { get; private set; } = default!;
    }
}
