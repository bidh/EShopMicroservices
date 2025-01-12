using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain;

//Whenever an OrderCreatedEvent is published, this handler will be invoked
//It is created when an order is created which is implemented in the Ordering.Domain.Models.Order class
//the Domain Event is dispatched from Ordering.Infrastructure.Data.Interceptors.DispatchDomainEventsInterceptor
public class OrderCreatedEventHandler(
    IPublishEndpoint publishedEndpoint,
    IFeatureManager featureManager,
    ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled :{DomainEvent}", domainEvent.GetType().Name);

        if(await featureManager.IsEnabledAsync("OrderFullfillment"))
        {
            var orderCreatedIntegrationEvent = domainEvent.order.ToOrderDto();
            await publishedEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
        }        
    }
}
