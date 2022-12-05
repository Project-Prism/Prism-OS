using PrismRuntime.SShell.Structure;
using PrismRuntime.SShell.Scripts;
using PrismTools;

namespace PrismRuntime.SShell
{
	public static class Shell
	{
		#region Methods

		public static void Invoke(string[] VS)
		{
			if (Scripts.Count == 0)
			{
				Debugger.Warn("Command interperiter not initialized!");
				return;
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

						Scripts[I].Invoke(T);
						return;
					}
					catch (Exception E)
					{
						Debugger.Error(E.Message);
						return;
					}
				}
			}

			Debugger.Warn("Command not found!");
		}

		public static void Main()
		{
			Debugger.Warn("Droping to recovery shell...");

			_ = new Unix.PowerOff();
			_ = new Unix.Reboot();
			_ = new Unix.Clear();
			_ = new Unix.MKDir();
			_ = new Unix.Touch();
			_ = new Unix.Halt();
			_ = new Unix.Cat();
			_ = new Unix.Man();
			_ = new Unix.PWD();
			_ = new Unix.CP();
			_ = new Unix.LS();
			_ = new Unix.RM();
			_ = new Status();
			_ = new VEdit();
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

				Invoke(Input.Split(' '));
			}
		}

		#endregion

		#region Fields

		internal static Debugger Debugger { get; set; } = new("SShell");
		internal static List<Script> Scripts { get; set; } = new();

		#endregion
	}
}