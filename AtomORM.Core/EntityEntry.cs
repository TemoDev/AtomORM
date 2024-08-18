namespace AtomORM.Core;

public class EntityEntry
{
    public object Entity { get; set; }
    public EntityState State { get; set; }

    public EntityEntry(object entity, EntityState state)
    {
        Entity = entity;
        State = state;
    }
}