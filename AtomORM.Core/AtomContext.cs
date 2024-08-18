using System.Reflection;
using System.Threading.Channels;

namespace AtomORM.Core;

public class AtomContext
{
    private readonly string _connectionString;
    protected AtomContext(AtomContextOptions options)
    {
        _connectionString = options.ConnectionString;
    }
    public void GetConnectionString()
    {
        Console.WriteLine(_connectionString);
    }

    public void MapEntities()
    {
        var properties = CreateAtomEntityInstances();
        foreach (var p in properties)
        {
            Console.WriteLine(p.ToString());
        }
        foreach (var p in properties)
        {
            var printPropertiesMethod = p.GetType().GetMethod("LoadEntity");
            printPropertiesMethod.Invoke(p, null);
        }
    }

    public List<object> CreateAtomEntityInstances()
    {
        var properties = GetAtomEntityProperties();
        List<object> instances = new();
        
        foreach (var p in properties)
        {
            Type genericType = p.PropertyType.GetGenericArguments()[0];
            Type atomEntityType = typeof(AtomEntity<>).MakeGenericType(genericType);
            object atomEntityInstance = Activator.CreateInstance(atomEntityType);
            p.SetValue(this, atomEntityInstance);
            instances.Add(atomEntityInstance);
        }

        return instances;
    }

    private List<PropertyInfo> GetAtomEntityProperties()  {
        var properties = GetType().GetProperties();
        List<PropertyInfo> entities = new();
        
        foreach (var p in properties)
        {
            if (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(AtomEntity<>))
            {
                entities.Add(p);
            }
        }

        return entities;
    }
}