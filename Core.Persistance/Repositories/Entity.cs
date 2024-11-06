namespace Core.Persistance.Repositories;

public abstract class Entity<TId> : IEntity<TId>, IEntityTimestamps
{
    public TId Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? IsActive { get; set; }
    public bool? IsDelete { get; set; }
    public short OrderNo { get; set; }

    public Entity()
    {
        Id = default(TId);
    }

    public Entity(TId id)
    {
        Id = id;
    }
}