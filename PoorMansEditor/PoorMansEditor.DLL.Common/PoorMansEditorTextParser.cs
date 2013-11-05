using System;
using System.Collections.Generic;
using System.Text;

namespace PoorMansEditor.Common
{
	/// <summary>
	/// No need for Shift Reduce or other LR
	/// </summary>
	public partial class PoorMansEditorTextParser
	{

		//-------------------------------------------------------------------------
		# region Property string CommandCurrent w Event post (CommandCurrentChanged)
		/// <summary>
		/// CommandCurrent
		/// </summary>
		public
		  string
		  CommandCurrent
		{
			get
			{
				return command_current;
			} // CommandCurrent.get
			set
			{
				//if (command_current != value)		// do not write if equivalent/equal/same
				{
					// for multi threading apps uncomment lines beginnig with //MT:
					//MT: lock(command_current) // MultiThread safe				
					{
						command_current = value;
						if (null != CommandCurrentChanged)
						{
							CommandCurrentChanged(this, new EventArgs());
						}
					}
				}
			} // CommandCurrent.set
		} // CommandCurrent

		/// <summary>
		/// private member field for holding CommandCurrent data
		/// </summary>
		private
			string
			command_current
			;

		///<summary>
		/// Event for wiring BusinessLogic object changes and presentation
		/// layer notifications.
		/// CommandCurrentChanged (<propertyname>Changed) is intercepted by Windows Forms
		/// 1.x and 2.0 event dispatcher 
		/// and for some CLR types by WPF event dispatcher 
		/// usually INotifyPropertyChanged and PropertyChanged event
		///</summary>
		public
			event
			EventHandler
			CommandCurrentChanged
			;
		# endregion Property string CommandCurrent w Event post (CommandCurrentChanged)
		//-------------------------------------------------------------------------

		//-------------------------------------------------------------------------
		# region Property string CommandPrevious w Event post (CommandPreviousChanged)
		/// <summary>
		/// CommandPrevious
		/// </summary>
		public
		  string
		  CommandPrevious
		{
			get
			{
				return command_previous;
			} // CommandPrevious.get
			set
			{
				//if (command_previous != value)		// do not write if equivalent/equal/same
				{
					// for multi threading apps uncomment lines beginnig with //MT:
					//MT: lock(command_previous) // MultiThread safe				
					{
						command_previous = value;
						if (null != CommandPreviousChanged)
						{
							CommandPreviousChanged(this, new EventArgs());
						}
					}
				}
			} // CommandPrevious.set
		} // CommandPrevious

		/// <summary>
		/// private member field for holding CommandPrevious data
		/// </summary>
		private
			string
			command_previous
			;

		///<summary>
		/// Event for wiring BusinessLogic object changes and presentation
		/// layer notifications.
		/// CommandPreviousChanged (<propertyname>Changed) is intercepted by Windows Forms
		/// 1.x and 2.0 event dispatcher 
		/// and for some CLR types by WPF event dispatcher 
		/// usually INotifyPropertyChanged and PropertyChanged event
		///</summary>
		public
			event
			EventHandler
			CommandPreviousChanged
			;
		# endregion Property string CommandPrevious w Event post (CommandPreviousChanged)
		//-------------------------------------------------------------------------
		
	
		//-------------------------------------------------------------------------
		# region Property List<string> TextCurrent w Event post (TextCurrentChanged)
		/// <summary>
		/// TextCurrent
		/// </summary>
		public
		  List<string>
		  TextCurrent
		{
			get
			{
				return text_current;
			} // TextCurrent.get
			set
			{
				//if (text_current != value)		// do not write if equivalent/equal/same
				{
					// for multi threading apps uncomment lines beginnig with //MT:
					//MT: lock(text_current) // MultiThread safe				
					{
						text_current = value;
						if (null != TextCurrentChanged)
						{
							TextCurrentChanged(this, new EventArgs());
						}
					}
				}
			} // TextCurrent.set
		} // TextCurrent

