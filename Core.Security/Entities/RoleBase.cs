using Core.Persistance.Repositories;

namespace Core.Security.Entities;

public class RoleBase<TId> : Entity<TId>
{
    public string Name { get; set; }

    public RoleBase()
    {
        Name = string.Empty;
    }

    public RoleBase(string name)
    {
        Name = name;
    }

    public RoleBase(TId id, string name)
        : base(id)
    {
        Name = name;
    }
}
