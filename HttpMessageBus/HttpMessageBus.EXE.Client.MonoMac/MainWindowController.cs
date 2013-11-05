using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using global::MonoMac.AppKit;


using HttpMessageBus.DLL_Client.Common;

namespace HttpMessageBus.EXE.Client.MonoMac
{
	public partial class MainWindowController : global::MonoMac.AppKit.NSWindowController
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


		string channel_current = null;
		HttpMessageBus.DLL_Client.Common.Client client = null;

		public override void WindowDidLoad ()
		{
			base.WindowDidLoad ();

			buttonSubscribe.Activated 
					+= buttonSubscribe_Clicked;
			buttonUnsubscribe.Activated
					+= buttonUnsubscribe_Clicked;
			buttonNotify.Activated 
					+= buttonNotify_Clicked;


			client = new HttpMessageBus.DLL_Client.Common.Client ();

			this.textBoxPort.StringValue = client.Port.ToString();
			this.textBoxHostIPAddress.StringValue = client.HostIPAddress;

			client.ResponseStringChanged += HandleResponseStringChanged;

			return;
		}

		void HandleResponseStringChanged (object sender, EventArgs e)
		{
			string messages = client.ResponseString + Environment.NewLine + this.textBoxMessages.StringValue; 
			this.textBoxMessages.StringValue =  messages;

			return;
		}

		protected void buttonNotify_Clicked (object sender, EventArgs e)
		{
			if (null != comboboxChannel.StringValue) 
			{
				channel_current = comboboxChannel.StringValue;
				client.Notify (channel_current, textBoxMessage.StringValue);
			}

			return;
		}

		protected void buttonSubscribe_Clicked (object sender, EventArgs e)
		{
			if (null != comboboxChannel.StringValue) 
			{
				channel_current = comboboxChannel.StringValue;
				client.Subscribe (channel_current);
			}

			return;
		}

		protected void buttonUnsubscribe_Clicked (object sender, EventArgs e)
		{
			if (null != comboboxChannel.StringValue) 
			{
				channel_current = comboboxChannel.StringValue;
				// remove in UI or handle on server
				client.Unsubscribe (channel_current);
			}

			return;
		}
	}
}

