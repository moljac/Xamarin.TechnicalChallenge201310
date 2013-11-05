using System;
using Gtk;
using WebKit;

namespace PoorMansEditor.EXE
{
public partial class MainWindow: Gtk.Window
{
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		// Build ();
		/*
		Gtk.Application.Init ();
		Gtk.Window win = new Gtk.Window ("Title");
		Mono.WebBrowser.IWebBrowser browser =
			Mono.WebBrowser.Manager.GetNewInstance(Mono.WebBrowser.Platform.Gtk);
		browser.Load(win.Handle, 500, 250);
		win.ShowAll ();
		GLib.Timeout.Add( 500, delegate {
			browser.Navigation.Go ("http://google.com/");
			return false;
		});
		Gtk.Application.Run ();
		*/

		Application.Init ();
		Window window = new Window ("a browser in 13 lines...");
		window.SetDefaultSize(800,600);
		window.Destroyed += delegate (object sender, EventArgs e) {
			Application.Quit ();
		};
		ScrolledWindow scrollWindow = new ScrolledWindow ();
		WebView webView = new WebView ();
		webView.Open ("http://mono-project.com");
		scrollWindow.Add (webView);
		window.Add (scrollWindow);
		window.ShowAll ();
		Application.Run ();
		/*
		*/
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
}
