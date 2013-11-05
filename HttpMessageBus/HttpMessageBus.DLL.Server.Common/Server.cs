using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.IO;

namespace HttpMessageBus.Server
{
	/// <summary>
	/// 
	/// </summary>
	/// <see cref="http://msdn.microsoft.com/en-us/library/ff647782.aspx"/>
	/// <see cref="http://msdn.microsoft.com/en-us/library/ff647801.aspx"/>
	/// <see cref="http://msdn.microsoft.com/en-us/library/windows/desktop/aa364510(v=vs.85).aspx"/>
	/// <see cref="http://msdn.microsoft.com/en-us/library/ms973839.aspx"/>
	/// <see cref="http://www.codeproject.com/Articles/137979/Simple-HTTP-Server-in-C"/>
	public partial class Server
	{
		//-------------------------------------------------------------------------
		# region Property string[] Prefixes w Event post (PrefixesChanged)
		/// <summary>
		/// Prefixes
		/// </summary>
		public
		  string[]
		  Prefixes
		{
			get
			{
				return prefixes;
			} // Prefixes.get
			set
			{
				//if (prefixes != value)		// do not write if equivalent/equal/same
				{
					// for multi threading apps uncomment lines beginnig with //MT:
					//MT: lock(prefixes) // MultiThread safe				
					{
						prefixes = value;
						if (null != PrefixesChanged)
						{
							PrefixesChanged(this, new EventArgs());
						}
					}
				}
			} // Prefixes.set
		} // Prefixes

		/// <summary>
		/// private member field for holding Prefixes data
		/// </summary>
		private
			string[]
			prefixes
			;

		///<summary>
		/// Event for wiring BusinessLogic object changes and presentation
		/// layer notifications.
		/// PrefixesChanged (<propertyname>Changed) is intercepted by Windows Forms
		/// 1.x and 2.0 event dispatcher 
		/// and for some CLR types by WPF event dispatcher 
		/// usually INotifyPropertyChanged and PropertyChanged event
		///</summary>
		public
			event
			EventHandler
			PrefixesChanged
			;
		# endregion Property string[] Prefixes w Event post (PrefixesChanged)
		//-------------------------------------------------------------------------

		//-------------------------------------------------------------------------
		# region Property bool Active w Event post (ActiveChanged)
		/// <summary>
		/// Active
		/// </summary>
		public
		  bool
		  Active
		{
			get
			{
				return active;
			} // Active.get
			set
			{
				//if (active != value)		// do not write if equivalent/equal/same
				{
					// for multi threading apps uncomment lines beginnig with //MT:
					//MT: lock(active) // MultiThread safe				
					{
						active = value;
						if (null != ActiveChanged)
						{
							ActiveChanged(this, new EventArgs());
						}
					}
				}
			} // Active.set
		} // Active

		/// <summary>
		/// private member field for holding Active data
		/// </summary>
		private
			bool
			active
			;

		///<summary>
		/// Event for wiring BusinessLogic object changes and presentation
		/// layer notifications.
		/// ActiveChanged (<propertyname>Changed) is intercepted by Windows Forms
		/// 1.x and 2.0 event dispatcher 
		/// and for some CLR types by WPF event dispatcher 
		/// usually INotifyPropertyChanged and PropertyChanged event
		///</summary>
		public
			event
			EventHandler
			ActiveChanged
			;
		# endregion Property bool Active w Event post (ActiveChanged)
		//-------------------------------------------------------------------------


		public bool Chunked = true;

		HttpListener listener = null;
		private readonly Func<string[]> ResponseDataBuild;


