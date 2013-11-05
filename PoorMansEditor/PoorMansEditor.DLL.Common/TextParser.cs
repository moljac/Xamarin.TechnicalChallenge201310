using System;
using System.Collections.Generic;
using System.Text;

namespace PoorMansEditor.Common
{
	/// <summary>
	/// No need for Shift Reduce or other LR
	/// </summary>
	public partial class TextParser
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

		public TextParser()
		{
			this.TextCurrent = new List<string>();

			// Command changed handling
			//CommandCurrentChanged += new EventHandler(PoorMansEditorTextParser_CommandCurrentChanged);

			return;
		}

		List<MarkUpLine> markdown_mapping;

		public string PoorManEditorText2Html(string text)
		{
			char[] archDelim = new char[] { '\r', '\n' };
			string[] input = text.Split(archDelim, StringSplitOptions.RemoveEmptyEntries);

			markdown_mapping = GenerateMarkDownMapping(input);
			paragraphs = MarkDownStructureToHtml(markdown_mapping);

			html = string.Join(Environment.NewLine, paragraphs.ToArray());

			System.Diagnostics.Debug.WriteLine("HTML = " + Environment.NewLine + html);

			return html;
		}

		private List<MarkUpLine> GenerateMarkDownMapping(string[] input)
		{
			List<MarkUpLine> md_map = new List<MarkUpLine>();

			MarkUpLine pme_line = null;
			string text_accumulated = "";

			foreach (string line in input)
			{
				if (IsCommand(line))
				{
					pme_line = new MarkUpLine();
					
					if (text_accumulated != "")
					{
						pme_line.TextInner = text_accumulated;
						text_accumulated = "";
					}

					string cmd = line.Replace(".", "").Trim();
					if (cmd == "paragraph")
					{
						pme_line.CommandMarkUp.Add(cmd);
					}
					else if (cmd.Contains("indent"))
					{
						pme_line.CommandMarkUp.Add("paragraph");
						pme_line.CommandStyle.Add(cmd);

						if (cmd.Contains("indent -"))
						{
							pme_line = new MarkUpLine();
							pme_line.CommandMarkUp.Add("paragraph");
							pme_line.CommandStyle.Add(cmd);
						}
					}
					else
					{
						pme_line.CommandStyle.Add(cmd);
					}

					md_map.Add(pme_line);
				}
				else
				{
					pme_line.TextInner = line;
				}
			   System.Diagnostics.Debug.WriteLine("pme_line =" + pme_line);
			}

			return md_map;
		}

		string html = @"";
		string style_global = @"";
		string html_block_template = "";

		Stack<string> stack = null;
		List<string> paragraphs = null;
		
		private List<string> MarkDownStructureToHtml(List<MarkUpLine> input)
		{
			paragraphs = new List<string>();
			stack = new Stack<string>();

			foreach (MarkUpLine pme_line in input)
			{
				Console.WriteLine(@"pme_line = " + pme_line);

				if (pme_line.CommandMarkUp.Count == 0)
				{
					html_block_template = @"<span></span>";
				}
				else
				{
					html_block_template = @"<p></p>";
				}

				if (pme_line.CommandMarkUp.Contains("paragraph"))
				{
					Push("paragraph", HtmlStyle(pme_line.CommandStyle), pme_line.TextInner);
				}
				else
				{
					Push("", HtmlStyle(pme_line.CommandStyle), pme_line.TextInner);
				}
			}
			paragraphs.Add(html_block_accumulated);

			return paragraphs;
		}

		string html_block_accumulated = "";

		private void Push(string command, string style, string html_inner)
		{
			if (command == "paragraph" && stack.Count == 0)
			{
				// paragraph mode - new / push
				this.stack.Push(command);
				html_block_accumulated = @"<p>";
				html_block_accumulated = html_block_accumulated.Replace(@"<p>", @"<p " + style + @">" + html_inner);
			}
			else if (command == "paragraph" && stack.Count != 0)
			{
				// paragraph mode - old / pop
				this.stack.Pop();
				html_block_accumulated += @" </p>";
				paragraphs.Add(html_block_accumulated);

				// paragraph mode - new / push
				this.stack.Push(command);
				html_block_accumulated = @"<p>";
				style = @"style=""" + style + @"""";
				html_block_accumulated = html_block_accumulated.Replace(@"<p>", @"<p " + style + @">" + html_inner);
			}
			else if (command == "" && stack.Count == 0)
			{
				// paragraph mode - undefined 
				html_block_accumulated = @"<span " + style + @">" + html_inner + @" </span>";
				paragraphs.Add(html_block_accumulated);
			}
			else if (command == "" && stack.Count != 0)
			{
				// paragraph mode - old / append
				if (html_inner == "")
				{
					if (html_block_accumulated.Contains(@"<p style="))
					{
						html_block_accumulated = html_block_accumulated.Replace(@"<p style=""", @"<p style=""" + style);
					}
					else 
					{
						style = @"style=""" + style + @"""";
						html_block_accumulated = html_block_accumulated.Replace(@"<p", @"<p " + style);
					}
				}
				else
				{
					 if (style != "")
					{
						if (style.Contains("text-align:justify; align:justify"))
						{
							html_block_accumulated = html_block_accumulated.Replace(@"<p style=""", @"<p style=""" + style + ";");
							html_block_accumulated += html_inner;
						}
						else
						{
							style = @"style=""" + style + @"""";
							html_block_accumulated += @"<span " + style + @">" + html_inner + @" </span>";
						}
					}
					else
					{
						html_block_accumulated += html_inner + @"</p>";
					}
				}
			}
			

			return;
		}

		
		private int indent_default_in_px = 30;
		private int indent_delta_factor = 0;
		private int indent_global_in_px = 0;
		
		private string HtmlStyle(List<string> list)
		{
			string style_accumulated = "";


			foreach (string s in list)
			{
				string cmd_style = s;

				if (cmd_style.StartsWith("indent"))
				{
					cmd_style = s.Replace("indent", "").Trim();
					bool conversion_ok = int.TryParse(cmd_style, out indent_delta_factor);
					indent_delta_factor *= indent_default_in_px;
					cmd_style = "indent";

					indent_global_in_px += indent_delta_factor;
				}


				switch (cmd_style)
				{
					case "regular":
						// .regular: 
						// resets the font to the normal font
					case "normal":
						// .normal: 
						// resets the font to the normal font
						// missing in doc
						style_accumulated += "";
						break;
					case "fill":
						// .fill: 
						// enables sets indentation to fill for paragrahs, where the last character of 
						// a line must end at the end of the margin (except for the last line of a paragarph)
						style_accumulated += @"text-align:justify; align:justify";
						break;
					case "nofill":
						// .nofill: 
						// the default, sets the formatter to regular formatting
						style_accumulated += "text-align:left;";
						break;
					case "italic":
					case "italics":
						// .italics:  in the sample 
						// .italic:  int the command list
						// sets the font to italic
						style_accumulated += "font-style:italic;";
						break;
					case "bold":
						// .bold: sets the font to bold
						style_accumulated += "font-weight:bold;";
						break;
					case "large":
						// .large: 
						// missing in doc
						style_accumulated += "font-size:large;";
						break;
					case "indent":
						// .indent NUMBER: 
						// indents the specified amount (each unit is probably about the lenght of 
						// the string “WWWW”, but other values would work)
					style_accumulated += "margin-left:" + indent_global_in_px.ToString() + @"px;";
						break;
					default:
						break;
				}
			}

			return style_accumulated;
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









	}
}
