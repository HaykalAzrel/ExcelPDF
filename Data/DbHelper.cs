using Microsoft.Data.SqlClient;

namespace ExcelPDF.Data;

public class DbHelper
{
    private string? _connectionString;

    public DbHelper(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public SqlConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }
}