		/// <summary>
		/// private member field for holding TextCurrent data
		/// </summary>
		private
			List<string>
			text_current
			;

		///<summary>
		/// Event for wiring BusinessLogic object changes and presentation
		/// layer notifications.
		/// TextCurrentChanged (<propertyname>Changed) is intercepted by Windows Forms
		/// 1.x and 2.0 event dispatcher 
		/// and for some CLR types by WPF event dispatcher 
		/// usually INotifyPropertyChanged and PropertyChanged event
		///</summary>
		public
			event
			EventHandler
			TextCurrentChanged
			;
		# endregion Property List<string> TextCurrent w Event post (TextCurrentChanged)
		//-------------------------------------------------------------------------

		public PoorMansEditorTextParser()
		{
			this.TextCurrent = new List<string>();

			// Command changed handling
			CommandCurrentChanged += new EventHandler(PoorMansEditorTextParser_CommandCurrentChanged);

			return;
		}

		public string PoorManEditorText2Html(string text)
		{
			char[] archDelim = new char[] { '\r', '\n' };
			string[] input = text.Split(archDelim, StringSplitOptions.RemoveEmptyEntries);

			foreach (string line in input)
			{
				if (IsCommand(line))
				{
					this.CommandPrevious = this.CommandCurrent;
					this.CommandCurrent = line.Replace(".", "").Trim();
				}
				else
				{
					this.TextCurrent.Add(line);
				}

			}

			html = string.Join(Environment.NewLine, paragraphs.ToArray());

			System.Diagnostics.Debug.WriteLine("HTML = " + Environment.NewLine + html);

			return html;
		}

		private bool IsCommand(string line)
		{
			if (line.StartsWith("."))
			{
				// command
				return true;
			}
			else
			{
			}
			return false;
		}


