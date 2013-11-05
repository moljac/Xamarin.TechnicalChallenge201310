using System;
using System.Drawing;
using MonoMac.Foundation;
using global::MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace HttpMessageBus.EXE.Client.MonoMac
{
	class MainClass
	{
		static void Main (string[] args)
		{
			NSApplication.Init ();
			NSApplication.Main (args);
		}
	}
}

