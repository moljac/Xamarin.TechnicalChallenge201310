// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;
using System.CodeDom.Compiler;

namespace HttpMessageBus.EXE.Client.MonoMac
{
	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		global::MonoMac.AppKit.NSButton buttonNotify { get; set; }

		[Outlet]
		global::MonoMac.AppKit.NSButton buttonSubscribe { get; set; }

		[Outlet]
		global::MonoMac.AppKit.NSButton buttonUnsubscribe { get; set; }

		[Outlet]
		global::MonoMac.AppKit.NSComboBox comboboxChannel { get; set; }

		[Outlet]
		global::MonoMac.AppKit.NSTextField textBoxHostIPAddress { get; set; }

		[Outlet]
		global::MonoMac.AppKit.NSTextField textBoxMessage { get; set; }

		[Outlet]
		global::MonoMac.AppKit.NSTextField textBoxMessages { get; set; }

		[Outlet]
		global::MonoMac.AppKit.NSTextField textBoxPort { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (buttonNotify != null) {
				buttonNotify.Dispose ();
				buttonNotify = null;
			}

			if (buttonSubscribe != null) {
				buttonSubscribe.Dispose ();
				buttonSubscribe = null;
			}

			if (buttonUnsubscribe != null) {
				buttonUnsubscribe.Dispose ();
				buttonUnsubscribe = null;
			}

			if (comboboxChannel != null) {
				comboboxChannel.Dispose ();
				comboboxChannel = null;
			}

			if (textBoxHostIPAddress != null) {
				textBoxHostIPAddress.Dispose ();
				textBoxHostIPAddress = null;
			}

			if (textBoxPort != null) {
				textBoxPort.Dispose ();
				textBoxPort = null;
			}

			if (textBoxMessage != null) {
				textBoxMessage.Dispose ();
				textBoxMessage = null;
			}

			if (textBoxMessages != null) {
				textBoxMessages.Dispose ();
				textBoxMessages = null;
			}
		}
	}

	[Register ("MainWindow")]
	partial class MainWindow
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
