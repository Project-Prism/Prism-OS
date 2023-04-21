using Cosmos.System.Network.IPv4.TCP;
using Cosmos.System.Network.IPv4;
using System.Text;

namespace PrismNetwork;

public class WebClient
{
	public WebClient(string URL)
	{
		this.URL = new(URL);
	}
	public WebClient(URL URL)
	{
		this.URL = URL;
	}
	public WebClient()
	{
		URL = new("");
	}

	#region Methods

	public byte[] DownloadFile(int Port = 80)
	{
		EndPoint EP = new(URL.GetAddress(), (ushort)Port);
		string Request =
			$"GET {URL.GetPath()} HTTP/1.1\n" +
			"Connection: Keep - Alive";

		TcpClient Client = new(URL.GetAddress(), Port);
		Client.Connect(URL.GetAddress(), Port);
		Client.Send(Encoding.UTF8.GetBytes(Request));
		return Client.Receive(ref EP);
	}

	#endregion

	#region Fields

	public URL URL;

	#endregion

}