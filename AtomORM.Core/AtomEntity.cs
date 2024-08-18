using System.Reflection;
using System.Reflection.Metadata;
using Azure.Identity;

namespace AtomORM.Core;

/// <summary>
///     A <see cref="AtomEntity{TEntity}" /> can be used to query and save instances of <typeparamref name="TEntity" />.
/// </summary>
//     LINQ queries against a <see cref="AtomEntity{TEntity}" /> will be translated into queries against the database.
public class AtomEntity<TEntity> where TEntity : class
{
    private readonly AtomContext _context;
    private readonly List<EntityEntry> _entries;

    public AtomEntity(AtomContext context, List<EntityEntry> entries)
    {
        _context = context;
        _entries = entries;
    }
    public AtomEntity()
    {
        Console.WriteLine("Atom entity created");
    }
    public Dictionary<string, string> LoadEntity()
    {
        var properties = typeof(TEntity).GetProperties();
        if (properties.Length == 0) return null;

        var res = new Dictionary<string, string>();
        
        foreach (var p in properties)
        {
            Console.WriteLine($"{p.Name} is type of {p.PropertyType}");
            res.Add(p.Name, p.PropertyType.ToString());
        }
        return res;
    }

    public void Add(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");

        _entries.Add(new EntityEntry(entity, EntityState.Added));
        // var entityType = typeof(TEntity);
        // var props = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        // foreach (var p in props)
        // {
        //     var propertyName = p.Name;
        //     var propertyType = p.PropertyType;
        //     var propertyValue = p.GetValue(entity);
        //     Console.WriteLine($"{propertyName} (Type: {propertyType}) = {propertyValue}");
        // }
    }

    public void Update(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
        
        var existingEntry = _entries.FirstOrDefault(e => e.Entity.Equals(entity));
        if (existingEntry != null)
        {
            existingEntry.State = EntityState.Modified;
        }
        else
        {
            _entries.Add(new EntityEntry(entity, EntityState.Modified));
        }
    }
    
    public void Delete(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");

        var existingEntry = _entries.FirstOrDefault(e => e.Entity.Equals(entity));
        if (existingEntry != null)
        {
            existingEntry.State = EntityState.Deleted;
        }
        else
        {
            _entries.Add(new EntityEntry(entity, EntityState.Deleted));
        }
    }
}



















