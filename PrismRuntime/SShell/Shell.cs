using PrismRuntime.SShell.Structure;
using PrismRuntime.SShell.Scripts;
using PrismRuntime.SSharp;
using PrismTools;

namespace PrismRuntime.SShell
{
	public static class Shell
	{
		#region Methods

		/// <summary>
		/// Invokes a command if it exists.
		/// </summary>
		/// <param name="VS">Arguments to the command.</param>
		public static void Invoke(string[] VS)
		{
			// Skip if there are no commands to run.
			if (Scripts.Count == 0)
			{
				Debugger.Warn("Command interperiter not initialized!");
				return;
			}

			for (int I = 0; I < Scripts.Count; I++)
			{
				// Check if command exists.
				if (Scripts[I].ScriptName == VS[0])
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

			if (File.Exists($"0:\\{VS[0]}")|| File.Exists(VS[0]))
			{
				Executable EXE = new(File.ReadAllBytes(VS[0]));

				while (EXE.IsEnabled)
				{
					EXE.Next();
				}

				return;
			}

			Debugger.Warn("Command not found!");
		}

		/// <summary>
		/// Main method for the shell.
		/// </summary>
		public static void Main()
		{
			Debugger.Warn("Droping to recovery shell...");

			// Initialize all commands.
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