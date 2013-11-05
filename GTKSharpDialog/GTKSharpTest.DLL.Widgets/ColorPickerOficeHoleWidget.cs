using System;
using HollyLibrary;

namespace GTKSharpTest.DLL.Widgets
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class ColorPickerOficeHoleWidget : Gtk.Bin
	{
		HColorPicker colorPicker;

		public ColorPickerOficeHoleWidget ()
		{
			colorPicker = new HColorPicker ();
			colorPicker.Color = new Gdk.Color( 255, 0, 125 );

			this.Build ();


		}
	}
}

