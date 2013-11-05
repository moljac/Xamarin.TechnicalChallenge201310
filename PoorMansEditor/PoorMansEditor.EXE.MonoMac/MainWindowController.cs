using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using System.IO;

namespace PoorMansEditor.EXE
{
	public partial class MainWindowController : MonoMac.AppKit.NSWindowController
	{

		#region Constructors

		// Called when created from unmanaged code
		public MainWindowController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public MainWindowController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		// Call to load from the XIB/NIB file
		public MainWindowController () : base ("MainWindow")
		{
			Initialize ();
		}
		// Shared initialization code
		void Initialize ()
		{

		}

		#endregion

		//strongly typed window accessor
		public new MainWindow Window {
			get {
				return (MainWindow)base.Window;
			}
		}

		string text = "";

		public override void WindowDidLoad ()
		{
			base.WindowDidLoad ();

			string path = System.IO.Path.GetDirectoryName (System.Reflection.Assembly.GetExecutingAssembly ().Location);
			string[] lines = System.IO.File.ReadAllLines(Path.Combine(path,@"SampleMarkDown.txt"));

			text = string.Join (Environment.NewLine, lines);
			textViewMarkDown.Value = text;

			NSUrl nsurl = new NSUrl ("http://holisticware.net");
			NSUrlRequest nsurlrequest = new NSUrlRequest (nsurl);
			webViewMarkUp.MainFrame.LoadRequest (nsurlrequest);

			buttonHTML.Activated += buttonHTML_HandleActivated;
			buttonPDF.Activated += buttonPDF_HandleActivated;
			buttonPNG.Activated += buttonPNG_HandleActivated;

			return;
		}

		private void buttonHTML_HandleActivated (object sender, EventArgs e)
		{

			// Load the HTML document
			string html_path = Path.Combine (NSBundle.MainBundle.ResourcePath, "result.template.html");
			//webViewMarkUp.MainFrame.LoadRequest(new NSUrlRequest (new NSUrl (htmlPath)));
		

			global::PoorMansEditor.Common.TextParser parser = null;
			parser = new global::PoorMansEditor.Common.TextParser();

			string html_converted = parser.PoorManEditorText2Html(text);

			string placeholder = "<div>$content$</div>";
			string html_content = System.IO.File.ReadAllText(html_path);
			html_content = html_content.Replace (placeholder, html_converted);


			webViewMarkUp.MainFrame.LoadHtmlString (html_content, null);

			string html_result_path = Path.Combine (NSBundle.MainBundle.ResourcePath, "result.html");
			System.IO.File.WriteAllText(html_result_path, html_content);

			System.Diagnostics.Process.Start (html_result_path);

			return;
		}

		private void buttonPDF_HandleActivated (object sender, EventArgs e)
		{

			return;
		}

		private void buttonPNG_HandleActivated (object sender, EventArgs e)
		{

			return;
		}
	}
}

