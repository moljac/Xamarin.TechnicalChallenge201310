using System;
using System.Collections.Generic;
using System.Text;

namespace PoorMansEditor.Common
{
    /// <summary>
    /// </summary>
    public partial class PoorMansEditorTextParser
    {
        private void PoorMansEditorTextParser_CommandCurrentChanged(object sender, EventArgs e)
        {

            switch (this.CommandPrevious)
            {
                case "paragraph":
                    // .paragraph: 
                    // Starts a new paragraph
                    GenerateParagraph("paragraph", this.TextCurrent);
                    break;
                case "regular":
                    // .regular: 
                    // resets the font to the normal font
                    Generate("regular", this.TextCurrent, @"");
                    break;
                case "normal":
                    // .normal: 
                    // resets the font to the normal font
                    // missing in doc
                    Generate("normal", this.TextCurrent, @"");
                    break;
                case "fill":
                    // .fill: 
                    // enables sets indentation to fill for paragrahs, where the last character of 
                    // a line must end at the end of the margin (except for the last line of a paragarph)
                    Generate("fill", this.TextCurrent, @"<span style=""text-align:justify;""");
                    break;
                case "nofill":
                    // .nofill: 
                    // the default, sets the formatter to regular formatting
                    Generate("nofill", this.TextCurrent, @"<span style=""text-align:left;""");
                    break;
                case "italic":
                case "italics":
                    // .italics:  in the sample 
                    // .italic:  int the command list
                    // sets the font to italic
                    Generate("italic[s]", this.TextCurrent, @"<span style=""font-style:italic;""");
                    break;
                case "bold":
                    // .bold: sets the font to bold
                    Generate("bold", this.TextCurrent, @"<span style=""font-weight:bold;""");
                    break;
                case "large":
                    // .large: 
                    // missing in doc
                    Generate("large", this.TextCurrent, @"style=""font-size:large;""");
                    break;
                case "indent":
                    // .indent NUMBER: 
                    // indents the specified amount (each unit is probably about the lenght of 
                    // the string “WWWW”, but other values would work)
                    Generate("indent", this.TextCurrent, @"style=""margin-left:" + indent_in_px.ToString() + @";""");
                    break;
                default:
                    break;
            }

            return;
        }

        string html = "";
        string html_pretty_output = Environment.NewLine;
        private string html_paragraph_current = "";
        private string html_inner = "";
        private readonly string html_styled = @"<span style=""""></span>";
        List<string> paragraphs = new List<string>();

        private readonly string style_normal = @"style=""""";
        private string style_previous = @"";

        private void GenerateParagraph(string command, List<string> list)
        {
            if (html_paragraph_current.StartsWith(@"<p>") == false)
            {
                html_paragraph_current = @"<p>" + html_paragraph_current;
            }
            if (html_paragraph_current.EndsWith(@"</p>") == false)
            {
                html_paragraph_current = html_paragraph_current + @"</p>";
            }

            paragraphs.Add(html_paragraph_current);

            return;
        }

        private void Generate(string command, List<string> list, string style_previous)
        {
            html_inner = string.Join(" ", list.ToArray());

            string html_styled_new = html_styled.Replace(style_normal, style_previous);

            html_styled_new = html_styled_new.Replace(@"</span>", html_inner + @"</span>");

            html_paragraph_current += html_styled_new + Environment.NewLine;

            this.TextCurrent.Clear();

            return;
        }

    }
}
