using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class MarkDown
{
    /// <summary>
    /// Converte um texto Markdown em um documento HTML5 completo.
    /// </summary>
    public string ConverterParaHtml(string markdown)
    {
        ArgumentNullException.ThrowIfNull(markdown);

        var cursor = CursorDeLinhas.DoTexto(markdown);
        var blocos = new LeitorDeBlocos(cursor).LerTodos();
        var corpo = string.Join("\n", blocos);

        return new DocumentoHtml(corpo).Renderizar();
    }
}

internal sealed class DocumentoHtml
{
    private readonly string corpo;

    public DocumentoHtml(string corpo)
    {
        this.corpo = corpo;
    }

    public string Renderizar()
    {
        var titulo = TituloDoDocumento.Extrair(corpo);

        return $"""
            <!DOCTYPE html>
            <html lang="pt-BR">
            <head>
            <meta charset="utf-8">
            <meta name="viewport" content="width=device-width, initial-scale=1">
            <title>{titulo}</title>
            </head>
            <body>
            {corpo}
            </body>
            </html>
            """;
    }
}

internal static class TituloDoDocumento
{
    private const string TituloPadrao = "Documento";

    private static readonly Regex PrimeiroCabecalho =
        new(@"<h[1-6]>(.*?)</h[1-6]>", RegexOptions.Singleline);

    private static readonly Regex MarcacaoHtml = new("<[^>]+>");

    public static string Extrair(string corpoHtml)
    {
        var correspondencia = PrimeiroCabecalho.Match(corpoHtml);
        if (!correspondencia.Success)
            return TituloPadrao;

        var cabecalhoHtml = PadroesMarkdown.Grupo(correspondencia, 1);
        var semMarcacao = MarcacaoHtml.Replace(cabecalhoHtml, string.Empty);
        var titulo = semMarcacao.Trim();

        return titulo.Length == 0 ? TituloPadrao : titulo;
    }
}

internal sealed class CursorDeLinhas
{
    private readonly string[] linhas;
    private int posicao;

    private CursorDeLinhas(string[] linhas)
    {
        this.linhas = linhas;
    }

    public static CursorDeLinhas DoTexto(string texto)
    {
        var normalizado = texto.ReplaceLineEndings("\n");
        var linhas = normalizado.Split('\n');
        return new CursorDeLinhas(linhas);
    }

    public bool TemMais() => posicao < linhas.Length;

    public string LinhaAtual() => linhas[posicao];

    public string ProximaLinha()
    {
        var existeProxima = posicao + 1 < linhas.Length;
        return existeProxima ? linhas[posicao + 1] : string.Empty;
    }

    public void Avancar() => posicao++;

    public void PularLinhasEmBranco()
    {
        while (TemMais() && string.IsNullOrWhiteSpace(LinhaAtual()))
            Avancar();
    }
}

internal sealed class LeitorDeBlocos
{
    private readonly CursorDeLinhas cursor;
    private readonly ConversoresDeBloco conversores;

    public LeitorDeBlocos(CursorDeLinhas cursor)
    {
        this.cursor = cursor;
        conversores = ConversoresDeBloco.Padrao();
    }

    public IReadOnlyList<string> LerTodos()
    {
        var blocos = new List<string>();

        cursor.PularLinhasEmBranco();
        while (cursor.TemMais())
        {
            blocos.Add(LerProximoBloco());
            cursor.PularLinhasEmBranco();
        }

        return blocos;
    }

    private string LerProximoBloco()
    {
        var conversor = conversores.Escolher(cursor);
        return conversor.Converter(cursor);
    }
}

internal interface IConversorDeBloco
{
    bool Reconhece(CursorDeLinhas cursor);

    string Converter(CursorDeLinhas cursor);
}

internal sealed class ConversoresDeBloco
{
    private readonly IReadOnlyList<IConversorDeBloco> conversores;

    private ConversoresDeBloco(IReadOnlyList<IConversorDeBloco> conversores)
    {
        this.conversores = conversores;
    }

