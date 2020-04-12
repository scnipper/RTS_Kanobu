using System.Net.Sockets;
using QuickType;

namespace Util
{
	public class P
	{
		private static P instance;

		public Config config;
		public Socket client;

		public static P Get => instance ?? (instance = new P());
	}
}