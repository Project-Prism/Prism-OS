using Cosmos.System.Network.IPv4.UDP.DNS;
using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4;

namespace PrismAPI.Network;

/// <summary>
/// URL parsing class.
/// </summary>
public class URL
{
	/// <summary>
	/// Creates a new instance of she <see cref="URL"/> class.
	/// </summary>
	/// <param name="FullURL"></param>
	public URL(string FullURL)
	{
		this.FullURL = FullURL;
	}

	#region Properties

	public bool HasProtocol => FullURL.Contains(Delimiter);

	public Address Address
	{
		get
		{
			DnsClient Client = new();
			Client.Connect(NetworkConfiguration.CurrentNetworkConfig.IPConfig.DefaultGateway);
			Client.SendAsk(Host);
			return Client.Receive();
		}
	}

	public string Protocol
	{
		get
		{
			if (!HasProtocol)
			{
				return string.Empty;
			}

			return FullURL[..FullURL.IndexOf(Delimiter)];
		}
		set
		{
			if (!HasProtocol)
			{
				return;
			}

			FullURL = FullURL.Replace(Protocol + Delimiter, value + Delimiter);
		}
	}

	public string Host
	{
		get
		{
			string Temp = FullURL;

			if (HasProtocol)
			{
				Temp = Temp.Replace(Protocol + Delimiter, string.Empty);
			}

			return Temp.Split('/')[0].Split(':')[0];
		}
		set
		{
			FullURL = FullURL.Replace(Delimiter + Host, Delimiter + value);
		}
	}

	public string Path
	{
		get
		{
			string Temp = FullURL;

			if (HasProtocol)
			{
				Temp = Temp.Replace(Protocol + Delimiter, string.Empty);
			}

			return Temp.Split(Host + /*':' + Port +*/ '/')[1];
		}
		set
		{
			FullURL = FullURL.Replace(Host + /*':' + Port +*/ '/' + Path, Host + /*':' + Port +*/ '/' + value);
		}
	}

	public string Port => throw new NotImplementedException();

	#endregion

	#region Constants

	public const string Delimiter = "://";

	#endregion

	#region Fields

	public string FullURL;

	#endregion
}