    // A ordem importa: o parágrafo é o último, pois aceita qualquer linha.
    public static ConversoresDeBloco Padrao()
    {
        return new ConversoresDeBloco(new IConversorDeBloco[]
        {
            new ConversorDeBlocoDeCodigo(),
            new ConversorDeCabecalho(),
            new ConversorDeRegraHorizontal(),
            new ConversorDeCitacao(),
            new ConversorDeTabela(),
            new ConversorDeLista("ul", PadroesMarkdown.ItemNaoOrdenado),
            new ConversorDeLista("ol", PadroesMarkdown.ItemOrdenado),
            new ConversorDeParagrafo(),
        });
    }

    public IConversorDeBloco Escolher(CursorDeLinhas cursor)
    {
        return conversores.First(conversor => conversor.Reconhece(cursor));
    }
}

internal sealed class ConversorDeBlocoDeCodigo : IConversorDeBloco
{
    public bool Reconhece(CursorDeLinhas cursor)
    {
        var linha = cursor.LinhaAtual();
        return PadroesMarkdown.AberturaDeCerca.IsMatch(linha);
    }

    public string Converter(CursorDeLinhas cursor)
    {
        var linguagem = LerLinguagem(cursor.LinhaAtual());
        cursor.Avancar();

        var codigo = LerCodigo(cursor);
        cursor.Avancar();

        return MontarHtml(linguagem, codigo);
    }

    private static string LerLinguagem(string linhaDeAbertura)
    {
        return PadroesMarkdown.Grupo(PadroesMarkdown.AberturaDeCerca, linhaDeAbertura, 1);
    }

    private static string LerCodigo(CursorDeLinhas cursor)
    {
        var linhas = new List<string>();

        while (cursor.TemMais() && !EstaNoFechamento(cursor))
        {
            var linha = cursor.LinhaAtual();
            linhas.Add(TextoHtml.Escapar(linha));
            cursor.Avancar();
        }

        return string.Join("\n", linhas);
    }

    private static bool EstaNoFechamento(CursorDeLinhas cursor)
    {
        var linha = cursor.LinhaAtual();
        return PadroesMarkdown.FechamentoDeCerca.IsMatch(linha);
    }

    private static string MontarHtml(string linguagem, string codigo)
    {
        var linguagemEscapada = TextoHtml.Escapar(linguagem);
        var atributo = linguagem.Length == 0
            ? string.Empty
            : $" class=\"language-{linguagemEscapada}\"";

        return $"<pre><code{atributo}>{codigo}</code></pre>";
    }
}

internal sealed class ConversorDeCabecalho : IConversorDeBloco
{
    public bool Reconhece(CursorDeLinhas cursor)
    {
        var linha = cursor.LinhaAtual();
        return PadroesMarkdown.Cabecalho.IsMatch(linha);
    }

    public string Converter(CursorDeLinhas cursor)
    {
        var linha = cursor.LinhaAtual();
        cursor.Avancar();

        var marcadores = PadroesMarkdown.Grupo(PadroesMarkdown.Cabecalho, linha, 1);
        var texto = PadroesMarkdown.Grupo(PadroesMarkdown.Cabecalho, linha, 2);
        var nivel = marcadores.Length;
        var conteudo = TextoInline.Converter(texto);

        return $"<h{nivel}>{conteudo}</h{nivel}>";
    }
}

internal sealed class ConversorDeRegraHorizontal : IConversorDeBloco
{
    public bool Reconhece(CursorDeLinhas cursor)
    {
        var linha = cursor.LinhaAtual();
        return PadroesMarkdown.RegraHorizontal.IsMatch(linha);
    }

    public string Converter(CursorDeLinhas cursor)
    {
        cursor.Avancar();
        return "<hr>";
    }
}

internal sealed class ConversorDeCitacao : IConversorDeBloco
{
    public bool Reconhece(CursorDeLinhas cursor)
    {
        var linha = cursor.LinhaAtual();
        return PadroesMarkdown.Citacao.IsMatch(linha);
    }

    public string Converter(CursorDeLinhas cursor)
    {
        var markdownInterno = LerMarkdownDaCitacao(cursor);
        var cursorInterno = CursorDeLinhas.DoTexto(markdownInterno);
        var blocos = new LeitorDeBlocos(cursorInterno).LerTodos();
        var conteudo = string.Join("\n", blocos);

        return $"<blockquote>\n{conteudo}\n</blockquote>";
    }

