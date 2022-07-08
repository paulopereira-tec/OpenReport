![Nuget](./src/assets/report.png =150px)


![OpenReport](src/assets/report.png)
# OpenReport
O projeto **Open Report** nasceu a partir da necessidade de se criar relatórios usando códigos html simples sem a necessidade de utilizar ferramentas complexas como Stimulsoft, ReportViewer e outras mais que, embora extremamente úteis, são complexas demais para objetivos mais simples.

O uso do **Open Report** é simples. Basta instanciar a classe principal indicando o arquivo html. É possível ainda realizar a interpolação de dados com um objeto. O **Open Report** utiliza o projeto DotLiquid que faz a interpolação de dados utilizando *double mustashe*.

## Sobre utilização do DotLiquid
Para instruções de uso sobre a biblioteca DotLiquid, acesse http://dotliquidmarkup.org/docs ou https://shopify.github.io/liquid/tags/control-flow/ (Biblioteca base em Ruby - O princípio será o mesmo).

## Utilização
```
// Crie um código HTML para ser utilizado.
string html = @"
<h1>Exemplo de utilização da classe Report</h1>
<p><b>Nome</b> {{ Name }}</p>
<p><b>Telefone</b> {{ Telephone }}</p>
<p><b>Endereço</b> {{ Address }}</p>
<p><b>Sexo</b> {% if (Gender == 'F') %} Feminino {% else %} Masculino {% endif %}</p>
";

// Crie ou instancie um objeto para interpolação
var person = new {
    Name = "Fulano de Tal",
    Telephone = "(xx) xxx-xxx-xxx",
    Address = "Rua X - Bairro Y - Cidade Z",
    Gender = "F"
}

// Instancie um objeto da classe Report. O segundo parâmetro é opcional.
Report report = new(html, person);
```

É possível gerar o PDF de duas formas:
- (1) recuperando um array de bytes para ser utilizado como bem entender
- (2) salvando-o num diretório desejado.

Em ambos os casos, a classe geradora do PDF irá retornar o array de bytes. Veja abaixo as duas implementações:

```
// recupera o array de bytes
bytes[] content = report.Generate();

/**
 * Salva o PDF no local desejado. O segundo parâmetro deterimina se o arquivo será apagado imediatamente após a
 * geração do PDF. O padrão é `true`.
 */
report.Generate("diretorio/arquivo.pdf", false);
```