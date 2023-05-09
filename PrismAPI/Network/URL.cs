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

	#region Methods

	/// <summary>
	/// Gets the protocol of the url.
	/// </summary>
	/// <returns>Protocol of the url (e.g. http, ftp)</returns>
	public string GetProtocol() => FullURL.Split(':')[0];
	/// <summary>
	/// Gets the main domain of the url.
	/// </summary>
	/// <returns>Domain of the url.</returns>
	public string GetDomain() => FullURL.Replace("//", "&&&&").Split("/")[0].Replace("&&&&", "//") + "/";
	/// <summary>
	/// Gets the path of the URL
	/// </summary>
	/// <returns>Path of the url after the domain.</returns>
	public string GetPath() => FullURL.Split('/')[^1];

	/// <summary>
	/// Gets the address of the url.
	/// </summary>
	/// <returns>Address to contact</returns>
	public Address GetAddress()
	{
		DnsClient Client = new();
		Client.Connect(NetworkConfiguration.CurrentNetworkConfig.IPConfig.DefaultGateway);
		Client.SendAsk(GetDomain());
		return Client.Receive();
	}

	#endregion

	#region Fields

	public string FullURL;

	#endregion
}