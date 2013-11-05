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
	[Register ("SecondViewController")]
	partial class SecondViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITextView textBoxMessages { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (textBoxMessages != null) {
				textBoxMessages.Dispose ();
				textBoxMessages = null;
			}
		}
	}
}
