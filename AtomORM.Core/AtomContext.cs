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
}