		/// <summary>
		/// Admin privileges needed - start Visual Studio or Xamarin Studio as Admin
		/// [System.Net.HttpListenerException] = {"Access is denied"}
		/// </summary>
		/// <param name="prefixes"></param>
		/// <param name="method"></param>
		/// 
		public Server(string[] prefixes, Func<string[]> method)
		{
			if (!HttpListener.IsSupported)
			{
				string msg = "Windows XP SP2, Server 2003 or later needed";
				throw new NotSupportedException(msg);
			}

			// URI prefixes are required, for example 
			// "http://localhost:8080/index/".
			if (prefixes == null || prefixes.Length == 0) {
				throw new ArgumentException ("prefixes");
			} 
			else 
			{
				List<string> prefixes_tmp = new List<string> ();
				foreach (string s in prefixes) {
					prefixes_tmp.Add (s);
				}
				this.Prefixes = prefixes_tmp.ToArray ();
			}

			// A responder method is required
			if (method == null)
			{
				throw new ArgumentException("method");
			}

			this.listener = new HttpListener();
			foreach (string s in prefixes)
			{
				listener.Prefixes.Add(s);
			}

			ResponseDataBuild = method;

			if (Chunked) {
				this.ProcessResponseSwitch += ProcessResponseChunked;
			} else {
				this.ProcessResponseSwitch += ProcessResponseSimple;
			}

			try
			{
				listener.Start();
			}
			catch (Exception exc)
			{
				string msg = exc.Message;
				System.Console.ForegroundColor = ConsoleColor.Red;
				System.Console.WriteLine("Exception: " + msg);
				System.Console.ForegroundColor = ConsoleColor.Yellow;
				System.Console.WriteLine("Start as Administrator (elevated)");
			}

			return;
		}

		public Server(Func<string[]> method, params string[] prefixes)
			: this(prefixes, method)
		{
			return;
		}

		static List<string> ip_addresses_local = null;
		static List<string> ip_addresses = null;

		public static string[] IPAddresses()
		{

			ip_addresses_local = new List<string>();
			ip_addresses = new List<string>();

			IPHostEntry host;
			host = Dns.GetHostEntry(Dns.GetHostName());

			foreach (IPAddress ip in host.AddressList)
			{
				ip_addresses.Add (ip.ToString ());

				if (ip.AddressFamily.ToString() == "InterNetwork")
				{
					ip_addresses_local.Add (ip.ToString ());
				}
			}

			return ip_addresses_local.ToArray();
		}


		public static string[] PrefixesLocal(int port)
		{
			List<string> prefixes = new List<string> ();

			foreach (string ipa in Server.IPAddresses()) 
			{
				prefixes.Add("http://" + ipa + ":" + port.ToString() + "/");
			}

			prefixes.Add("http://" + "localhost" + ":" + port.ToString() + "/");
			prefixes.Add("http://" + "127.0.0.1" + ":" + port.ToString() + "/");

			return prefixes.ToArray ();
		}



		public void Run()
		{
			Console.WriteLine("Server listening on:");
			foreach(string ip in this.Prefixes)
			{
				Console.WriteLine("\t \t" + ip);
			}

			ThreadPool.QueueUserWorkItem
				(
				  (o) =>
				  {
					  try
					  {
						  while (listener.IsListening)
						  {
							  ThreadPool.QueueUserWorkItem
								  (
									(context) =>
									{
										HttpListenerContext ctx = context as HttpListenerContext;
										ProcessRequest(ctx);
									}
								  , listener.GetContext()
								  );
						  }
					  }
					  catch (Exception exc)
					  {
						  string msg = exc.Message;
						  Console.WriteLine("Exception: {0}", msg);
					  };
				  }
			 );
		}


		static string request_data = null;
		string[] raw_url_parts = null;
		List<string> channels = new List<string>();
		static string request_post_data;
		static string client_user_ipendpoint;
		static string client_user_channel;
		static string client_command;


