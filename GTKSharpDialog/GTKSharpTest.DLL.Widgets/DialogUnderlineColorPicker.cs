using System;

namespace GTKSharpTest.DLL.Widgets
{
	public partial class DialogUnderlineColorPicker : Gtk.Dialog
	{
		public DialogUnderlineColorPicker ()
		{
			this.Build ();
		}

		protected void OnButtonCancelClicked (object sender, EventArgs e)
		{
			this.Destroy ();
		}
	}
}

