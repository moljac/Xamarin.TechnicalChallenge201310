using System;

namespace GTKSharpTest.DLL.Widgets
{
	public partial class DialogColorPickerOfficeLike : Gtk.Dialog
	{
		//public global::Gtk.Color ColorSelected; 	// Gtk.Color  type or namespace Color does not exist in Gtk namespace
		public string ColorSelected; 	

		public DialogColorPickerOfficeLike ()
		{
			this.Build ();

			this.ShowAll ();
		}

		protected void OnButtonCancelClicked (object sender, EventArgs e)
		{
			this.Destroy ();
		}

		protected void OnButtonOkClicked (object sender, EventArgs e)
		{
			// return valiue
			this.Destroy ();

			return;
		}
	}
}

