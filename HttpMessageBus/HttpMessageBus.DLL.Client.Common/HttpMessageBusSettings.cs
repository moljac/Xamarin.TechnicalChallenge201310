using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpMessageBus 
{
	public partial class HttpMessageBusSettings 
	{
		public static int		Port = 9000;
		public static int		PortClientListener = 9999;
		public static string	Host = "192.168.1.70";

		public static string[] Verbs = new string[]
		{
		  "listen"		// subscribe
		, "notify"		// send message
		, "report"		// create xml report
		};

		public static bool DebugMode = false;
	}
}
