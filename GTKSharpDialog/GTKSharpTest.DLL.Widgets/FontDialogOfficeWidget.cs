using System;
using Gtk;

using System.Linq;
using System.Collections.Generic;

namespace GTKSharpTest.DLL.Widgets
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class FontDialogOfficeWidget : Gtk.Bin
	{
		string demo_text =
@"

HolisticWare shameless plug

";
		public FontDialogOfficeWidget ()
		{
			this.Build ();

			this.notebook1.CurrentPage = 0;

			this.CreateTextTags (textviewDemoText.Buffer);

			FontsListViewPrepare();
			FonstSizesListViewPrepare();
			FontStylesListViewPrepare();

			this.textviewDemoText.Buffer.Text = demo_text;

			return;
		}

		private void FontStylesListViewPrepare()
		{
			//--------------------------------------------------------------
			List<string> fontstyles = this.FontStyles();
			// Create a column for the artist name
			Gtk.TreeViewColumn fontstyle_column = new Gtk.TreeViewColumn();
			this.treeviewFontStyles.HeadersVisible = false;
			// fontsize_column.Title = "Fonts Size"; 
			this.treeviewFontStyles.AppendColumn(fontstyle_column);
			// Create a model - 1 string - Font Name
			Gtk.ListStore fontstyles_list_store = new Gtk.ListStore(typeof(string));
			// Assign the model to the TreeView
			this.treeviewFontStyles.Model = fontstyles_list_store;
			foreach (string s in fontstyles)
			{
				fontstyles_list_store.AppendValues(s);
			}
			// Create the text cell that will display the font name
			Gtk.CellRendererText fontstyle_name_cell = new Gtk.CellRendererText();

			// Add the cell to the column
			fontstyle_column.PackStart(fontstyle_name_cell, true);

			// Tell the Cell Renderers which items in the model to display
			fontstyle_column.AddAttribute(fontstyle_name_cell, "text", 0);

			this.treeviewFontStyles.CursorChanged += treeViewFontStyles_SelectionChanged;
			//--------------------------------------------------------------

			return;
		}

		private void FonstSizesListViewPrepare()
		{
			//--------------------------------------------------------------
			List<int> fontsizes = this.FontSizes();
			// Create a column for the artist name
			Gtk.TreeViewColumn fontsize_column = new Gtk.TreeViewColumn();
			this.treeviewFontSizes.HeadersVisible = false;
			// fontsize_column.Title = "Fonts Size"; 
			this.treeviewFontSizes.AppendColumn(fontsize_column);
			// Create a model - 1 string - Font Name
			Gtk.ListStore fontsizes_list_store = new Gtk.ListStore(typeof(int));
			// Assign the model to the TreeView
			this.treeviewFontSizes.Model = fontsizes_list_store;
			foreach (int s in fontsizes)
			{
				fontsizes_list_store.AppendValues(s);
			}
			// Create the text cell that will display the font name
			Gtk.CellRendererText fontize_name_cell = new Gtk.CellRendererText();

			// Add the cell to the column
			fontsize_column.PackStart(fontize_name_cell, true);

			// Tell the Cell Renderers which items in the model to display
			fontsize_column.AddAttribute(fontize_name_cell, "text", 0);

			this.treeviewFontSizes.CursorChanged += treeViewFontSizes_SelectionChanged;
			//--------------------------------------------------------------

			return;
		}

		private void FontsListViewPrepare()
		{
			//--------------------------------------------------------------
			List<string> fonts = this.Fonts();
			// Create a column for the artist name
			Gtk.TreeViewColumn font_column = new Gtk.TreeViewColumn();
			this.treeviewFonts.HeadersVisible = false;
			// font_column.Title = "Fonts"; 
			this.treeviewFonts.AppendColumn(font_column);
			// Create a model - 1 string - Font Name
			Gtk.ListStore font_list_store = new Gtk.ListStore(typeof(string));
			// Assign the model to the TreeView
			this.treeviewFonts.Model = font_list_store;
			foreach (string s in fonts)
			{
				font_list_store.AppendValues(s);
			}
			// Create the text cell that will display the font name
			Gtk.CellRendererText font_name_cell = new Gtk.CellRendererText();

			// Add the cell to the column
			font_column.PackStart(font_name_cell, true);

			// Tell the Cell Renderers which items in the model to display
			font_column.AddAttribute(font_name_cell, "text", 0);

			this.treeviewFonts.CursorChanged += treeViewFonts_SelectionChanged;
			//--------------------------------------------------------------

			return;
		}

		void treeViewFonts_SelectionChanged (object sender, EventArgs args)
		{
			string font_selected = "";

			TreeSelection selection = (sender as TreeView).Selection;
			TreeModel model;
			TreeIter iter;

			// iter - points to selected row
			if(selection.GetSelected(out model, out iter))
			{
				TreePath tree_path = model.GetPath (iter);
				string select_path = tree_path.ToString();

				int idx_column = 0;
				object selected_object = model.GetValue (iter, idx_column);
				font_selected = (string)selected_object;
			}

			entryFont.Text = font_selected;
			ApplyTextTags (textviewDemoText.Buffer, "font:" + font_selected);

			return;
		}

		void treeViewFontSizes_SelectionChanged (object sender, EventArgs args)
		{
			int fontsize_selected = -1;

			TreeSelection selection = (sender as TreeView).Selection;
			TreeModel model;
			TreeIter iter;

			// iter - points to selected row
			if(selection.GetSelected(out model, out iter))
			{
				TreePath tree_path = model.GetPath (iter);
				string select_path = tree_path.ToString();

				int idx_column = 0;
				object selected_object = model.GetValue (iter, idx_column);
				fontsize_selected = (int)selected_object;
			}

			entryFontSize.Text = fontsize_selected.ToString();
			ApplyTextTags (textviewDemoText.Buffer, "fontsize:" + fontsize_selected.ToString());

			return;
		}

		void treeViewFontStyles_SelectionChanged (object sender, EventArgs args)
		{
			string fontstyle_selected = "";

			TreeSelection selection = (sender as TreeView).Selection;
			TreeModel model;
			TreeIter iter;

			// iter - points to selected row
			if(selection.GetSelected(out model, out iter))
			{
				TreePath tree_path = model.GetPath (iter);
				string select_path = tree_path.ToString();

				int idx_column = 0;
				object selected_object = model.GetValue (iter, idx_column);
				fontstyle_selected = (string)selected_object;
			}

			entryFontStyle.Text = fontstyle_selected;
			switch (fontstyle_selected)
			{
				case "Regular":
					ApplyTextTags (textviewDemoText.Buffer, "style:normal");
					ApplyTextTags (textviewDemoText.Buffer, "fontweight:normal");
					break;
				case "Italic":
					ApplyTextTags (textviewDemoText.Buffer, "style:italic");
					ApplyTextTags (textviewDemoText.Buffer, "fontweight:normal");
					break;
				case "Bold":
					ApplyTextTags (textviewDemoText.Buffer, "style:normal");
					ApplyTextTags (textviewDemoText.Buffer, "fontweight:bold");
					break;
				case "Bold Italic":
					ApplyTextTags (textviewDemoText.Buffer, "style:italic");
					ApplyTextTags (textviewDemoText.Buffer, "fontweight:bold");
					break;
				default:
					break;
			}

			return;
		}


		protected void OnCheckbuttonStrikethroughToggled (object sender, EventArgs e)
		{
			if (this.checkbuttonStrikethrough.Active)
			{
				ApplyTextTags(textviewDemoText.Buffer, "strikethrough:true");
			}
			else
			{
				ApplyTextTags(textviewDemoText.Buffer, "strikethrough:false");
			}
			return;
		}

		protected void OnCheckbuttonDoubleStrikethroughToggled (object sender, EventArgs e)
		{
			if (this.checkbuttonDoubleStrikethrough.Active)
			{
				ApplyTextTags(textviewDemoText.Buffer, "doublestrikethrough:true");
			}
			else
			{
				ApplyTextTags(textviewDemoText.Buffer, "doublestrikethrough:false");
			}
			return;
		}

		protected void OnCheckbuttonSuperscriptToggled (object sender, EventArgs e)
		{
			if (this.checkbuttonSuperscript.Active)
			{
				ApplyTextTags(textviewDemoText.Buffer, "superscript:true");
			}
			else
			{
				ApplyTextTags(textviewDemoText.Buffer, "superscript:false");
			}
			return;
		}

		protected void OnCheckbuttonSubscriptToggled (object sender, EventArgs e)
		{
			if (this.checkbuttonSubscript.Active)
			{
				ApplyTextTags(textviewDemoText.Buffer, "subscript:true");
			}
			else
			{
				ApplyTextTags(textviewDemoText.Buffer, "subscript:false");
			}
			return;
		}

		protected void OnCheckbuttonSmallCapsToggled (object sender, EventArgs e)
		{
			if (this.checkbuttonSmallCaps.Active)
			{
				ApplyTextTags(textviewDemoText.Buffer, "smallcaps:true");
			}
			else
			{
				ApplyTextTags(textviewDemoText.Buffer, "smallcaps:false");
			}
			return;
		}

		protected void OnCheckbuttonAllCapsToggled (object sender, EventArgs e)
		{
			if (this.checkbuttonAllCaps.Active)
			{
				ApplyTextTags(textviewDemoText.Buffer, "allcaps:true");
			}
			else
			{
				ApplyTextTags(textviewDemoText.Buffer, "allcaps:false");
			}
			return;
		}

		protected void OnCheckbuttonHiddenToggled (object sender, EventArgs e)
		{
			if (this.checkbuttonHidden.Active)
			{
				ApplyTextTags(textviewDemoText.Buffer, "hidden:true");
			}
			else
			{
				ApplyTextTags(textviewDemoText.Buffer, "hidden:false");
			}
			return;
		}

		protected void OnComboboxUnderlineChanged(object sender, EventArgs e)
		{
			string style_undelined_selected = comboboxUnderline.ActiveText;

			switch (style_undelined_selected)
			{
				case "None":
					ApplyTextTags(textviewDemoText.Buffer, "underlinestyle:none");
					break;
				case "Single":
					ApplyTextTags(textviewDemoText.Buffer, "underlinestyle:single");
					break;
				case "Double":
					ApplyTextTags(textviewDemoText.Buffer, "underlinestyle:double");
					break;
				case "Low":
					ApplyTextTags(textviewDemoText.Buffer, "underlinestyle:low");
					break;
				case "Error":
					ApplyTextTags(textviewDemoText.Buffer, "underlinestyle:error");
					break;
				default:
					break;
			}


			return;
		}
		
		public List<string> Fonts()
		{
			List<string> fonts = new List<string> ();

			// Pango.FontFamily
			// System.Drawing.FontFamily
			fonts = System.Drawing.FontFamily.Families.Select(f => f.Name)
							.ToList<string>();

			return fonts;
		}

		public List<int> FontSizes()
		{
			List<int> font_sizes = new List<int> () 
			{
			  6
			, 7
			, 8
			, 9
			, 10
			, 11
			, 12
			, 13
			, 14
			, 15
			, 16
			, 17
			, 18
			, 20
			, 22
			, 24
			, 28
			, 32
			, 36
			, 40
			, 48
			, 56
			, 64
			, 72
			};

			return font_sizes;
		}

		public List<string> FontStyles()
		{
			List<string> font_styles = new List<string> () 
			{
			  "Regular"
			, "Italic"
			, "Bold"
			, "Bold Italic"
			};

			return font_styles;
		}


		protected void colorButtonFontColor_ColorSet (object sender, EventArgs e)
		{

			return;
		}

		protected void OnButtonOKClicked (object sender, EventArgs e)
		{
			Application.Quit ();

			return;
		}

		protected void OnButtonCancelClicked (object sender, EventArgs e)
		{
			Application.Quit ();

			return;
		}


		Gdk.Color color = new Gdk.Color(22,22,22);

	
		protected void OnColorbuttonFontColorClicked (object sender, EventArgs e)
		{
			/*
			ColorSelectionDialog cdia = new ColorSelectionDialog("Select color");
			cdia.Response += delegate (object o, ResponseArgs resp) 
			{

				if (resp.ResponseId == ResponseType.Ok) 
				{
					color = cdia.ColorSelection.CurrentColor;

					//label.ModifyFg(StateType.Normal, color);
					this.ApplyTextTags (textviewDemoText.Buffer, "color");
				}
			};

			cdia.Run();
			cdia.Destroy();
			*/


			return;
		}

		protected void OnColorbuttonFontColorColorSet (object sender, EventArgs e)
		{
			// cannot get Color after selection
			// color = this.colorbuttonFontColor.Color; 

			return;
		}

		Gdk.Color underline_color_selected = Gdk.Color.Zero;

		protected void OnButtonUndelineColorClicked(object sender, EventArgs e)
		{
			ColorSelectionDialog dlg_color = new ColorSelectionDialog("Select color");

			dlg_color.Response += delegate (object o, ResponseArgs resp) 
			{
				if (resp.ResponseId == ResponseType.Ok) 
				{
					underline_color_selected =  dlg_color.ColorSelection.CurrentColor;
					string rgb = underline_color_selected.ToString();
					ApplyTextTags(textviewDemoText.Buffer, "underlinecolor:" + rgb);
				}
				if (resp.ResponseId == ResponseType.Cancel) 
				{
				}
				if (resp.ResponseId == ResponseType.Close) 
				{
				}
			};

			dlg_color.Run();
			dlg_color.Destroy();

			return;
		}

		protected void OnButtonFontColorClicked (object sender, EventArgs e)
		{
			DialogColorPickerOfficeLike dlg = new DialogColorPickerOfficeLike ();

			dlg.Response += delegate (object o, ResponseArgs resp) {
				if (resp.ResponseId == ResponseType.Ok) {
				}
			};

			return;
		}
	}
}

