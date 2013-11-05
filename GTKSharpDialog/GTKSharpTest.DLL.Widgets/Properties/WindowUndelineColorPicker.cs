using System;

namespace GTKSharpTest.DLL.Widgets
{
	public partial class WindowUndelineColorPicker : Gtk.Window
	{
		public WindowUndelineColorPicker () : 
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}

		protected void OnButtonOKClicked (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}
	}
}

