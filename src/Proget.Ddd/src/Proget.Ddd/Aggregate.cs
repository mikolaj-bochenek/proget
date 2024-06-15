namespace Proget.Ddd;

public abstract class Aggregate<T> : Entity<T>, IAggregate<T> where T : struct
{
    private bool _isVersionIncremented = false;
    private readonly List<IDomainEvent> _domainEvents = [];
    public IEnumerable<IDomainEvent> DomainEvents => _domainEvents;

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        IncrementVersion();
        _domainEvents.Add(domainEvent);
    }

    public void ResetVersion()
    {
        Version = 0;
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    private void IncrementVersion()
    {
        if (_isVersionIncremented)
        {
            return;
        }

        Version++;
        _isVersionIncremented = true;
    }
}
