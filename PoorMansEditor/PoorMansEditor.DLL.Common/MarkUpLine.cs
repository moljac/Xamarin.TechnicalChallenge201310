using System;
using System.Collections.Generic;
using System.Text;

namespace PoorMansEditor.Common
{
	/// <summary>
	/// </summary>
	public partial class MarkUpLine
	{
        public List<string> CommandMarkUp;
        public List<string> CommandStyle;
        public string TextInner;

        public MarkUpLine()
        {
            CommandMarkUp = new List<string>();
            CommandStyle = new List<string>();
            TextInner = "";

            return;
        }

        public override string ToString()
        {
            string markup = string.Join(" ", CommandMarkUp.ToArray());
            string style  = string.Join(" ", CommandStyle.ToArray());

            string retval = String.Format("Line[structure=[{0}], style=[{1}], text={2}]", markup, style, TextInner);

            return retval;
        }
    }
}
