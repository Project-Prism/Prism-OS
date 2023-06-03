using PrismAPI.Tools;

namespace PrismAPI.Runtime.SShell.Scripts;

public class Locker : Script
{
	public Locker() : base(nameof(Locker).ToLower(), "Encrypt, hash, and decrypt data.")
	{
		AdvancedDescription = $"{ScriptName} [option] [file] [key]\n-e : Encrypt\n-d : Decrypt\n-h : Hash";
	}

	public override void Invoke(string[] Args)
	{
		switch (Args[0])
		{
			case "-e":
				if (Args.Length != 3)
				{
					Console.WriteLine("Not enough args.");
					return;
				}

				File.WriteAllBytes(Args[1], Crypt.Encrypt(Args[2], File.ReadAllBytes(Args[1])));
				break;
			case "-d":
				if (Args.Length != 3)
				{
					Console.WriteLine("Not enough args.");
					return;
				}

				File.WriteAllBytes(Args[1], Crypt.Decrypt(Args[2], File.ReadAllBytes(Args[1])));
				break;
			case "-h":
				if (Args.Length != 2)
				{
					Console.WriteLine("Not enough args.");
					return;
				}

				Console.WriteLine(Crypt.Hash(Args[1]));
				break;
			default:
				return;
		}
	}
}