		private void ProcessRequest (HttpListenerContext context)
		{
			string raw_url = context.Request.RawUrl;
			raw_url_parts = raw_url.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

			HttpListenerRequest request = context.Request;

			client_user_ipendpoint	= request.RemoteEndPoint.ToString();

			switch (raw_url_parts.Length)
			{
				case 1:
					client_command = raw_url_parts[0];
					break;
				case 2:
					client_command = raw_url_parts[0];
					client_user_channel = raw_url_parts[1];
					break;
				default:
					return;
			}

			using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
			{
				request_post_data = reader.ReadToEnd();
			}

			string[] response_string_array = null;

			switch (client_command)
			{
				case "listen":
					ChannelCreateOrJoin(client_user_channel);
					break;
				case "notify":
					NotifyChannel(client_user_ipendpoint, client_user_channel, request_post_data, context);
					// content to be returned 
					response_string_array = ResponseDataBuild();
					break;
				case "report":
					string report = ReportChannelData(client_user_channel);
					context.Response.ContentType = 
									//"application/xml"
									"text/xml"
									;
					response_string_array =  report.Split(new string[]{Environment.NewLine}, StringSplitOptions.None);
					// content to be returned 
					break;
				default:
					break;
			}

			try
			{
				if (null != ProcessResponseSwitch && null != response_string_array)
				{
					ProcessResponseSwitch(context, response_string_array);
				}
				else
				{
					string msg = "Writing to response stream missing?!?!";
					Console.WriteLine(msg);
				}
			}
			catch (Exception exc)
			{
				string msg = exc.Message;
				Console.WriteLine("Exception: {0}", msg);
			}
			finally
			{
				// even for chunked?!!?
				context.Response.OutputStream.Close();
			}

			return;
		}

		private void ChannelCreateOrJoin(string p)
		{
			channels.Add(raw_url_parts[1]);

			return;
		}

		MessageBus MessageBus = new MessageBus();


		/// <summary>
		/// 1st design hold Context, so we can write data to other users
		/// 2nd attempt (TODO) hold NetworkStreams
		/// </summary>
		/// <param name="user_client"></param>
		/// <param name="channel"></param>
		/// <param name="message"></param>
		/// <param name="context"></param>
		private void NotifyChannel(string user_client, string channel, string message, HttpListenerContext context)
		{
			MessageBusChannel message_bus_channel =
					(
						from mb in MessageBus.MessageBusChannels
							where mb.Channel == channel
							select mb
					).FirstOrDefault();
			;

			if (null == message_bus_channel)
			{
				MessageBusChannel channel_new = new MessageBusChannel()
				{
					Channel = channel
				};
				channel_new.UserContexts.Add(context);
				channel_new.UsersClients.Add(user_client);

				this.MessageBus.MessageBusChannels.Add(channel_new);
			}
			else
			{
				if (message_bus_channel.UsersClients.Exists(uc => uc == user_client) == false)
				{
					message_bus_channel.UsersClients.Add(user_client);
					message_bus_channel.UserContexts.Add(context);
				}
				string[] message_response = SendResponseMessage();

				foreach (HttpListenerContext ctx in message_bus_channel.UserContexts)
				{
					Console.WriteLine("BROADCAST to" + ctx.Request.RemoteEndPoint.ToString());
					ProcessResponseChunked(ctx, message_response);
				}
			}


			return;
		}

		private string ReportChannelData(string p)
		{
			System.Xml.Serialization.XmlSerializer writer =
						new System.Xml.Serialization.XmlSerializer(typeof(MessageBus));

			MemoryStream ms = new MemoryStream();
			System.IO.StreamWriter content = new System.IO.StreamWriter(ms);
			writer.Serialize(ms, this.MessageBus);
			string result = Encoding.UTF8.GetString(ms.ToArray());

			return result;
		}


