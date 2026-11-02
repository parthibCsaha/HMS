namespace HMS.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    public Guid? DeletedBy { get; set; }

    /// <summary>
    /// Optimistic concurrency token. PostgreSQL uses xmin (configured via EF),
    /// which is handled transparently — no explicit column needed.
    /// </summary>
    public uint Version { get; set; }
}
