namespace Proget.Ddd;

public interface IAggregate<T> : IAggregate, IEntity<T> where T : struct
{
}

public interface IAggregate : IEntity
{
    IEnumerable<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
    void ResetVersion();
    void AddDomainEvent(IDomainEvent domainEvent);
}
