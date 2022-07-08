using DotCreative.Services.OpenReport.Enums;
using DotLiquid;
using iText.Html2pdf;

namespace DotCreative.Services.OpenReport;

public class Report
{
  public EPageSize PageSize { get; private set; }

  public EPageOrientation PageOrientation { get; private set; }

  public string FileName { get; private set; }

  public string Content { get; private set; }

  public object? Data { get; private set; }

  public Report(string content, object? dynamicData = null)
  {
    Content = content;
    Data = dynamicData;
  }

  public string Render()
  {
    Template template = Template.Parse(Content);

    return template.Render(Hash.FromAnonymousObject(Data));
  }

  public async Task<byte[]> Generate(string fileName = "output.pdf", bool mustDelete = true)
  {
    string content = Render();

    HtmlConverter.ConvertToPdf(content, File.Open(fileName, FileMode.Create));

    string file = Path.GetFileName(fileName);
    var bytes = await File.ReadAllBytesAsync(file);


    if (mustDelete) File.Delete(file);

    return bytes;
  }
}
