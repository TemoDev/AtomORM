namespace AtomORM.Core;

public class AtomContextOptions
{
    public string ConnectionString { get; set; }

    public void SetConnectionString(string connectionString)
    {
        ConnectionString = connectionString;
        Console.WriteLine("Connection string in DbContextOptions class is set: {0}", connectionString);
    }
}