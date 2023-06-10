using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace LangtonAntServer
{
	class LangtonAntServer : MarshalByRefObject
	{
		static void Main(string[] args)
		{
			TcpChannel chan = new TcpChannel(8085);
			ChannelServices.RegisterChannel(chan, false);
			RemotingConfiguration.RegisterWellKnownServiceType(typeof(LangtonAntServerClass.LangtonAntServerRemoteClass), "LangtonAntServer", WellKnownObjectMode.SingleCall);
			System.Console.WriteLine("Hit <enter> to exit...");
			System.Console.ReadLine();
		}
	}
}