		private static string RequestData(HttpListenerContext ctx)
		{
			IPEndPoint ip_endpoint_remote = ctx.Request.RemoteEndPoint;
			Encoding content_encoding = ctx.Request.ContentEncoding;
			string content_type = ctx.Request.ContentType;
			System.Collections.Specialized.NameValueCollection headers = ctx.Request.Headers;
			string http_method_verb = ctx.Request.HttpMethod;
			bool is_authenticated = ctx.Request.IsAuthenticated;
			bool islocal = ctx.Request.IsLocal;
			IPEndPoint ip_endpoint_local = ctx.Request.LocalEndPoint;
			Version protocol_version = ctx.Request.ProtocolVersion;
			System.Collections.Specialized.NameValueCollection querystring = ctx.Request.QueryString;
			string raw_url = ctx.Request.RawUrl;
			string servicename = ctx.Request.ServiceName;
			TransportContext transport_context = ctx.Request.TransportContext;
			Uri url_referer = ctx.Request.UrlReferrer;
			string user_agent = ctx.Request.UserAgent;
			string user_hostaddress = ctx.Request.UserHostAddress;
			string user_hostname = ctx.Request.UserHostName;
			string[] languages = ctx.Request.UserLanguages;

			request_data = 
				Environment.NewLine +
				@" date-time			= " + DateTime.Now + Environment.NewLine + 
				@" ip_endpoint_remote	= " + ip_endpoint_remote.ToString() + Environment.NewLine + 
				@" ip_endpoint_local	= "	+ ip_endpoint_local.ToString() + Environment.NewLine + 
				@" content_encoding		= " + content_encoding + Environment.NewLine + 
				@" content_type			= " + content_type + Environment.NewLine + 
				@" http_method_verb		= " + http_method_verb + Environment.NewLine + 
				@" is_authenticated		= " + is_authenticated + Environment.NewLine + 
				@" islocal				= " + islocal + Environment.NewLine + 
				@" protocol_version		= " + protocol_version + Environment.NewLine + 
				@" raw_url				= " + raw_url + querystring + Environment.NewLine + 
				@" servicename			= " + servicename + Environment.NewLine + 
				@" url_referer			= " + url_referer + Environment.NewLine + 
				@" user_agent			= " + user_agent + Environment.NewLine + 
				@" user_hostaddress		= " + user_hostaddress + Environment.NewLine + 
				@" user_hostname		= " + user_hostname + Environment.NewLine + 
				@"----------------------------------------------"
				;	
			
			Console.WriteLine(@"\t\t HttpListenerContext.Request = " + request_data);

			return request_data;
		}


		/// <summary>
		/// Switching for Stream write (Function ptr simulation)
		/// </summary>
		/// <param name="c"></param>
		/// <param name="r"></param>
		public delegate void StreamWriteMethodSwitchDelegate(HttpListenerContext c, string[] r);

		/// <summary>
		/// Initialize this in ctor or some init function or before calling Run()
		/// </summary> 
		public event StreamWriteMethodSwitchDelegate ProcessResponseSwitch;

		public void ProcessResponseSimple(HttpListenerContext ctx, string[] response_string_array)
		{
			ReportDebugInfoSimple(ctx);

			try
			{
				string response_string = string.Join("", response_string_array);
				byte[] buf = Encoding.UTF8.GetBytes(response_string);
				ctx.Response.ContentLength64 = buf.Length;
				ctx.Response.OutputStream.Write(buf, 0, buf.Length);
				ctx.Response.OutputStream.Close();
			}
			catch (Exception exc) 
			{
				string msg = exc.Message;
				Console.WriteLine ("\t\t Stream Response Normal write Exception = " + msg);
			}

			return;
		}


		public void ProcessResponseChunked(HttpListenerContext ctx, string[] response_string_array)
		{
			Console.WriteLine ("Stream write chunked");

			ReportDebugInfoChunked (ctx);


			int i = 1;
			foreach (string response_chunk in response_string_array)
			{
				Console.WriteLine ("Stream write chunk[{0}] = " + response_chunk, i);
				byte[] buf = Encoding.UTF8.GetBytes(response_chunk);
				// context.Response.ContentLength64 = buf.Length; // Note: no length in chunked response
				try
				{
					ctx.Response.OutputStream.Write(buf, 0, buf.Length);
					// Note: Flushing causes chunked transfer!
					ctx.Response.OutputStream.Flush();
				}
				catch(Exception exc) 
				{
					string msg = exc.Message;
					Console.WriteLine ("\t\t Stream Response Chunked write Exception = " + msg);
					Console.WriteLine ("\t\t chunk[{0}] = " + response_chunk, i);
				}
			
				i++;
			}

			return;
		}

