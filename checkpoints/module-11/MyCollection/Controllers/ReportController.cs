using IronPdf;
using Microsoft.AspNetCore.Mvc;

namespace MyCollection.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    [HttpGet("download")]
    public IActionResult DownloadReport()
    {
        var renderer = new ChromePdfRenderer();

        renderer.RenderingOptions.MarginTop = 20;
        renderer.RenderingOptions.MarginBottom = 20;
        renderer.RenderingOptions.MarginLeft = 15;
        renderer.RenderingOptions.MarginRight = 15;
        renderer.RenderingOptions.PrintHtmlBackgrounds = true;
        renderer.RenderingOptions.Title = "My Collection Report";

        var pdf = renderer.RenderUrlAsPdf("https://localhost:7018/report");

        return File(pdf.BinaryData, "application/pdf", "MyCollectionReport.pdf");
    }
}
