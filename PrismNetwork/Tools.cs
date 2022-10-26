using Cosmos.System.Network.IPv4.TCP;
using Cosmos.System.Network.IPv4;

namespace PrismNetwork
{
	public static class Tools
	{
		/// <summary>
		/// Pings an IP address.
		/// </summary>
		/// <param name="A"></param>
		/// <returns>Time in miliseconds it takes for the server to respond.</returns>
		public static ulong Ping(Address A)
		{
			DateTime T = DateTime.Now;
			new TcpClient(80).Connect(A, 80);
			return (ulong)(DateTime.Now - T).TotalMilliseconds;
		}
	}
}