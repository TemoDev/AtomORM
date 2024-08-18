using System.Reflection;

namespace AtomORM.Core;

/// <summary>
///     A <see cref="AtomEntity{TEntity}" /> can be used to query and save instances of <typeparamref name="TEntity" />.
/// </summary>
//     LINQ queries against a <see cref="AtomEntity{TEntity}" /> will be translated into queries against the database.
public class AtomEntity<TEntity> where TEntity : class
{
    private EntityState ModificationState = EntityState.Unchanged;
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
}