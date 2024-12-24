namespace Ordering.Domain.Abstractions
{
    // We are using MediatR library to implement Domain Events
    // because it is allowing domain events to be dispatched through the mediator handlers.
    // So by this way we can use mediator handlers in order to handle these domain events using the mediator.
    public interface IDomainEvent : INotification
    {
        Guid EvenId => Guid.NewGuid();
        public DateTime OccurredOn => DateTime.UtcNow;
        public string EventType => GetType().AssemblyQualifiedName;
    }
}
