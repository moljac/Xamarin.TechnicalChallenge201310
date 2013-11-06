using System;

using System.Net;
using System.IO;
using System.Text;

namespace HttpMessageBus.DLL_Client.Common
{
	/// <summary>
	/// 
	/// </summary>
	/// <see cref="http://msdn.microsoft.com/en-us/library/ff647782.aspx"/>
	/// <see cref="http://msdn.microsoft.com/en-us/library/ff647801.aspx"/>
	/// <see cref="http://zoompf.com/blog/2012/05/too-chunky"/>
	/// <see cref="http://stackoverflow.com/questions/16998/reading-chunked-response-with-httpwebresponse"/>
	/// <see cref="http://stackoverflow.com/questions/10006669/c-sharp-using-webclient-to-download-chunked-encoded-content"/>
	/// <see cref="http://blog.degree.no/2011/06/webclient-httpwebresponse-problems-with-chunked-transfer-encoding/"/>
	/// <see cref="http://social.msdn.microsoft.com/Forums/en-US/4f28d99d-9794-434b-8b78-7f9245c099c4/problems-with-httpwebrequest-and-transferencoding-chunked"/>
	/// 
	public partial class Client
	{
		public string HostIPAddress = HttpMessageBusSettings.Host;
		public int Port = HttpMessageBusSettings.Port;

		// no HttpClient

		// WebRequest instance itself is sufficient to send data. However, if 
		// user needs to set protocol-specific properties, user must cast the WebRequest 
		// to the protocol-specific type
		// web_request = System.Net.WebRequest.Create(url);
		// System.Net.WebRequest web_request = null;

		HttpWebRequest http_web_request = null; 

		System.Net.WebResponse response = null;

//		StreamWriter stream_writer_http_web_request = null;
//		StreamWriter stream_writer_web_request = null;

		Stream stream_data = null;
		Encoding encoding = Encoding.ASCII;
		int bufsize = 16 * 1024;

		public Client ()
		{

			return;
		}

		string channel_current = null;
		string url = null;
		string operation = "";
		string[] operations = new string[]{"listen", "notify"};

		public void Subscribe(string channel)
		{
			url = "http://" + HostIPAddress + ":" + Port.ToString () + "/"; 
			channel_current = channel;		
			url =  url + "/" + operation + "/" + channel_current;

			try
			{
				http_web_request = (HttpWebRequest)WebRequest.Create(url);
			}
			catch (Exception exc)
			{
				Console.WriteLine ("(HttpWebRequest)WebRequest.Create(url) error = " + exc.Message);
			}
			http_web_request.Method = "POST";
			//http_web_request.Credentials = CredentialCache.DefaultCredentials;
			http_web_request.ContentType = "application/x-www-form-urlencoded";
			//http_web_request.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
			http_web_request.UserAgent = "Xamarin Tech Challenge Client";

			//http_web_request.ContentLength = bytes.Length;
//			stream_writer_http_web_request = new StreamWriter 
//			                                 	(
//				                                  http_web_request.GetRequestStream ()
//				                                , encoding
//				                          		);

//			stream_writer_web_request = new StreamWriter 
//			                            		(
//				                            	  web_request.GetRequestStream ()
//				                            	, encoding
//				                           		);

			return;
		}

		public void Unsubscribe(string channel)
		{
			stream_data.Close ();
			stream_data = null;
			http_web_request = null;

			return;
		}

