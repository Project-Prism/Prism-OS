using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.Network.IPv4.TCP;
using Cosmos.System.Network.IPv4;
using PrismTools;

namespace PrismNetwork
{
	public static class NetworkManager
	{
		#region Methods

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

		/// <summary>
		/// Initializes the system networking interface.
		/// </summary>
		public static void Init()
		{
			try
			{
				_ = new DHCPClient().SendDiscoverPacket();
				Debugger.Log("Initialized networking!", Debugger.Severity.Ok);
			}
			catch
			{
				Debugger.Log("Unable to initialize networking!", Debugger.Severity.Warning);
			}
		}

		#endregion

		#region Fields

		public static Debugger Debugger { get; set; } = new("Network");

		#endregion
	}
}