    private static string LerMarkdownDaCitacao(CursorDeLinhas cursor)
    {
        var linhas = new List<string>();

        while (cursor.TemMais() && PadroesMarkdown.Citacao.IsMatch(cursor.LinhaAtual()))
        {
            var linha = cursor.LinhaAtual();
            linhas.Add(RemoverMarcador(linha));
            cursor.Avancar();
        }

        return string.Join("\n", linhas);
    }

    private static string RemoverMarcador(string linha)
    {
        return PadroesMarkdown.Citacao.Replace(linha, string.Empty, 1);
    }
}

internal sealed class ConversorDeLista : IConversorDeBloco
{
    private readonly string tag;
    private readonly Regex padraoDoItem;

    public ConversorDeLista(string tag, Regex padraoDoItem)
    {
        this.tag = tag;
        this.padraoDoItem = padraoDoItem;
    }

    public bool Reconhece(CursorDeLinhas cursor)
    {
        var linha = cursor.LinhaAtual();
        return EhItem(linha);
    }

    public string Converter(CursorDeLinhas cursor)
    {
        var itens = LerItens(cursor);
        var conteudo = string.Join("\n", itens);

        return $"<{tag}>\n{conteudo}\n</{tag}>";
    }

    private IReadOnlyList<string> LerItens(CursorDeLinhas cursor)
    {
        var itens = new List<string>();

        while (cursor.TemMais() && EhItem(cursor.LinhaAtual()))
        {
            var linha = cursor.LinhaAtual();
            itens.Add(ConverterItem(linha));
            cursor.Avancar();
        }

        return itens;
    }

    private bool EhItem(string linha)
    {
        var correspondeAoPadrao = padraoDoItem.IsMatch(linha);
        var ehRegraHorizontal = PadroesMarkdown.RegraHorizontal.IsMatch(linha);

        return correspondeAoPadrao && !ehRegraHorizontal;
    }

    private string ConverterItem(string linha)
    {
        var texto = PadroesMarkdown.Grupo(padraoDoItem, linha, 1);
        var textoAparado = texto.Trim();
        var conteudo = TextoInline.Converter(textoAparado);

        return $"<li>{conteudo}</li>";
    }
}

internal sealed class ConversorDeTabela : IConversorDeBloco
{
    public bool Reconhece(CursorDeLinhas cursor)
    {
        var linhaDeCabecalho = cursor.LinhaAtual();
        var linhaDeDelimitador = cursor.ProximaLinha();

        return LinhaDeTabela.IniciaTabela(linhaDeCabecalho, linhaDeDelimitador);
    }

    public string Converter(CursorDeLinhas cursor)
    {
        var cabecalho = LerLinha(cursor);
        var delimitador = LerLinha(cursor);
        var alinhamentos = AlinhamentosDeColuna.Ler(delimitador);
        var linhasDoCorpo = LerLinhasDoCorpo(cursor);

        var secaoDoCabecalho = MontarSecaoDoCabecalho(cabecalho, alinhamentos);
        var secaoDoCorpo = MontarSecaoDoCorpo(linhasDoCorpo, alinhamentos);

        return $"<table>\n{secaoDoCabecalho}{secaoDoCorpo}</table>";
    }

    private static LinhaDeTabela LerLinha(CursorDeLinhas cursor)
    {
        var linha = cursor.LinhaAtual();
        cursor.Avancar();

        return LinhaDeTabela.Ler(linha);
    }

    private static IReadOnlyList<LinhaDeTabela> LerLinhasDoCorpo(CursorDeLinhas cursor)
    {
        var linhas = new List<LinhaDeTabela>();

        while (cursor.TemMais() && EhLinhaDoCorpo(cursor.LinhaAtual()))
        {
            linhas.Add(LerLinha(cursor));
        }

        return linhas;
    }

    private static bool EhLinhaDoCorpo(string linha)
    {
        var estaEmBranco = string.IsNullOrWhiteSpace(linha);
        var iniciaOutroBloco = PadroesMarkdown.IniciaBlocoEspecial(linha);

        return !estaEmBranco && !iniciaOutroBloco;
    }

    private static string MontarSecaoDoCabecalho(
        LinhaDeTabela cabecalho,
        AlinhamentosDeColuna alinhamentos)
    {
        var linha = MontarLinha(cabecalho, "th", alinhamentos);

        return $"<thead>\n{linha}\n</thead>\n";
    }

