using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;

namespace AtomORM.Core;

public class DatabaseService(string connectionString)
{
    private string ConnectionString { get; set; } = connectionString;
    
    /*
     * TODO:
     * Implement methods below to access database and modify data.
     */
    
    
    public void AddData(object TEntity)
    {
        // This code is for future reference it does not work 
        // using (var connection = new SqlConnection(_connectionString))
        // {
        //     var query = "INSERT INTO Employees (Id, Name, Age, Position) VALUES (@Id, @Name, @Age, @Position)";
        //
        //     using (var command = new SqlCommand(query, connection))
        //     {
        //         command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = employee.Id;
        //         command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = employee.Name;
        //         command.Parameters.Add("@Age", SqlDbType.Int).Value = employee.Age;
        //         command.Parameters.Add("@Position", SqlDbType.NVarChar).Value = employee.Position;
        //
        //         connection.Open();
        //         command.ExecuteNonQuery();
        //     }
        // }
    }

    public void UpdateData(object TEntity)
    {
        throw new NotImplementedException();
    }
    
    public void DeleteData(object TEntity)
    {
        throw new NotImplementedException();
    }
    
}