using System.Data;
using System.Reflection;
using System.Threading.Channels;
using Microsoft.Data.SqlClient;

namespace AtomORM.Core;

public class AtomContext
{
    private readonly string _connectionString;
    private IDbConnection Connection { get; set; }
    protected AtomContext(AtomContextOptions options)
    {
        if (string.IsNullOrEmpty(options.ConnectionString))
        {
            throw new ArgumentException("Invalid connection string format");
        }
        _connectionString = options.ConnectionString;
        Connection = new SqlConnection(_connectionString);
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
            var loadEntityMethod = p.GetType().GetMethod("LoadEntity");
            var result = loadEntityMethod!.Invoke(p, null);

            if (result is Dictionary<string, string> entityProperties)
            {
                foreach (var property in entityProperties)
                {
                    Console.WriteLine($"Property Name: {property.Key}, Property Type Mapped: {MapCSharpTypeToSqlType(Type.GetType(property.Value))}");
                }
            }
            else
            {
                Console.WriteLine("No properties found or entity returned null.");
            }
        }

    }

    private List<object> CreateAtomEntityInstances()
    {
        var properties = GetAtomEntityProperties();
        List<object> instances = new();
        
        foreach (var p in properties)
        {
            var genericType = p.PropertyType.GetGenericArguments()[0];
            var atomEntityType = typeof(AtomEntity<>).MakeGenericType(genericType);
            var atomEntityInstance = Activator.CreateInstance(atomEntityType);
            p.SetValue(this, atomEntityInstance);
            instances.Add(atomEntityInstance!);
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
     
    // Manage database connection
    public void OpenConnection()
    {
        if (Connection.State != ConnectionState.Open)
        {
            Connection.Open();
        }
    }

    public void CloseConnection()
    {
        if (Connection.State != ConnectionState.Closed)
        {
            Connection.Close();
        }
    }
    
    
    private static string MapCSharpTypeToSqlType(Type csharpType)
    {
        var typeMap = new Dictionary<Type, string>
        {
            { typeof(int), "INT" },
            { typeof(long), "BIGINT" },
            { typeof(short), "SMALLINT" },
            { typeof(byte), "TINYINT" },
            { typeof(bool), "BIT" },
            { typeof(char), "CHAR(1)" },
            { typeof(string), "NVARCHAR(MAX)" },
            { typeof(decimal), "DECIMAL(18, 2)" },
            { typeof(double), "FLOAT" },
            { typeof(float), "REAL" },
            { typeof(DateTime), "DATETIME" },
            { typeof(Guid), "UNIQUEIDENTIFIER" },
            { typeof(byte[]), "VARBINARY(MAX)" },
            { typeof(TimeSpan), "TIME" },
            { typeof(DateTimeOffset), "DATETIMEOFFSET" }
        };

        if (typeMap.TryGetValue(csharpType, out var sqlType))
        {
            return sqlType;
        }
    
        throw new ArgumentException($"No SQL Server type mapping found for C# type {csharpType.Name}");
    }

}















