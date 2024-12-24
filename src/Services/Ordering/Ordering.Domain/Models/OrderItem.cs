﻿namespace Ordering.Domain.Models
{
    public class OrderItem : Entity<Guid>
    {
        public OrderId OrderId { get; private set; } = default!;
        public ProductId ProductId { get; private set; } = default!;
        public int Quantity{ get; private set; } = default!;
        public decimal Price { get; private set; } = default!;        

        internal OrderItem(OrderId orderId, ProductId productId, int quantity, decimal price)
        {
            OrderId = orderId;
            ProductId = productId;
            Price = price;
            Quantity = quantity;
        }
    }
}