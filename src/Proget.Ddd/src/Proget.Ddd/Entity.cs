namespace Proget.Ddd;

public abstract class Entity<T> : IEntity<T> where T : struct
{
    public T Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public T? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public T? LastModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
    public long Version { get; set; }
}
