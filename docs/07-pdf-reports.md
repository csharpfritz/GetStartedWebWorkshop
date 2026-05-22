# Module 7: PDF Reports with IronPDF

[← Previous Module](06-entity-framework.md) | [Next Module →](08-aspire.md) | [Back to README](../README.md)

In this module, you'll add a **PDF report page** to `MyCollection` using **IronPDF**. The page displays your collection items in a clean grid layout, and IronPDF renders that page directly into a downloadable PDF report.

This approach is powerful because you design the report as a normal Blazor page — complete with CSS styling — and IronPDF's Chrome-based rendering engine converts it pixel-perfect into a PDF document.

> **Sponsor note:** This module is sponsored by [Iron Software](https://www.ironsoftware.com/), makers of IronPDF.

By the end of this module, your app will include:

- A new Blazor page that displays collection items in a printable grid
- An IronPDF integration that renders the page as a PDF
- A download button that generates and delivers the PDF to the user's browser

---

## 1. Why Generate PDFs from Blazor?

Many apps need to produce reports, invoices, or summaries as PDF files. Traditionally this meant using a separate PDF layout engine and duplicating your UI logic. With IronPDF, you can reuse your existing Blazor pages and CSS — design the report in HTML, preview it in the browser, and render it to PDF with a few lines of code.

IronPDF uses a **Chrome-based rendering engine** internally, so the PDF output matches exactly what you see in the browser. This means:

- CSS Grid and Flexbox work as expected
- Images render correctly
- Fonts and colors are preserved
- You can iterate on the design using normal browser dev tools

---

## 2. Install the IronPDF NuGet Package

Let's get IronPDF into the project so the rest of this module has something real to build on.

Open a terminal in the `MyCollection` project directory and run:

```bash
dotnet add package IronPdf
```

This adds the IronPDF library to your Blazor Server project.

---

## 3. Create the Report Page

Now let's create the page that will become the report. I like starting here because once the HTML looks right in the browser, the PDF part gets much easier.

Create a new file at `Components/Pages/Report.razor`:

```razor
@page "/report"
@using Microsoft.EntityFrameworkCore
@inject IDbContextFactory<MyCollectionContext> DbFactory

<PageTitle>Collection Report</PageTitle>

<div class="report-container">
    <h1>My Collection Report</h1>
    <p class="report-date">Generated: @DateTime.Now.ToString("MMMM dd, yyyy")</p>

    @if (items is null)
    {
        <p>Loading...</p>
    }
    else if (items.Count == 0)
    {
        <p>No items in your collection yet.</p>
    }
    else
    {
        <table class="report-table">
            <thead>
                <tr>
                    <th>Photo</th>
                    <th>Name</th>
                    <th>Description</th>
                    <th>Date Added</th>
                </tr>
            </thead>
            <tbody>
                @foreach (var item in items)
                {
                    <tr>
                        <td>
                            <img src="@GetPhotoUrl(item)" alt="@item.Name" class="report-thumbnail" />
                        </td>
                        <td>@item.Name</td>
                        <td>@item.Description</td>
                        <td>@item.DateAdded.ToString("yyyy-MM-dd")</td>
                    </tr>
                }
            </tbody>
        </table>

        <div class="report-summary">
            <p><strong>Total Items:</strong> @items.Count</p>
        </div>
    }
</div>

@code {
    private List<CollectionItem>? items;

    protected override async Task OnInitializedAsync()
    {
        await using var db = await DbFactory.CreateDbContextAsync();
        items = await db.CollectionItems
            .OrderBy(i => i.Name)
            .ToListAsync();
    }

    private string GetPhotoUrl(CollectionItem item)
    {
        if (string.IsNullOrWhiteSpace(item.PhotoFileName))
        {
            return "https://placehold.co/80x80?text=No+Photo";
        }
        return $"/uploads/{item.PhotoFileName}";
    }
}
```

---

## 4. Style the Report Page

A report should look intentional on paper, not like a random web page snapshot. Let's add some print-friendly styling next.

Add the following scoped CSS file at `Components/Pages/Report.razor.css`:

```css
.report-container {
    max-width: 900px;
    margin: 0 auto;
    padding: 2rem;
    font-family: 'Segoe UI', sans-serif;
}

.report-date {
    color: #666;
    font-style: italic;
    margin-bottom: 1.5rem;
}

.report-table {
    width: 100%;
    border-collapse: collapse;
    margin-bottom: 1.5rem;
}

.report-table th,
.report-table td {
    border: 1px solid #ddd;
    padding: 0.75rem;
    text-align: left;
    vertical-align: middle;
}

.report-table th {
    background-color: #f5f5f5;
    font-weight: 600;
}

.report-table tr:nth-child(even) {
    background-color: #fafafa;
}

.report-thumbnail {
    width: 80px;
    height: 80px;
    object-fit: cover;
    border-radius: 4px;
}

.report-summary {
    padding: 1rem;
    background-color: #f0f7ff;
    border-radius: 6px;
    border: 1px solid #cce0ff;
}
```

---

## 5. Add the PDF Export Endpoint

Now let's connect that page to IronPDF. This endpoint will render the report and stream it back as a file download.

Create a new file at `Controllers/ReportController.cs`:

```csharp
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
```

> **Note:** Adjust the URL and port to match your local development environment. If you're using Aspire with Dev Tunnels (Module 7), use the appropriate base URL.

---

## 6. Add a Download Button

With the endpoint in place, the user experience gets a lot simpler. Let's add a download link right on the report page.

Update the `Report.razor` page to include a download link. Add the following just below the closing `</div>` of `report-summary`:

```razor
<div class="report-actions">
    <a href="/api/report/download" class="btn-download" target="_blank">
        📄 Download as PDF
    </a>
</div>
```

And add this to your `Report.razor.css`:

```css
.report-actions {
    margin-top: 1.5rem;
    text-align: center;
}

.btn-download {
    display: inline-block;
    padding: 0.75rem 1.5rem;
    background-color: #0066cc;
    color: white;
    text-decoration: none;
    border-radius: 6px;
    font-weight: 600;
    transition: background-color 0.2s;
}

.btn-download:hover {
    background-color: #0052a3;
}
```

---

## 7. Register the Controller in Program.cs

One easy detail to miss here is that the controller has to be part of the ASP.NET Core pipeline. Let's wire that up before we test.

In your `Program.cs`, ensure controllers are registered. Add these lines if they are not already present:

```csharp
builder.Services.AddControllers();
```

And after `app` is built:

```csharp
app.MapControllers();
```

---

## 8. Test the Report

Now let's run the app and make sure both versions work: the HTML page in the browser and the PDF download. Pretty cool, right?

1. Run your application with `dotnet watch`
2. Navigate to `https://localhost:7018/report`
3. Verify that your collection items appear in the table
4. Click **Download as PDF**
5. Open the downloaded file and confirm the layout matches the browser view

---

## 9. How IronPDF Works Under the Hood

IronPDF uses a **Chrome-based rendering engine** to convert web content into PDF. When you call `RenderUrlAsPdf`, it:

1. Launches an internal Chromium instance
2. Navigates to the URL you specified
3. Waits for the page to fully render (including CSS and JavaScript)
4. Captures the rendered output as a PDF document

This means any valid HTML/CSS that works in Chrome will work in IronPDF. You get pixel-perfect results without needing a separate report designer or template language.

### Key IronPDF features used in this module

| Feature | Description |
|---------|-------------|
| `ChromePdfRenderer` | The main rendering class |
| `RenderUrlAsPdf` | Renders a live URL to PDF |
| `RenderingOptions` | Controls margins, backgrounds, page size |
| `BinaryData` | Gets the PDF as a byte array for download |

### Alternative approaches

IronPDF also supports:

- **`RenderHtmlAsPdf`** — render an HTML string directly (useful for email-style reports)
- **`RenderHtmlFileAsPdf`** — render a local `.html` file
- Custom headers and footers with page numbers
- Password protection and digital signatures
- Merging multiple PDFs into one document

Learn more at [ironpdf.com/technology/blazor-pdf-generator](https://ironpdf.com/technology/blazor-pdf-generator).

---

## 10. Summary

In this module, you:

- Installed the IronPDF NuGet package
- Created a Blazor report page with a styled collection grid
- Built an API endpoint that uses `ChromePdfRenderer` to render the page as a PDF
- Added a download button so users can export their collection as a document

This pattern — design in HTML, render to PDF — is reusable for invoices, shipping labels, inventory reports, and more. IronPDF handles the complexity of PDF generation so you can focus on designing the content.

---

## What's Next?

In the next module, you'll add app orchestration and observability with .NET Aspire.

[Next Module →](08-aspire.md) | [← Back to README](../README.md)