		private void PoorMansEditorTextParser_CommandCurrentChanged(object sender, EventArgs e)
		{

			switch (this.CommandPrevious)
			{
				case "paragraph":
					// .paragraph: 
					// Starts a new paragraph
					GenerateParagraph(this.TextCurrent);
					break;
				case "regular":
					// .regular: 
					// resets the font to the normal font
					GenerateRegular(this.TextCurrent);
					break;
				case "normal":
					// .normal: 
					// resets the font to the normal font
					GenerateNormal(this.TextCurrent);
					break;
				case "fill":
					// .fill: 
					// enables sets indentation to fill for paragrahs, where the last character of 
					// a line must end at the end of the margin (except for the last line of a paragarph)
					GenerateFill(this.TextCurrent);
					break;
				case "nofill":
					// .nofill: 
					// the default, sets the formatter to regular formatting
					GenerateNoFill(this.TextCurrent);
					break;
				case "italic":
				case "italics":
					// .italics:  in the sample 
					// .italic:  int the command list
					// sets the font to italic
					GenerateItalic(this.TextCurrent);
					break;
				case "bold":
					// .bold: sets the font to bold
					GenerateBold(this.TextCurrent);
					break;
				case "large":
					// .paragraph: 
					// Starts a new paragraph
					GenerateLarge(this.TextCurrent);
					break;
				case "indent":
					// .indent NUMBER: 
					// indents the specified amount (each unit is probably about the lenght of 
					// the string “WWWW”, but other values would work)
					GenerateIndent(this.TextCurrent);
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
		private int indent_in_px = 50;
		private string html_styled = @"<span></span>";
		List<string> paragraphs = new List<string>();

		private void GenerateParagraph(List<string> list)
		{
			paragraphs.Add(html_paragraph_current);
			html_paragraph_current = "<p></p>";

			return;
		}

		private void GenerateLarge(List<string> list)
		{
			html_inner = string.Join(" ", list.ToArray());
			string html_styled_large = html_styled.Replace(@"<span>", @"<span style=""font-size:large;"">");

			html_styled_large = html_styled_large.Replace(@"</span>", html_inner + @"</span>");

			if (html_paragraph_current == "")
			{
				html_paragraph_current = "<p></p>";
			}

			html_paragraph_current = html_paragraph_current.Replace(@"</p>", html_styled_large + @"</p>");

			return;
		}


		private void GenerateFill(List<string> list)
		{
			if (html_paragraph_current == "")
			{
				html_paragraph_current = "<p></p>";
			}

			html_inner = string.Join(" ", list.ToArray());
			string html_styled_fill = html_styled.Replace(@"<span>", @"<span style=""text-align:justify;"">");

			html_styled_fill = html_styled_fill.Replace(@"</span>", html_inner + @"</span>");

			html_paragraph_current = html_paragraph_current.Replace(@"</p>", html_styled_fill + @"</p>");


			return;
		}

		private void GenerateNoFill(List<string> list)
		{
			if (html_paragraph_current == "")
			{
				html_paragraph_current = "<p></p>";
			}

			html_inner = string.Join(" ", list.ToArray());
			string html_styled_nofill = html_styled.Replace(@"<span>", @"<span style=""text-align:left;"">");

			html_styled_nofill = html_styled_nofill.Replace(@"</span>", html_inner + @"</span>");

			html_paragraph_current = html_paragraph_current.Replace(@"</p>", html_styled_nofill + @"</p>");


			return;
		}

		private void GenerateNormal(List<string> list)
		{
			if (html_paragraph_current == "")
			{
				html_paragraph_current = "<p></p>";
			}

			html_inner = string.Join(" ", list.ToArray());
			string html_styled_regular = html_styled;

			html_styled_regular = html_styled_regular.Replace(@"</span>", html_inner + @"</span>");

			html_paragraph_current = html_paragraph_current.Replace(@"</p>", html_styled_regular + @"</p>");


			return;
		}

		private void GenerateRegular(List<string> list)
		{
			if (html_paragraph_current == "")
			{
				html_paragraph_current = "<p></p>";
			}

			html_inner = string.Join(" ", list.ToArray());
			string html_styled_regular = html_styled;

			html_styled_regular = html_styled_regular.Replace(@"</span>", html_inner + @"</span>");

			html_paragraph_current = html_paragraph_current.Replace(@"</p>", html_styled_regular + @"</p>");


			return;
		}


		private void GenerateItalic(List<string> list)
		{
			if (html_paragraph_current == "")
			{
				html_paragraph_current = "<p></p>";
			}

			html_inner = string.Join(" ", list.ToArray());
			string html_styled_italic = html_styled.Replace(@"<span>", @"<span style=""font-style:italic;"">");

			html_styled_italic = html_styled_italic.Replace(@"</span>", html_inner + @"</span>");

			html_paragraph_current = html_paragraph_current.Replace(@"</p>", html_styled_italic + @"</p>");


			return;
		}

		private void GenerateBold(List<string> list)
		{
			if (html_paragraph_current == "")
			{
				html_paragraph_current = "<p></p>";
			}

			html_inner = string.Join(" ", list.ToArray());
			string html_styled_bold = html_styled.Replace(@"<span>", @"<span style=""font-weight:bold;"">");

			html_styled_bold = html_styled_bold.Replace(@"</span>", html_inner + @"</span>");

			html_paragraph_current = html_paragraph_current.Replace(@"</p>", html_styled_bold + @"</p>");


			return;
		}

		private void GenerateIndent(List<string> list)
		{
			if (html_paragraph_current == "")
			{
				html_paragraph_current = "<p></p>";
			}

			html_inner = string.Join(" ", list.ToArray());
			string html_styled_bold = html_styled.Replace(@"<span>", @"<span style=""margin-left:" + indent_in_px.ToString() + @";"">");

			html_styled_bold = html_styled_bold.Replace(@"</span>", html_inner + @"</span>");

			html_paragraph_current = html_paragraph_current.Replace(@"</p>", html_styled_bold + @"</p>");


			return;
		}
	}
}
