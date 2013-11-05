using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace HttpMessageBus.EXE.Client.XamarinAndroid.VS
{
	[Activity(Label = "HttpMessageBus.EXE.Client", MainLauncher = true, Icon = "@drawable/icon")]
	public class Activity1 : Activity
	{
		Button buttonSubscribe = null;
		Button buttonUnsubscribe = null;
		Button buttonNotify = null;
		EditText textBoxMessage = null;
		EditText textBoxMessages = null;
		EditText textBoxHostIPAddress = null;
		EditText textBoxPort = null;
		EditText textBoxChannel = null;

		string channel_current = null;
		HttpMessageBus.DLL_Client.Common.Client client = null;
		
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			buttonSubscribe = FindViewById<Button>(Resource.Id.buttonSubscribe);
			buttonUnsubscribe = FindViewById<Button>(Resource.Id.buttonUnsubscribe);
			buttonNotify = FindViewById<Button>(Resource.Id.buttonNotify);

			textBoxPort = FindViewById<EditText>(Resource.Id.textBoxPort);
			textBoxHostIPAddress = FindViewById<EditText>(Resource.Id.textBoxHostIPAddress);
			textBoxMessage = FindViewById<EditText>(Resource.Id.textBoxMessage);
			textBoxMessages = FindViewById<EditText>(Resource.Id.textBoxMessages);
			textBoxChannel = FindViewById<EditText>(Resource.Id.textBoxChannel);

			buttonSubscribe.Click	
					+= buttonSubscribe_Clicked;
			buttonUnsubscribe.Click
					+= buttonUnsubscribe_Clicked;
			buttonNotify.Click
					+= buttonNotify_Clicked;


			client = new HttpMessageBus.DLL_Client.Common.Client();

			this.textBoxPort.Text = client.Port.ToString();
			this.textBoxHostIPAddress.Text = client.HostIPAddress;

			client.ResponseStringChanged += HandleResponseStringChanged;
			
			return;
		}


		void HandleResponseStringChanged(object sender, EventArgs e)
		{
			string messages = client.ResponseString + System.Environment.NewLine + this.textBoxMessages.Text;
			this.textBoxMessages.Text = messages;

			return;
		}

		protected void buttonNotify_Clicked(object sender, EventArgs e)
		{
			if (null != textBoxChannel.Text)
			{
				channel_current = textBoxChannel.Text;
				client.Notify(channel_current, textBoxMessage.Text);
			}

			return;
		}

		protected void buttonSubscribe_Clicked(object sender, EventArgs e)
		{
			if (null != textBoxChannel.Text)
			{
				channel_current = textBoxChannel.Text;
				client.Subscribe(channel_current);
			}

			return;
		}

		protected void buttonUnsubscribe_Clicked(object sender, EventArgs e)
		{
			if (null != textBoxChannel.Text)
			{
				channel_current = textBoxChannel.Text;
				// remove in UI or handle on server
				client.Unsubscribe(channel_current);
			}

			return;
		}

	}
}

