using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HttpMessageBus.EXE_Client.WPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		string channel_current = null;
		HttpMessageBus.DLL_Client.Common.Client client = null;


		public MainWindow()
		{
			InitializeComponent();

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
