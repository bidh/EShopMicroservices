namespace Ordering.Domain.Models
{
    // it is rich domain model which also contains the business logic
    public class Order : Aggregate<OrderId>
    {
        private readonly List<OrderItem> _orderItems = [];
        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public CustomerId CustomerId { get; private set; } = default!;
        public OrderName OrderName { get; private set; } = default!;
        public Address ShippingAddress { get; private set; } = default!;
        public Address BillingAddress { get; private set; } = default!;
        public Payment Payment { get; private set; } = default!;
        public OrderStatus OrderStatus { get; private set; } = OrderStatus.Pending;
        public decimal TotalPrice
        {
            get => _orderItems.Sum(item => item.Price * item.Quantity);
            private set { }
        } 

        public static Order Create(
            OrderId id, 
            CustomerId customerId, 
            OrderName orderName, 
            Address shippingAddress, 
            Address billingAddress, 
            Payment payment)
        {
            var order = new Order
            {
                Id = id,
                CustomerId = customerId,
                OrderName = orderName,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                Payment = payment
            };

            order.AddDomainEvent(new OrderCreatedEvent(order));

            return order;
        }

        public void Update(
            OrderName orderName,
            Address shippingAddress,
            Address billingAddress,
            Payment payment,
            OrderStatus status)
        {
            OrderName = orderName;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            Payment = payment;
            OrderStatus = status;

            AddDomainEvent(new OrderUpdatedEvent(this));
        }

        public void Add(ProductId productId, int quantity, decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var orderItem = new OrderItem(Id, productId, quantity, price);
            _orderItems.Add(orderItem);
        }

        public void Remove(ProductId productId)
        {
            var orderItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);

            if (orderItem is not null)
            {
                _orderItems.Remove(orderItem);
            }
        }
    }
}