    private static string MontarSecaoDoCorpo(
        IReadOnlyList<LinhaDeTabela> linhasDoCorpo,
        AlinhamentosDeColuna alinhamentos)
    {
        if (linhasDoCorpo.Count == 0)
            return string.Empty;

        var linhas = new List<string>();
        foreach (var linhaDoCorpo in linhasDoCorpo)
        {
            linhas.Add(MontarLinha(linhaDoCorpo, "td", alinhamentos));
        }

        var conteudo = string.Join("\n", linhas);

        return $"<tbody>\n{conteudo}\n</tbody>\n";
    }

    private static string MontarLinha(
        LinhaDeTabela linha,
        string tagDaCelula,
        AlinhamentosDeColuna alinhamentos)
    {
        var celulas = new List<string>();

        for (var coluna = 0; coluna < alinhamentos.QuantidadeDeColunas(); coluna++)
        {
            var texto = linha.Celula(coluna);
            var atributo = alinhamentos.Atributo(coluna);
            celulas.Add(MontarCelula(texto, tagDaCelula, atributo));
        }

        var conteudo = string.Join("\n", celulas);

        return $"<tr>\n{conteudo}\n</tr>";
    }

    private static string MontarCelula(string texto, string tag, string atributo)
    {
        var conteudo = TextoInline.Converter(texto);

        return $"<{tag}{atributo}>{conteudo}</{tag}>";
    }
}

internal sealed class AlinhamentosDeColuna
{
    private readonly IReadOnlyList<string> atributos;

    private AlinhamentosDeColuna(IReadOnlyList<string> atributos)
    {
        this.atributos = atributos;
    }

    public static AlinhamentosDeColuna Ler(LinhaDeTabela delimitador)
    {
        var atributos = new List<string>();

        for (var coluna = 0; coluna < delimitador.QuantidadeDeCelulas(); coluna++)
        {
            var celula = delimitador.Celula(coluna);
            atributos.Add(AtributoDe(celula));
        }

        return new AlinhamentosDeColuna(atributos);
    }

    public int QuantidadeDeColunas() => atributos.Count;

    public string Atributo(int coluna)
    {
        var existe = coluna < atributos.Count;
        return existe ? atributos[coluna] : string.Empty;
    }

    private static string AtributoDe(string celulaDoDelimitador)
    {
        var comecaComDoisPontos = celulaDoDelimitador.StartsWith(':');
        var terminaComDoisPontos = celulaDoDelimitador.EndsWith(':');

        if (comecaComDoisPontos && terminaComDoisPontos)
            return Estilo("center");

        if (terminaComDoisPontos)
            return Estilo("right");

        if (comecaComDoisPontos)
            return Estilo("left");

        return string.Empty;
    }

    private static string Estilo(string alinhamento)
    {
        return $" style=\"text-align: {alinhamento}\"";
    }
}

internal sealed class LinhaDeTabela
{
    private static readonly Regex SeparadorDeCelula = new(@"(?<!\\)\|");

    private static readonly Regex Delimitador =
        new(@"^\s{0,3}\|?\s*:?-+:?\s*(\|\s*:?-+:?\s*)*\|?\s*$");

    private readonly IReadOnlyList<string> celulas;

    private LinhaDeTabela(IReadOnlyList<string> celulas)
    {
        this.celulas = celulas;
    }

    public static LinhaDeTabela Ler(string linha)
    {
        var linhaAparada = linha.Trim();
        var semBordas = RemoverBordas(linhaAparada);
        var partes = SeparadorDeCelula.Split(semBordas);
        var celulasLimpas = partes.Select(LimparCelula);

        return new LinhaDeTabela(celulasLimpas.ToList());
    }

    public static bool IniciaTabela(string linhaDeCabecalho, string linhaDeDelimitador)
    {
        var cabecalhoTemBarra = linhaDeCabecalho.Contains('|');
        var ehDelimitador = EhDelimitador(linhaDeDelimitador);

        if (!cabecalhoTemBarra || !ehDelimitador)
            return false;

        var cabecalho = Ler(linhaDeCabecalho);
        var delimitador = Ler(linhaDeDelimitador);

        return cabecalho.QuantidadeDeCelulas() == delimitador.QuantidadeDeCelulas();
    }

