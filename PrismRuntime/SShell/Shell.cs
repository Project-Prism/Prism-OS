using PrismRuntime.SShell.Structure;
using PrismRuntime.SShell.Scripts;
using PrismTools;

namespace PrismRuntime.SShell
{
	public static class Shell
	{
		#region Methods

		public static ReturnCode Invoke(string[] VS)
		{
			if (Scripts.Count == 0)
			{
				return ReturnCode.CommandNotFound;
			}

			for (int I = 0; I < Scripts.Count; I++)
			{
				if (Scripts[I].Name == VS[0])
				{
					try
					{
						string[] T = new string[VS.Length - 1];
						for (int I2 = 0; I2 < T.Length; I2++)
						{
							T[I2] = VS[I2 + 1];
						}

						return Scripts[I].Invoke(T);
					}
					catch (Exception E)
					{
						Debugger.Error(E.Message);
						return ReturnCode.Unknown;
					}
				}
			}
			return ReturnCode.CommandNotFound;
		}

		public static void Main()
		{
			Debugger.Warn("Droping to recovery shell...");

			_ = new Unix.PowerOff();
			_ = new Unix.Reboot();
			_ = new Unix.Clear();
			_ = new Unix.Halt();
			_ = new Unix.Cat();
			_ = new Unix.CP();
			_ = new Unix.LS();
			_ = new Unix.RM();
			_ = new VEdit();
			_ = new Help();
			_ = new CLI();

			while (true)
			{
				Console.Write("> ");

				string? Input = Console.ReadLine();
				if (Input == null)
				{
					Debugger.Warn("Null");
					continue;
				}

				switch (Invoke(Input.Split(' ')))
				{
					case ReturnCode.CommandNotFound:
						Debugger.Warn("Command not found!");
						break;
					case ReturnCode.NotEnoughArgs:
						Debugger.Warn("Not enough arguments were specified!");
						break;
					case ReturnCode.TooManyArgs:
						Debugger.Warn("Too many arguments were specified!");
						break;
					case ReturnCode.Unknown:
						Debugger.Warn("The script closed unexpectedly!");
						break;
				}
			}
		}

		#endregion

		#region Fields

		internal static Debugger Debugger { get; set; } = new("SShell");
		internal static List<Script> Scripts { get; set; } = new();

		#endregion
	}
}