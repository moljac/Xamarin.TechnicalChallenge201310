using System;
using Gtk;

using System.Net;
using System.IO;

using HttpMessageBus.DLL_Client.Common;

public partial class MainWindow: Gtk.Window
{
	string channel_current = null;
	Client client = null;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		buttonSubscribe.Clicked 
				+= buttonSubscribe_Clicked;
		buttonUnsubscribe.Clicked 
				+= buttonUnsubscribe_Clicked;
		buttonNotify.Clicked
				+= buttonNotify_Clicked; 


		client = new HttpMessageBus.DLL_Client.Common.Client ();

		this.textBoxPort.Text = client.Port.ToString();
		this.textBoxHostIPAddress.Text = client.HostIPAddress;

		client.ResponseStringChanged += HandleResponseStringChanged;

		return;
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{

		Application.Quit ();
		a.RetVal = true;
	}

	void HandleResponseStringChanged (object sender, EventArgs e)
	{
		string messages = client.ResponseString + Environment.NewLine + this.textBoxMessages.Buffer.Text; 
		this.textBoxMessages.Buffer.Text =  messages;

		return;
	}

	protected void buttonNotify_Clicked (object sender, EventArgs e)
	{
		if (null != comboboxChannel.ActiveText) 
		{
			channel_current = comboboxChannel.ActiveText;
			client.Notify (channel_current, textBoxMessage.Text);
		}

		return;
	}

		protected void buttonSubscribe_Clicked (object sender, EventArgs e)
	{
		if (null != comboboxChannel.ActiveText) 
		{
			channel_current = comboboxChannel.ActiveText;
			client.Subscribe (channel_current);
		}

		return;
	}

	protected void buttonUnsubscribe_Clicked (object sender, EventArgs e)
	{
		if (null != comboboxChannel.ActiveText) 
		{
			channel_current = comboboxChannel.ActiveText;
			// remove in UI or handle on server
			client.Unsubscribe (channel_current);
		}

		return;
	}
}
