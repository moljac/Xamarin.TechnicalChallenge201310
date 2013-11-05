using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace HttpMessageBus.EXE.Client.XamariniOS
{
	public partial class FirstViewController : UIViewController
	{
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public FirstViewController ()
			: base (UserInterfaceIdiomIsPhone ? "FirstViewController_iPhone" : "FirstViewController_iPad", null)
		{
			this.Title = NSBundle.MainBundle.LocalizedString ("First", "First");
			this.TabBarItem.Image = UIImage.FromBundle ("first");
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		string channel_current = null;
		HttpMessageBus.DLL_Client.Common.Client client = null;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.

			// Dismiss editing
			var tap = new UITapGestureRecognizer ();
			tap.AddTarget (() =>{

				this.View.EndEditing (true);
			});
			this.View.AddGestureRecognizer (tap);


			buttonSubscribe.TouchUpInside
					+= buttonSubscribe_Clicked;
			buttonUnsubscribe.TouchUpInside
					+= buttonUnsubscribe_Clicked;
			buttonNotify.TouchUpInside += buttonNotify_Clicked;

			client = new HttpMessageBus.DLL_Client.Common.Client ();

			this.textBoxPort.Text = client.Port.ToString();
			this.textBoxHostIPAddress.Text = client.HostIPAddress;

			client.ResponseStringChanged += HandleResponseStringChanged;

			return;
		}

		void buttonNotify_Clicked (object sender, EventArgs e)
		{
			if (null != this.textBoxChannel.Text) 
			{
				channel_current = this.textBoxChannel.Text;
				client.Notify (channel_current, this.textBoxMessages.Text);
			}

			return;
		}

		void HandleResponseStringChanged (object sender, EventArgs e)
		{
			string messages = client.ResponseString + Environment.NewLine + this.textBoxMessages.Text; 
			this.textBoxMessages.Text =  messages;

			return;
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) {
				return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
			} else {
				return true;
			}
		}


		protected void buttonSubscribe_Clicked (object sender, EventArgs e)
		{
			if (null != textBoxChannel.Text) 
			{
				channel_current = textBoxChannel.Text;
				client.Subscribe (channel_current);
			}

			return;
		}

		protected void buttonUnsubscribe_Clicked (object sender, EventArgs e)
		{
			if (null != textBoxChannel.Text) 
			{
				channel_current = textBoxChannel.Text;
				// remove in UI or handle on server
				client.Subscribe (channel_current);
			}

			return;
		}
	}
}

