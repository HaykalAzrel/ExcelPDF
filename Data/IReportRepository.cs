using ExcelPDF.Models;

namespace ExcelPDF.Data;

public interface IReportRepository
{
    List<ReportModel> GetAllReports();
}
