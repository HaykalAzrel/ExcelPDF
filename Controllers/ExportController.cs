using ExcelPDF.Data;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using Rotativa.AspNetCore;

namespace ExcelPDF.Controllers;

public class ExportController : Controller
{
    private readonly IReportRepository _repo;

    public ExportController(IReportRepository repo)
    {
        _repo = repo;
    }

    public IActionResult Index()
    {
        var data = _repo.GetAllReports();
        return View("Report", data);
    }

    public IActionResult DownloadPdf()
    {
        var data = _repo.GetAllReports();
        return new ViewAsPdf("Report", data)
        {
            FileName = "Laporan-PDF.pdf"
        };
    }

    public IActionResult DownloadExcel()
    {
        var data = _repo.GetAllReports();
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Laporan");

        worksheet.Cell(1, 1).Value = "Nama";
        worksheet.Cell(1, 2).Value = "Nilai";

        for (int i = 0; i < data.Count; i++)
        {
            worksheet.Cell(i + 2, 1).Value = data[i].Name;
            worksheet.Cell(i + 2, 2).Value = data[i].Value;
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;

        return File(stream.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "Laporan-Excel.xlsx");
    }
}
