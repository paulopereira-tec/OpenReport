using DotCreative.Services.OpenReport.Tests.Models;

namespace DotCreative.Services.OpenReport.Tests;

[TestClass]
public class ReportTest
{
  /// <summary>
  /// Este teste12313 indica que deve-se esperar sucesso ao renderizar um código HTML.
  /// </summary>
  [TestMethod]
  public void MustBeSucces_RenderHtml()
  {
    try
    {
      // Arrange
      Person person = new Person
      {
        Name = "José da Silva",
        Address = "Avenida Rio Branco, 123 - Centro - Rio de Janeiro, RJ",
        Age = 25
      };

      string html = @"
<h1>Exemplo de renderização de html</h1>
<p><strong>Nome:</strong> {{ Name }} </p>
<p><strong>Endereço:</strong> {{ Address }}</p>
<p><strong>Idade:</strong> {{ Age }}</p>
";

      // Act
      Report report = new Report(html, person);
      report.Render();

      // Assert
      Assert.IsTrue(true);
    }
    catch (Exception ex)
    {
      Assert.Fail(ex.Message);
    }
  }

  /// <summary>
  /// Este teste deve criar com sucesso um arquivo PDF no local indicado.
  /// </summary>
  [TestMethod]
  public void MustBe_CreateAPdfFile()
  {
    try
    {
      Task.Run(async () =>
      {
        // Arrange
        Person person = new Person
        {
          Name = "José da Silva",
          Address = "Avenida Rio Branco, 123 - Centro - Rio de Janeiro, RJ",
          Age = 25
        };

        string html = @"
<h1>Exemplo de renderização de html</h1>
<p><strong>Nome:</strong> {{ Name }} </p>
<p><strong>Endereço:</strong> {{ Address }}</p>
<p><strong>Idade:</strong> {{ Age }}</p>
";

        string nomeArquivo = @"C:\Users\Paulo\Downloads\ArquivoDeTesteGerado.pdf";

        // Act
        Report report = new Report(html, person);
        report.Generate(nomeArquivo, false);

        bool existsAFile = File.Exists(nomeArquivo);

        //File.Delete(nomeArquivo);

        // Assert
        Assert.IsTrue(existsAFile);
      }).GetAwaiter()
        .GetResult();
    }
    catch (Exception ex)
    {
      Assert.Fail(ex.Message);
    }
  }

  /// <summary>
  /// este teste deve retornar com sucesso um array de bytes de um arquivo html
  /// </summary>
  [TestMethod]
  public void MustBeSuccess_ReturnBytesOfPdfFile()
  {
    try
    {
      Task.Run(async () =>
      {
        // Arrange
        Person person = new Person
        {
          Name = "José da Silva",
          Address = "Avenida Rio Branco, 123 - Centro - Rio de Janeiro, RJ",
          Age = 25
        };

        string html = @"
<h1>Exemplo de renderização de html</h1>
<p><strong>Nome:</strong> {{ Name }} </p>
<p><strong>Endereço:</strong> {{ Address }}</p>
<p><strong>Idade:</strong> {{ Age }}</p>
";

        // Act
        Report report = new Report(html, person);
        var bytesOfPdf = report.Generate();

        // Assert
        Assert.IsTrue(bytesOfPdf.GetType() == typeof(byte[]));
      }).GetAwaiter()
        .GetResult();
    }
    catch (Exception ex)
    {
      Assert.Fail(ex.Message);
    }
  }
}