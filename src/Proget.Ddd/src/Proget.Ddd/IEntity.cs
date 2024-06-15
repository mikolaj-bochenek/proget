namespace Proget.Ddd;

public interface IEntity<T> : IEntity where T : struct
{
    public T Id { get; set; }
    public T? CreatedBy { get; set; }
    public T? LastModifiedBy { get; set; }
}

public interface IEntity : IVersion
{
    public DateTime? CreatedAt { get; set; }
    public DateTime? LastModified { get; set; }
    public bool IsDeleted { get; set; }
}
