using Cosmos.Core.Memory;

namespace PrismAPI.Runtime.SShell.Scripts;

public class Status : Script
{
	public Status() : base("status", "Print statistics about certain system properties.")
	{
		AdvancedDescription =
			"status [property]\n" +
			"=================\n" +
			"ram - RAM usage\n" +
			"net - Network status";
	}

	public override void Invoke(string[] Args)
	{
		if (Args.Length < 1)
		{
			Console.WriteLine("Insufficient arguments!");
			return;
		}

		switch (Args[0])
		{
			case "ram":
				if (Args.Length > 1 && Args[1] == "collect")
				{
					Heap.Collect();
				}

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