    public int QuantidadeDeCelulas() => celulas.Count;

    public string Celula(int indice)
    {
        var existe = indice < celulas.Count;
        return existe ? celulas[indice] : string.Empty;
    }

    private static bool EhDelimitador(string linha)
    {
        var temBarra = linha.Contains('|');
        var correspondeAoPadrao = Delimitador.IsMatch(linha);

        return temBarra && correspondeAoPadrao;
    }

    private static string RemoverBordas(string linha)
    {
        var semInicio = linha.StartsWith('|') ? linha[1..] : linha;
        var terminaComBarraReal = semInicio.EndsWith('|') && !semInicio.EndsWith("\\|");

        return terminaComBarraReal ? semInicio[..^1] : semInicio;
    }

    private static string LimparCelula(string celula)
    {
        var comBarrasRestauradas = celula.Replace("\\|", "|");

        return comBarrasRestauradas.Trim();
    }
}

internal sealed class ConversorDeParagrafo : IConversorDeBloco
{
    public bool Reconhece(CursorDeLinhas cursor) => true;

    public string Converter(CursorDeLinhas cursor)
    {
        var linhas = LerLinhasDoParagrafo(cursor);
        var texto = string.Join("\n", linhas);
        var conteudo = TextoInline.Converter(texto);

        return $"<p>{conteudo}</p>";
    }

    private static IReadOnlyList<string> LerLinhasDoParagrafo(CursorDeLinhas cursor)
    {
        var linhas = new List<string>();

        do
        {
            var linha = cursor.LinhaAtual();
            var linhaAparada = linha.Trim();
            linhas.Add(linhaAparada);
            cursor.Avancar();
        }
        while (ContinuaParagrafo(cursor));

        return linhas;
    }

    private static bool ContinuaParagrafo(CursorDeLinhas cursor)
    {
        if (!cursor.TemMais())
            return false;

        var linha = cursor.LinhaAtual();
        var proximaLinha = cursor.ProximaLinha();
        var estaEmBranco = string.IsNullOrWhiteSpace(linha);
        var iniciaOutroBloco = PadroesMarkdown.IniciaBlocoEspecial(linha);
        var iniciaTabela = LinhaDeTabela.IniciaTabela(linha, proximaLinha);

        return !estaEmBranco && !iniciaOutroBloco && !iniciaTabela;
    }
}

internal sealed class TextoInline
{
    private readonly TrechosProtegidos trechos = new();

    public static string Converter(string markdown)
    {
        return new TextoInline().Processar(markdown);
    }

    // Trechos já convertidos (código, links, imagens) são trocados por
    // marcadores para que as etapas seguintes não mexam no HTML gerado.
    private string Processar(string markdown)
    {
        var texto = ProtegerCodigoInline(markdown);
        texto = TextoHtml.Escapar(texto);
        texto = ProtegerImagens(texto);
        texto = ProtegerLinks(texto);
        texto = AplicarEnfase(texto);

        return trechos.Restaurar(texto);
    }

    private string ProtegerCodigoInline(string texto)
    {
        return PadroesInline.CodigoInline.Replace(texto, ProtegerCodigo);
    }

    private string ProtegerCodigo(Match correspondencia)
    {
        var codigo = PadroesMarkdown.Grupo(correspondencia, 1);
        var codigoEscapado = TextoHtml.Escapar(codigo);

        return trechos.Proteger($"<code>{codigoEscapado}</code>");
    }

    private string ProtegerImagens(string texto)
    {
        return PadroesInline.Imagem.Replace(texto, ProtegerImagem);
    }

    private string ProtegerImagem(Match correspondencia)
    {
        var textoAlternativo = PadroesMarkdown.Grupo(correspondencia, 1);
        var endereco = PadroesMarkdown.Grupo(correspondencia, 2);

        return trechos.Proteger($"<img src=\"{endereco}\" alt=\"{textoAlternativo}\">");
    }

    private string ProtegerLinks(string texto)
    {
        return PadroesInline.Link.Replace(texto, ProtegerLink);
    }

