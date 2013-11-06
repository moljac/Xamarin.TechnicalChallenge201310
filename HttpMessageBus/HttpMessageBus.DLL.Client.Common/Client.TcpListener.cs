using System;

using System.Net;
using System.IO;
using System.Text;

namespace HttpMessageBus.DLL_Client.Common
{
	/// <summary>
	/// 
	/// </summary>
	public partial class Client
	{
		private System.Threading.Thread listen_thread = null;
		private System.Threading.ThreadStart thread_start = null;
		private System.Net.Sockets.TcpListener server = null;
		bool IsListening = true;

		public void StartTcpListener()
		{
			Int32 port = HttpMessageBusSettings.PortClientListener;
			IPAddress localAddr = IPAddress.Parse("127.0.0.1");

			// TcpListener server = new TcpListener(port);
			server = new System.Net.Sockets.TcpListener(IPAddress.Any, port);

			// Start listening for client requests.
			server.Start();

			thread_start = new System.Threading.ThreadStart(ListenForClients);
			this.listen_thread = new System.Threading.Thread(thread_start);
			this.listen_thread.Start();

			return;
		}

		private void ListenForClients()
		{
			this.server.Start();

			while (IsListening)
			{
				//blocks until a client has connected to the server
				System.Net.Sockets.TcpClient client = this.server.AcceptTcpClient();

				//create a thread to handle communication 
				//with connected client
				System.Threading.Thread client_thread = null;
				client_thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(HandleClientComm));
				client_thread.Start(client);
			}

			return;
		}

		private void HandleClientComm(object client)
		{
			System.Net.Sockets.TcpClient tcp_client = (System.Net.Sockets.TcpClient)client;
			System.Net.Sockets.NetworkStream clientStream = tcp_client.GetStream();

			byte[] message = new byte[4096];
			int bytesRead;

			while (true)
			{
				bytesRead = 0;

				try
				{
					//blocks until a client sends a message
					bytesRead = clientStream.Read(message, 0, 4096);
				}
				catch
				{
					//a socket error has occured
					break;
				}

				if (bytesRead == 0)
				{
					//the client has disconnected from the server
					break;
				}

				//message has successfully been received
				ASCIIEncoding encoder = new ASCIIEncoding();
				System.Diagnostics.Debug.WriteLine(encoder.GetString(message, 0, bytesRead));
			}

			TcpListenerSendResponseToClient(tcp_client, "message for client");

			tcp_client.Close();
		}

		// not needed
		private void TcpListenerSendResponseToClient(System.Net.Sockets.TcpClient tcp_client, string message)
		{
			System.Net.Sockets.NetworkStream client_stream = tcp_client.GetStream();
			ASCIIEncoding encoder = new ASCIIEncoding();
			byte[] buffer = encoder.GetBytes(message);

			client_stream.Write(buffer, 0, buffer.Length);
			client_stream.Flush();

			return;
		}
	}
}