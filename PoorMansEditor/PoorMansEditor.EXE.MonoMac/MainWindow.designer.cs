// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;
using System.CodeDom.Compiler;

namespace PoorMansEditor.EXE
{
	[Register ("MainWindow")]
	partial class MainWindow
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}

	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		MonoMac.AppKit.NSButton buttonHTML { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton buttonPDF { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton buttonPNG { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextView textViewMarkDown { get; set; }

		[Outlet]
		MonoMac.WebKit.WebView webViewMarkUp { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (textViewMarkDown != null) {
				textViewMarkDown.Dispose ();
				textViewMarkDown = null;
			}

			if (webViewMarkUp != null) {
				webViewMarkUp.Dispose ();
				webViewMarkUp = null;
			}

			if (buttonHTML != null) {
				buttonHTML.Dispose ();
				buttonHTML = null;
			}

			if (buttonPDF != null) {
				buttonPDF.Dispose ();
				buttonPDF = null;
			}

			if (buttonPNG != null) {
				buttonPNG.Dispose ();
				buttonPNG = null;
			}
		}
	}
}
