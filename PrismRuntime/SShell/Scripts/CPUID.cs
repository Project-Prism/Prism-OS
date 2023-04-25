namespace PrismRuntime.SShell.Scripts;

public class CPUID : Script
{
	public CPUID() : base(nameof(CPUID).ToLower(), "A basic tool to read info from the CPU.")
	{
		AdvancedDescription = "cpuid [ID]";
	}

	public override void Invoke(string[] Args)
	{
		int EAX = 0, EBX = 0, ECX = 0, EDX = 0;
		Cosmos.Core.CPU.ReadCPUID(uint.Parse(Args[0]), ref EAX, ref EBX, ref ECX, ref EDX);
		Console.WriteLine($"EAX: {EAX}");
		Console.WriteLine($"EBX: {EBX}");
		Console.WriteLine($"ECX: {ECX}");
		Console.WriteLine($"EDX: {EDX}");
	}
}