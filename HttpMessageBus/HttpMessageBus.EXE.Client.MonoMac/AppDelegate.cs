using System;
using System.Drawing;
using MonoMac.Foundation;
using global::MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace HttpMessageBus.EXE.Client.MonoMac
{
	public partial class AppDelegate : NSApplicationDelegate
	{
		MainWindowController mainWindowController;

		public AppDelegate ()
		{
		}

		public override void FinishedLaunching (NSObject notification)
		{
			mainWindowController = new MainWindowController ();
			mainWindowController.Window.MakeKeyAndOrderFront (this);
		}
	}
}

