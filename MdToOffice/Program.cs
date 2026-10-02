using Microsoft.VisualBasic;
using System;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Forms;

class Program
{
    [STAThread]
    static void Main(string[] args)
    {

        var cssStyleCode = File.ReadAllText("Style\\oneNote.css");
        var oneNoteStyle = true;

        foreach (var item in args)
        {
            switch (item.ToUpper())
            {
                case "FIAP":
                    cssStyleCode = File.ReadAllText("Style\\fiap.css");
                    oneNoteStyle = false;
                    break;
            }
        }

        //CopiarHtmlParaClipboard(html);

        //var MarkDown = File.ReadAllText("Mock\\fiapMarkDownExample1.md");
        var markDownFromClipboard = Clipboard.GetText(TextDataFormat.Text);
        MarkDown markDown = new MarkDown();

        var HtmlConverted = markDown.ConverterParaHtml(markDownFromClipboard);

        if (oneNoteStyle)
        {
            HtmlConverted = HtmlConverted.Replace("<hr>", "");
            HtmlConverted = HtmlConverted.Replace("<th style=\"text-align: right\">", "<th>");
            HtmlConverted = HtmlConverted.Replace("</p>\n<ul>", "</p>&emsp;<ul>");
            HtmlConverted = HtmlConverted.Replace("</p>\n<table>", "</p>&emsp;<table>");
            HtmlConverted = HtmlConverted.Replace("</table>\n<p>", "</table>&emsp;<p>");
            HtmlConverted = HtmlConverted.Replace("</p>\n<ol>", "</p>&emsp;<ol>");
            HtmlConverted = HtmlConverted.Replace("</ul>", "</ul>&emsp;");
            HtmlConverted = HtmlConverted.Replace("</h1>", "</h1>&emsp;");
            HtmlConverted = HtmlConverted.Replace("</h2>", "</h2>&emsp;");
            HtmlConverted = HtmlConverted.Replace("</h3>", "</h3>&emsp;");
            HtmlConverted = HtmlConverted.Replace("</h4>", "</h4>&emsp;");
            HtmlConverted = HtmlConverted.Replace("<h1>", "&emsp;<BR>&emsp;<h1>");
            HtmlConverted = HtmlConverted.Replace("<h2>", "&emsp;<BR>&emsp;<h2>");
            HtmlConverted = HtmlConverted.Replace("<h3>", "&emsp;<BR>&emsp;<h3>");
            HtmlConverted = HtmlConverted.Replace("<h4>", "&emsp;<BR>&emsp;<h4>");
            HtmlConverted = HtmlConverted.Replace("&emsp;\n&emsp;<BR>&emsp;", "&emsp;&emsp;");
        }


        string html = """
        <!DOCTYPE html>
        <html lang="pt-br">
        <head>
            <style>
       
        """ + cssStyleCode + """     


            </style>
        </head>

        <body>

       """ + HtmlConverted + """
        

        </body>
        </html>
        """;

        CopiarHtmlParaClipboard(html);


        File.WriteAllText("Temp/index.html", html);
    }



    static void CopiarHtmlParaClipboard(string html)
    {
        string clipboardHtml = CriarClipboardHtml(html);

        var data = new DataObject();

        // HTML para aplicações como Word
        data.SetData(DataFormats.Html, clipboardHtml);

        // Texto alternativo
        data.SetData(
            DataFormats.Text,
            RemoverTagsHtml(html)
        );

        Clipboard.SetDataObject(data, true);
    }

    static string CriarClipboardHtml(string html)
    {
        const string header =
            "Version:0.9\r\n" +
            "StartHTML:{0:D10}\r\n" +
            "EndHTML:{1:D10}\r\n" +
            "StartFragment:{2:D10}\r\n" +
            "EndFragment:{3:D10}\r\n";

        const string startFragment =
            "<!--StartFragment-->";

        const string endFragment =
            "<!--EndFragment-->";

        string body =
            "<html><body>" +
            startFragment +
            html +
            endFragment +
            "</body></html>";

        int headerLength = string.Format(
            header,
            0,
            0,
            0,
            0
        ).Length;

        int startHtml = headerLength;

        int endHtml = startHtml + body.Length;

        int startFragmentPosition =
            startHtml +
            body.IndexOf(startFragment) +
            startFragment.Length;

        int endFragmentPosition =
            startHtml +
            body.IndexOf(endFragment);

        string result = string.Format(
            header,
            startHtml,
            endHtml,
            startFragmentPosition,
            endFragmentPosition
        ) + body;

        return result;
    }

    static string RemoverTagsHtml(string html)
    {
        return System.Text.RegularExpressions.Regex
            .Replace(html, "<.*?>", string.Empty);
    }
}