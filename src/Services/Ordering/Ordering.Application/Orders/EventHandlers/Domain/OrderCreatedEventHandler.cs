using Microsoft.Extensions.Logging;

namespace Ordering.Application.Orders.EventHandlers.Domain;

//Whenever an OrderCreatedEvent is published, this handler will be invoked
//It is created when an order is created which is implemented in the Ordering.Domain.Models.Order class
//the Domain Event is dispatched from Ordering.Infrastructure.Data.Interceptors.DispatchDomainEventsInterceptor
public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled :{DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
