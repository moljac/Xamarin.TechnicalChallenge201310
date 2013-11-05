using System;
using Gtk;

using System.Linq;
using System.Collections.Generic;

namespace GTKSharpTest.DLL.Widgets
{
	public partial class FontDialogOfficeWidget : Gtk.Bin
	{
		Gtk.TextTag tag = null;
		Gtk.TextTagTable table_text_tag = null;

		private void ApplyTextTags(Gtk.TextBuffer tb, string tagname)
		{
			int cN = tb.Text.Length - 1;

			Gtk.TextIter start = tb.GetIterAtOffset(0);
			Gtk.TextIter stop = tb.GetIterAtOffset(cN);

			string[] delimiters = new string[] { ":" };
			string[] tagname_parts = tagname.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
			string key = tagname_parts[0];
			string value = null;
			if (tagname_parts.Length > 1)
			{
				value = tagname_parts[1];
			}

			Gtk.TextTag tag_lookup = tb.TagTable.Lookup("DemoTextSelection");
			Gtk.TextTag tag_local = null;
			// should be the same?!?!
			//tag_local = tag;		// class variable?? sometines null?
			tag_local = tag_lookup;


			Console.WriteLine("key [{0}] value[{1}] tagname [{2}]", key, value, tagname);
			switch (key)
			{
				case "font":
					tag_local.Font = value;
					break;
				case "fontsize":
					tag_local.Size = Convert.ToInt32(value); // events?!?!?
					break;
				case "fontweight":
					switch (value)
					{
						case "normal":
							tag_local.Weight = Pango.Weight.Normal;
							break;
						case "bold":
							// tag_local.Weight = Pango.Weight.Bold; // no visible diff
							tag_local.Weight = Pango.Weight.Ultrabold;
							break;
						case "heavy":
							tag_local.Weight = Pango.Weight.Heavy;
							break;
						case "light":
							tag_local.Weight = Pango.Weight.Light;
							break;
						case "semibold":
							tag_local.Weight = Pango.Weight.Semibold;
							break;
						case "ultrabold":
							tag_local.Weight = Pango.Weight.Ultrabold;
							break;
						case "ultralight":
							tag_local.Weight = Pango.Weight.Ultralight;
							break;
						default:
							tag_local.Weight = Pango.Weight.Normal;
							break;
					}
					break;
				case "style":
					switch (value)
					{
						case "normal":
							tag_local.Style = Pango.Style.Normal;
							break;
						case "italic":
							tag_local.Style = Pango.Style.Italic;
							break;
						case "oblique":
							tag_local.Style = Pango.Style.Oblique;
							break;
						default:
							tag_local.Style = Pango.Style.Normal;
							break;
					}
					break;
				case "strikethrough":
					tag_local.Strikethrough = Convert.ToBoolean(value);
					break;
				case "doublestrikethrough":
					tag_local.Strikethrough = Convert.ToBoolean(value);
					break;
				case "superscript":
					if (Convert.ToBoolean(value))
					{
						tag_local.Rise = 50;
					}
					else
					{
						tag_local.Rise = 0;
					}
					break;
				case "subscript":
					if (Convert.ToBoolean(value))
					{
						tag_local.Rise = -50;
					}
					else
					{
						tag_local.Rise = 0;
					}
					break;
				case "smallcaps":
					if (Convert.ToBoolean(value))
					{
						tag_local.Variant = Pango.Variant.SmallCaps;
					}
					else
					{
						tag_local.Variant = Pango.Variant.Normal;
					}
					break;
				case "allcaps":
					// no all caps?
					// tamp replacement
					if (Convert.ToBoolean(value))
					{
						tb.Text = tb.Text.ToUpper();
						tag_local.Variant = Pango.Variant.Normal;
					}
					else
					{
						tb.Text = this.demo_text;
						tag_local.Variant = Pango.Variant.Normal;
					}
					break;
				case "hidden":
					if (Convert.ToBoolean(value))
					{
						tag_local.Variant = Pango.Variant.Normal;
					}
					else
					{
						tag_local.Rise = 0;
					}
					break;
				case "underlinestyle":
					switch (value)
					{
						case "none":
							tag_local.Underline = Pango.Underline.None;
							break;
						case "double":
							tag_local.Underline = Pango.Underline.Double;
							break;
						case "single":
							tag_local.Underline = Pango.Underline.Single;
							break;
						case "error":
							tag_local.Underline = Pango.Underline.Error;
							break;
						case "low":
							tag_local.Underline = Pango.Underline.Low;
							break;
						default:
							tag_local.Underline = Pango.Underline.None;
								break;
					}
					break;
				case "underlinecolor":
					// cannot change color of the underline!
					// tag_local.Underline
					// trying font until custom dialog is over
					string rgb = tagname_parts[2];			// obtained as "rgb:HHHH/HHHH/HHHH"

					//--------------------------------------------------------------------
					// Attempt 1
					tag_local.Foreground = "";
					tag_local.ForegroundGdk = new Gdk.Color(60, 60, 60);
					//--------------------------------------------------------------------
				
					//--------------------------------------------------------------------
					// Attempt 2
					//textviewDemoText.ModifyFg(StateType.Normal, underline_color_selected);	// color not applied?!?!					
					//textviewDemoText.ModifyBg(StateType.Normal, underline_color_selected);	// color not applied?!?!					
					// return;		// to skip tb.ApplyTag later does not change color

					break;
				default:
					tag_local = tb.TagTable.Lookup(tagname);
					break;
			}

			tb.ApplyTag(tag_local, start, stop);

			return;
		}


		private void CreateTextTags(Gtk.TextBuffer tb)
		{
			table_text_tag = new Gtk.TextTagTable();

			// Tag per text block - not style
			tag = new Gtk.TextTag("DemoTextSelection");
			tag.WrapMode = Gtk.WrapMode.Char;
			tb.TagTable.Add(tag);

			return;
		}

	}
}