    private string ProtegerLink(Match correspondencia)
    {
        var texto = PadroesMarkdown.Grupo(correspondencia, 1);
        var endereco = PadroesMarkdown.Grupo(correspondencia, 2);
        var textoComEnfase = AplicarEnfase(texto);

        return trechos.Proteger($"<a href=\"{endereco}\">{textoComEnfase}</a>");
    }

    private static string AplicarEnfase(string texto)
    {
        var comNegritoItalico = PadroesInline.NegritoItalico.Replace(texto, "<strong><em>$1$2</em></strong>");
        var comNegrito = PadroesInline.Negrito.Replace(comNegritoItalico, "<strong>$1$2</strong>");

        return PadroesInline.Italico.Replace(comNegrito, "<em>$1$2</em>");
    }
}

internal sealed class TrechosProtegidos
{
    private readonly List<string> trechos = new();

    public string Proteger(string html)
    {
        trechos.Add(html);
        var indice = trechos.Count - 1;

        return Marcador(indice);
    }

    public string Restaurar(string texto)
    {
        var restaurado = texto;

        for (var indice = trechos.Count - 1; indice >= 0; indice--)
            restaurado = restaurado.Replace(Marcador(indice), trechos[indice]);

        return restaurado;
    }

    private static string Marcador(int indice) => $"\uE000{indice}\uE001";
}

internal static class TextoHtml
{
    public static string Escapar(string texto)
    {
        var escapado = texto.Replace("&", "&amp;");
        escapado = escapado.Replace("<", "&lt;");
        escapado = escapado.Replace(">", "&gt;");
        escapado = escapado.Replace("\"", "&quot;");

        return escapado;
    }
}

internal static class PadroesMarkdown
{
    public static readonly Regex AberturaDeCerca = new(@"^\s{0,3}```\s*([^\s`]*).*$");
    public static readonly Regex FechamentoDeCerca = new(@"^\s{0,3}```\s*$");
    public static readonly Regex Cabecalho = new(@"^\s{0,3}(#{1,6})\s+(.+?)(?:\s+#+)?\s*$");
    public static readonly Regex RegraHorizontal = new(@"^\s{0,3}([-*_])(?:\s*\1){2,}\s*$");
    public static readonly Regex Citacao = new(@"^\s{0,3}> ?");
    public static readonly Regex ItemNaoOrdenado = new(@"^\s{0,3}[-*+]\s+(.*)$");
    public static readonly Regex ItemOrdenado = new(@"^\s{0,3}\d{1,9}[.)]\s+(.*)$");

    public static bool IniciaBlocoEspecial(string linha)
    {
        return AberturaDeCerca.IsMatch(linha)
            || Cabecalho.IsMatch(linha)
            || RegraHorizontal.IsMatch(linha)
            || Citacao.IsMatch(linha)
            || ItemNaoOrdenado.IsMatch(linha)
            || ItemOrdenado.IsMatch(linha);
    }

    public static string Grupo(Regex padrao, string texto, int indice)
    {
        var correspondencia = padrao.Match(texto);
        return Grupo(correspondencia, indice);
    }

    public static string Grupo(Match correspondencia, int indice)
    {
        var grupo = correspondencia.Groups[indice];
        return grupo.Value;
    }
}

internal static class PadroesInline
{
    private const RegexOptions Opcoes = RegexOptions.Singleline | RegexOptions.CultureInvariant;

    public static readonly Regex CodigoInline = new(@"`([^`\n]+)`");
    public static readonly Regex Imagem = new(@"!\[([^\]]*)\]\(([^)\s]+)\)");
    public static readonly Regex Link = new(@"\[([^\]]+)\]\(([^)\s]+)\)");

    public static readonly Regex NegritoItalico = new(
        @"\*\*\*(?=\S)(.+?)(?<=\S)\*\*\*|(?<!\w)___(?=\S)(.+?)(?<=\S)___(?!\w)", Opcoes);

    public static readonly Regex Negrito = new(
        @"\*\*(?=\S)(.+?)(?<=\S)\*\*|(?<!\w)__(?=\S)(.+?)(?<=\S)__(?!\w)", Opcoes);

    public static readonly Regex Italico = new(
        @"\*(?=\S)(.+?)(?<=\S)\*|(?<!\w)_(?=\S)(.+?)(?<=\S)_(?!\w)", Opcoes);
}