		public void Init()
		{
			if (!HttpListener.IsSupported)
			{
				Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
				return;
			}
			// URI prefixes are required, 
			// for example "http://contoso.com:8080/index/".
			if (prefixes == null || prefixes.Length == 0)
				throw new ArgumentException("prefixes");

			listener = new HttpListener();

			// Add the prefixes. 
			foreach (string s in prefixes)
			{
				listener.Prefixes.Add(s);
			}
			listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;

			return;
		}


		public void Stop()
		{
			try
			{
				listener.Stop();
				listener.Close();
				listener = null;
			}
			catch (Exception exc)
			{
				string msg = exc.Message;

				Console.WriteLine("Exception = " + msg);
			}

			return;
		}

		private static void ReportDebugInfoSimple(HttpListenerContext ctx)
		{
			if (HttpMessageBusSettings.DebugMode)
			{
				byte[] buf = Encoding.UTF8.GetBytes(request_data);
				// context.Response.ContentLength64 = buf.Length; // Note: no length in chunked response
				try
				{
					ctx.Response.OutputStream.Write(buf, 0, buf.Length);
					ctx.Response.OutputStream.Close();
				}
				catch (Exception exc)
				{
					string msg = exc.Message;
					Console.WriteLine("\t\t Stream Response Chunked write Exception = " + msg);
					Console.WriteLine("\t\t request_data[{0}] = " + request_data);
				}
			}

			return;
		}

		private static void ReportDebugInfoChunked(HttpListenerContext ctx)
		{
			if (HttpMessageBusSettings.DebugMode)
			{
				byte[] buf = Encoding.UTF8.GetBytes(request_data);
				// context.Response.ContentLength64 = buf.Length; // Note: no length in chunked response
				try
				{
					// SendChunked + Flush
					ctx.Response.SendChunked = true;
					ctx.Response.OutputStream.Write(buf, 0, buf.Length);
					// Note: Flushing causes chunked transfer!
					ctx.Response.OutputStream.Flush();
				}
				catch (Exception exc)
				{
					string msg = exc.Message;
					Console.WriteLine("\t\t Stream Response Chunked write Exception = " + msg);
					Console.WriteLine("\t\t request_data[{0}] = " + request_data);
				}
			}

			return;
		}

		public static string[] SendResponseMessage()
		{
			List<string> response = new List<string>();

			response.Add("Message on channel =" + client_user_channel);
			response.Add("              time =" + DateTime.Now.ToString());
			response.Add("    user client ip =" + client_user_ipendpoint);
			response.Add("           message =" + request_post_data);

			return response.ToArray();
		}

		public static string[] SendResponseSimple()
		{
			string[] response = new string[]
			{
			  @"<HTML><BODY>"
			, string.Format(@"Webpage response = {0}",DateTime.Now)
			, @"</BODY></HTML>"
			};

			return response;
		}

		public static string[] SendResponseHtmlFile()
		{
			string[] response = null;
			try
			{
				//string path = Path.Combine("samples", "HTML-Ipsum.htm");
				string path = Path.Combine("samples", "HTML5 Reference.html");

				response = System.IO.File.ReadAllLines(path);
			}
			catch (Exception e)
			{
				Console.WriteLine("The content could not be read:");
				Console.WriteLine(e.Message);
			}

			return response;
		}

		public static string[] SendResponseDebug()
		{
			List<string> debug_info = new List<string>();
			debug_info.Add(request_data);

			return debug_info.ToArray();
		}
	}
}
