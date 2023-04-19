using PrismFilesystem.Formats.ELF.Structure.ELFHeader;
using PrismFilesystem.Formats.ELF;
using PrismRuntime.SShell.Scripts;
using PrismRuntime.SSharp;
using PrismTools;

namespace PrismRuntime.SShell
{
    public static unsafe class Shell
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
				Debugger.WriteFull("Command interperiter not initialized!", Severity.Warn);
				return;
			}

			for (int I = 0; I < Scripts.Count; I++)
			{
				// Check if command exists.
				if (Scripts[I].ScriptName == VS[0])
				{
					string[] T = new string[VS.Length - 1];

					for (int I2 = 0; I2 < T.Length; I2++)
					{
						T[I2] = VS[I2 + 1];
					}

					Scripts[I].Invoke(T);
					return;
				}
			}

			if (File.Exists($"0:\\{VS[0]}") || File.Exists(VS[0]))
			{
				// Read the program's running data.
				byte[] ROM = File.ReadAllBytes(VS[0]);

				// Check if the file isn't an ELF. Run as a SSharp program if it isn't.
				if (ROM.Length < sizeof(ELFHeader32))
				{
					Executable EXE = new(File.ReadAllBytes(VS[0]));

					while (EXE.IsEnabled)
					{
						EXE.Next();
					}

					return;
				}
				// Run an elf file when it's detected.
				else
				{
					// Create a new header, then run it.
					ELFFile32 ELF = new(ROM);
					ELF.Main();
					return;
				}
			}

			Debugger.WriteFull("Command not found!", Severity.Warn);
		}

		/// <summary>
		/// Main method for the shell.
		/// </summary>
		public static void Main()
		{
			Debugger.WriteFull("Droping to recovery shell...", Severity.Warn);
			Console.WriteLine("Type \"man\" to get a list of commands.");

			// Initialize all commands.
			_ = new Unix.PowerOff();
			_ = new Unix.HexDump();
			_ = new Unix.ReadELF();
			_ = new Unix.Reboot();
			_ = new Unix.Clear();
			_ = new Unix.MKDir();
			_ = new Unix.Touch();
			_ = new Unix.Halt();
			_ = new Unix.Cat();
			_ = new Unix.Man();
			_ = new Unix.PWD();
			_ = new Unix.CP();
			_ = new Unix.CD();
			_ = new Unix.LS();
			_ = new Unix.RM();
			_ = new Status();
			_ = new Locker();
			_ = new VEdit();
			_ = new CLI();

			while (true)
			{
				Console.Write("> ");

				string? Input = Console.ReadLine();

				if (Input == null)
				{
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