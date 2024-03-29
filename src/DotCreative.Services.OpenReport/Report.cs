﻿using DotLiquid;
using iText.Html2pdf;
using System.IO;

namespace DotCreative.Services.OpenReport
{
  /// <summary>
  /// Gera relatórios em formato PDF a partir de códigos HTML.
  /// </summary>
  public class Report
  {
    /// <summary>
    /// Conteúdo HTML a ser renderizado para geração do PDF.
    /// </summary>
    public string Content { get; private set; }

    /// <summary>
    /// Conteúdo dinâmico a ser interpolado com o HTML
    /// </summary>
    public object Data { get; private set; }

    /// <summary>
    /// Cria uma instância do objeto Report passando o conteúdo HTML
    /// e o conteúdo de um objeto para ser interpolado.
    /// </summary>
    public Report(string content, object dynamicData = null)
    {
      Content = content;
      Data = dynamicData;
    }

    /// <summary>
    /// Renderiza o conteúdo HTML. Se indicado o conteúdo dinâmico, este
    /// será interpolado com o HTML.
    /// </summary>
    public string Render()
    {
      Template template = Template.Parse(Content);

      return template.Render(Hash.FromAnonymousObject(Data));
    }

    /// <summary>
    /// Gera um arquivo PDF. Se especificado o filename, o mesmo é salvo localmente.
    /// O segundo parâmetro, determina se o arquivo deverá ser apagado imediatamente
    /// após a geração do PDF.
    /// </summary>
    public byte[] Generate(string filename = "output.pdf", bool mustDelete = true)
    {
      string content = Render();

      HtmlConverter.ConvertToPdf(content, File.Open(filename, FileMode.Create));

      var bytes = File.ReadAllBytes(filename);


      if (mustDelete) File.Delete(filename);

      return bytes;
    }
  }

}