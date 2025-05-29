using Microsoft.AspNetCore.Mvc;
using ExcelPDF.Models;
using ClosedXML.Excel;
using Rotativa.AspNetCore;

namespace ExcelPDF.Controllers;

public class ExportController : Controller
{
    public IActionResult Index()
    {
        var data = GetData();
        return View("Report", data);
    }

    public IActionResult DownloadPdf()
    {
        var data = GetData();
        return new ViewAsPdf("Report", data)
        {
            FileName = "Laporan-PDF.pdf"
        };
    }

    public IActionResult DownloadExcel()
    {
        var data = GetData();
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

    private List<ReportModel> GetData() => new()
    {
        new ReportModel { Name = "Item A", Value = 100 },
        new ReportModel { Name = "Item B", Value = 200 },
        new ReportModel { Name = "Item C", Value = 300 }
    };
}
