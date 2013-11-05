using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HttpMessageBus.EXE.Server.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			string[] ips_local = HttpMessageBus.Server.Server.IPAddresses();
			string[] ips = null;

			if (args.Length == 1)
			{
				ips = HttpMessageBus.Server.Server.PrefixesLocal(Convert.ToInt32(args[0]));
			}
			else
			{
				ips = HttpMessageBus.Server.Server.PrefixesLocal(HttpMessageBusSettings.Port);
			}

			//Console App
			// Set in Project +/ Properties +/ Application +/ Output Type = "Console Application"


			// 127.0.0.1 and localhost needed otherwise
			// Bad Request - Invalid Hostname
			// HTTP Error 400. The request hostname is invalid.
			string[] ip_addresses_local = null;
			ip_addresses_local = new string[]
			{
			//	@"http://127.0.0.1:" + HttpMessageBusSettings.Port + "/"
			//	, @"http://localhost:" + HttpMessageBusSettings.Port + "/"
			};

			string[] prefixes = ips.Concat(ip_addresses_local).ToArray();

			HttpMessageBus.Server.Server server = null;
			server = new HttpMessageBus.Server.Server
					(
					  HttpMessageBus.Server.Server.SendResponseMessage		// content to be returned
					// HttpMessageBus.Server.Server.SendResponseDebug		// content to be returned
					// HttpMessageBus.Server.Server.SendResponseHtmlFile	// content to be returned
					// HttpMessageBus.Server.Server.SendResponseHtmlFile	// content to be returned
					, prefixes
					);

			// Chunked is default so remove to test non chunked 
			// server.ProcessResponseSwitch -= server.ProcessResponseChunked;
			// server.ProcessResponseSwitch += server.ProcessResponseSimple;

			server.Run();
			System.Console.WriteLine("HttpMessageBus server. Press a key to quit.");
			System.Console.ReadKey();
			server.Stop();

			return;
		}

	}
}
