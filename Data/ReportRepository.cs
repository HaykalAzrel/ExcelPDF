using Microsoft.Data.SqlClient;
using System.Data;
using ExcelPDF.Models;
using Microsoft.Extensions.Configuration;

namespace ExcelPDF.Data;

public class ReportRepository : IReportRepository
{
    private readonly string? _connectionString;

    public ReportRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public List<ReportModel> GetAllReports()
    {
        var list = new List<ReportModel>();

        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand("GetReportData", conn)
        {
            CommandType = CommandType.StoredProcedure
        };

        conn.Open();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new ReportModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"]?.ToString() ?? string.Empty,
                Value = Convert.ToInt32(reader["Value"])
            });
        }


        return list;
    }
}
