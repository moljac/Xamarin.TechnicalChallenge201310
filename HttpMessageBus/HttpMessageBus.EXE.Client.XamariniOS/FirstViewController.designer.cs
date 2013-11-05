// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace HttpMessageBus.EXE.Client.XamariniOS
{
	[Register ("FirstViewController")]
	partial class FirstViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton buttonNotify { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton buttonSubscribe { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton buttonUnsubscribe { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField textBoxChannel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField textBoxHostIPAddress { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextView textBoxMessages { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField textBoxPort { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (buttonSubscribe != null) {
				buttonSubscribe.Dispose ();
				buttonSubscribe = null;
			}

			if (buttonUnsubscribe != null) {
				buttonUnsubscribe.Dispose ();
				buttonUnsubscribe = null;
			}

			if (textBoxChannel != null) {
				textBoxChannel.Dispose ();
				textBoxChannel = null;
			}

			if (textBoxHostIPAddress != null) {
				textBoxHostIPAddress.Dispose ();
				textBoxHostIPAddress = null;
			}

			if (textBoxMessages != null) {
				textBoxMessages.Dispose ();
				textBoxMessages = null;
			}

			if (textBoxPort != null) {
				textBoxPort.Dispose ();
				textBoxPort = null;
			}

			if (buttonNotify != null) {
				buttonNotify.Dispose ();
				buttonNotify = null;
			}
		}
	}
}
