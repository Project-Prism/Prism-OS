using PrismRuntime.SShell.Structure;

namespace PrismRuntime.SShell.Scripts
{
	public class Status : Script
	{
		public Status() : base("status", "Print statistics about certain system properties.") { }

		public override void Invoke(string[] Args)
		{
			switch (Args[0])
			{
				case "ram":
					Console.WriteLine($"{Cosmos.Core.GCImplementation.GetUsedRAM() / 1024 / 1024}/{Cosmos.Core.CPU.GetAmountOfRAM()} MB used.");
					break;
				case "net":
					foreach (var N in Cosmos.System.Network.Config.NetworkConfiguration.NetworkConfigs)
					{
						Console.WriteLine($"{N.Device.Name} : {(N.Device.Ready ? "Ready" : "Not ready")}");
					}
					break;
			}
		}
	}
}