		public void Notify(string channel, string message)
		{
			url = "http://" + HostIPAddress + ":" + Port.ToString () + "/"; 
			operation = "notify";
			channel_current = channel;		
			url =  url + "/" + operation + "/" + channel_current;


			// A new 'HttpWebRequest' object is created.
			HttpWebRequest http_web_request=(HttpWebRequest)WebRequest.Create(url);
			http_web_request.SendChunked = true;	// chunked transfer
			http_web_request.KeepAlive = true;		// persistent connections

			// 'TransferEncoding' property is set to 'gzip'.
			http_web_request.TransferEncoding="gzip";
			Console.WriteLine("\nPlease Enter the data to be posted to the uri:" + url);
			string inputData = message;
			string postData= 
					"message=" + inputData 
					+ Environment.NewLine + 
					"port=" + HttpMessageBusSettings.PortClientListener
				 	;
			// 'Method' property of 'HttpWebRequest' class is set to POST.
			http_web_request.Method="POST";
			ASCIIEncoding encodedData=new ASCIIEncoding();
			byte[]  byteArray=encodedData.GetBytes(postData);
			// 'ContentType' property of the 'HttpWebRequest' class is set to "application/x-www-form-urlencoded".
			http_web_request.ContentType="application/x-www-form-urlencoded";
			// 'ContentLength' property is set to Length of the data to be posted.
			http_web_request.ContentLength=byteArray.Length;

			try
			{

				Stream stream_request=http_web_request.GetRequestStream();
				stream_request.Write(byteArray,0,byteArray.Length);
				stream_request.Close();
				Console.WriteLine("\nData has been posted to the Uri\n\nPlease wait for the response..........");

				this.ResponseString = ResponseReadForRequest (http_web_request);
			}
			catch (Exception exc)
			{
				string msg = exc.Message;
				this.ResponseString = msg;
			}

			return;
		}

		static string ResponseReadForRequest (HttpWebRequest http_web_request)
		{
			// The response object of 'HttpWebRequest' is assigned to a 'HttpWebResponse' variable.
			HttpWebResponse http_web_response = (HttpWebResponse)http_web_request.GetResponse ();
			// Displaying the contents of the page to the console
			Stream stream_response = http_web_response.GetResponseStream ();
			StreamReader stream_reader = new StreamReader (stream_response);

			int buffer_capacity = 32 * 1024;
			Char[] buffer_read = new Char[buffer_capacity];
			int count = stream_reader.Read (buffer_read, 0, buffer_capacity);
			Console.WriteLine ("\nThe contents of the HTML page are :  ");

			String data_ouput = null;
			while (count > 0)
			{
				data_ouput = new String (buffer_read, 0, count);
				Console.WriteLine (data_ouput);
				count = stream_reader.Read (buffer_read, 0, buffer_capacity);
			}
			// Release the response object resources.
			stream_reader.Close ();
			stream_response.Close ();
			http_web_response.Close ();

			return data_ouput;
		}

		public void Notify(string message)
		{
			//byte[] bytes = null; 

			//bytes = encoding.GetBytes (message);
			//stream_data = http_web_request.GetRequestStream ();
			//stream_data.Write (bytes, 0, bytes.Length);

		}

		//-------------------------------------------------------------------------
		# region Property string ResponseString w Event post (ResponseStringChanged)
		/// <summary>
		/// ResponseString
		/// </summary>
		public
		  string
		  ResponseString
		{
			get
			{
				return response_string;
			} // ResponseString.get
			set
			{
				//if (response_string != value)		// do not write if equivalent/equal/same
				{
					// for multi threading apps uncomment lines beginnig with //MT:
					//MT: lock(response_string) // MultiThread safe				
					{
						response_string = value;
						if (null != ResponseStringChanged)
						{
							ResponseStringChanged(this, new EventArgs());
						}
					}
				}
			} // ResponseString.set
		} // ResponseString

		/// <summary>
		/// private member field for holding ResponseString data
		/// </summary>
		private
			string
			response_string
			;

		///<summary>
		/// Event for wiring BusinessLogic object changes and presentation
		/// layer notifications.
		/// ResponseStringChanged (<propertyname>Changed) is intercepted by Windows Forms
		/// 1.x and 2.0 event dispatcher 
		/// and for some CLR types by WPF event dispatcher 
		/// usually INotifyPropertyChanged and PropertyChanged event
		///</summary>
		public
			event
			EventHandler
			ResponseStringChanged
			;
		# endregion Property string ResponseString w Event post (ResponseStringChanged)
		//-------------------------------------------------------------------------	

	}
}

