using PrismBinary.Formats.ELF.Structure.ELFHeader;
using Console = System.Console;
using Cosmos.System;
using Cosmos.Core;

namespace PrismRuntime.SShell.Scripts
{
	public static unsafe class Unix
	{
		public class PowerOff : Script
		{
			public PowerOff() : base("poweroff", "Powers down the system.") { }

			public override void Invoke(string[] Args)
			{
				Power.Shutdown();
			}
		}
		public class HexDump : Script
		{
			public HexDump() : base("hexdump", "Print all bytes of a file as 'readable' hex.")
			{
				AdvancedDescription = "hexdump [NumberOfBytes] [path/to/file]";
			}

			public override void Invoke(string[] Args)
			{
				// Check if improper arguments were specifed.
				if (Args.Length < 2 || Args[0].Length == 0 || Args[1].Length == 0)
				{
					Console.WriteLine("Please specify proper options. Run 'man hexdump' for more info.");
					return;
				}

				// Check if the file exists
				if (int.TryParse(Args[0], out int N) && N > 0 && File.Exists(Args[1]))
				{
					// Load the file into memory.
					byte[] Hex = File.ReadAllBytes(Args[1]);

					// Print the number of hex bytes specified from the file.
					Console.WriteLine("0x" + BitConverter.ToString(Hex, 0, N).Replace("-", " 0x"));
				}
			}
		}
		public class ReadELF : Script
		{
			public ReadELF() : base("readelf", "Read about ELF files on the filesystem.")
			{
				AdvancedDescription =
					"readelf [options] [path/to/elf]\n" +
					"\tOnly '-h' is implemented at the moment.";
			}

			public override void Invoke(string[] Args)
			{
				// Check if improper arguments were specifed.
				if (Args.Length < 2 || Args[0].Length == 0 || Args[1].Length == 0)
				{
					Console.WriteLine("Please specify proper options. Run 'man readelf' for more info.");
					return;
				}

				// Check if the file exists
				if (Args[0] == "-h" && File.Exists(Args[1]))
				{
					fixed (byte* P = File.ReadAllBytes(Args[1]))
					{
						// Read the ELF header.
						ELFHeader32 Header = new((ELFHeader32*)P);

						// Print out it's details.
						Console.WriteLine($"Is Valid: {Header.IsValid()}");
						Console.WriteLine($"Type: {Header.Type}");
						Console.WriteLine($"Machine Type: {Header.MachineType}");
						Console.WriteLine($"Machine Version: {Header.MachineVersion}");
						Console.WriteLine($"Entry Point: 0x{Header.EntryPoint}");
					}
				}
				else
				{
					Console.WriteLine($"Impropper arguments were specified.");
					return;
				}
			}
		}
		public class Reboot : Script
		{
			public Reboot() : base("reboot", "Reboots the system.") { }

			public override void Invoke(string[] Args)
			{
				Power.Reboot();
			}
		}
		public class Clear : Script
		{
			public Clear() : base("clear", "Clear the terminal screen.") { }

			public override void Invoke(string[] Args)
			{
				Console.Clear();
			}
		}
		public class MKDir : Script
		{
			public MKDir() : base("mkdir", "Make a directory in the current path.")
			{
				AdvancedDescription = "mkdir [full paths]";
			}

			public override void Invoke(string[] Args)
			{
				if (Args.Length < 1)
				{
					Console.WriteLine("Insufficient arguments!");
					return;
				}

				for (int I = 0; I < Args.Length; I++)
				{
					if (Args[I][1] == ':' && Args[I][2] == '\\')
					{
						Directory.CreateDirectory(Args[I]);
					}
					else
					{
						Directory.CreateDirectory(Environment.CurrentDirectory + Args[I]);
					}
				}
			}
		}
		public class Touch : Script
		{
			public Touch() : base("touch", "Make a file with the input name in the current path.")
			{
				AdvancedDescription = "touch [full paths]";
			}

			public override void Invoke(string[] Args)
			{
				if (Args.Length < 1)
				{
					Console.WriteLine("Insuficcient arguments.");
					return;
				}

				for (int I = 0; I < Args.Length; I++)
				{
					if (Args[I][1] == ':' && Args[I][2] == '\\')
					{
						File.Create(Args[I]);
					}
					else
					{
						File.Create(Environment.CurrentDirectory + Args[I]);
					}
				}
			}
		}
		public class Halt : Script
		{
			public Halt() : base("halt", "Halt the CPU.") { }

			public override void Invoke(string[] Args)
			{
				CPU.Halt();
			}
		}
		public class Cat : Script
		{
			public Cat() : base("cat", "Echo the contents of a file to the terminal.")
			{
				AdvancedDescription = "cat [full path]";
			}

			public override void Invoke(string[] Args)
			{
				if (Args.Length != 1)
				{
					Console.WriteLine("Incorect amonut of arguments.");
				}

				Console.WriteLine(File.ReadAllText(Args[0]));
			}
		}
		public class Man : Script
		{
			public Man() : base("man", "Lists all commands or advanced info about a specific command.")
			{
				AdvancedDescription = "man [command name, optional.]";
			}

			public override void Invoke(string[] Args)
			{
				if (Args.Length > 0)
				{
					foreach (Script S in Shell.Scripts)
					{
						if (S.ScriptName == Args[0])
						{
							if (S.AdvancedDescription.Length == 0)
							{
								Console.WriteLine("Command does not have advanced description.");
								return;
							}

							Console.WriteLine($"{S.ScriptName} : {S.AdvancedDescription}");
							return;
						}
					}
					Console.WriteLine($"man: Could not find command manual for '{Args[0]}'.");
					return;
				}
				else
				{
					foreach (Script S in Shell.Scripts)
					{
						Console.WriteLine($"{S.ScriptName} : {S.BasicDescription}");
					}
				}
			}
		}
		public class PWD : Script
		{
			public PWD() : base("pwd", "Print working directory.") { }

			public override void Invoke(string[] Args)
			{
				Console.WriteLine(Environment.CurrentDirectory);
			}
		}
		public class CP : Script
		{
			public CP() : base("cp", "Copies a File somewhere.")
			{
				AdvancedDescription = "cp [Source] [Destination]";
			}

			public override void Invoke(string[] Args)
			{
				if (Args.Length < 2)
				{
					Console.WriteLine("Insuficcient arguments.");
					return;
				}

				string Destination = Args[1][1] == ':' && Args[1][2] == '\\' ? Args[1] : Environment.CurrentDirectory + Args[1];
				string Source = Args[0][1] == ':' && Args[0][2] == '\\' ? Args[0] : Environment.CurrentDirectory + Args[0];

				if (File.Exists(Source) && !File.Exists(Destination))
				{
					File.Copy(Source, Destination);
				}
			}
		}
		public class CD : Script
		{
			public CD() : base("cd", "Changes directories.")
			{
				AdvancedDescription = "cd [Destination]";
			}

			public override void Invoke(string[] Args)
			{
				if (Args.Length != 1)
				{
					Console.WriteLine("Insuficcient arguments.");
					return;
				}

				if (Args[0][1] == ':' && Args[0][2] == Path.DirectorySeparatorChar)
				{
					Environment.CurrentDirectory = Args[0];
					return;
				}

				foreach (string Section in Args[0].Split(Path.DirectorySeparatorChar))
				{
					Environment.CurrentDirectory = Section switch
					{
						".." => Environment.CurrentDirectory[..Environment.CurrentDirectory[..^1].LastIndexOf(Path.DirectorySeparatorChar)] + Path.DirectorySeparatorChar,
						"." => string.Empty,
						_ => Environment.CurrentDirectory + Section + (Section.EndsWith(Path.DirectorySeparatorChar) ? string.Empty : Path.DirectorySeparatorChar),
					};
				}
			}
		}
		public class LS : Script
		{
			public LS() : base("ls", "List the contents of a directory.")
			{
				AdvancedDescription = "ls [full path, optional]";
			}

			public override void Invoke(string[] Args)
			{
				string FullPath = Args.Length == 0 ? Environment.CurrentDirectory : Args[0];

				Console.ForegroundColor = ConsoleColor.Blue;
				foreach (string D in Directory.GetDirectories(FullPath))
				{
					if (D.Contains(' '))
					{
						Console.Write($" '{D}' ");
					}
					else
					{
						Console.Write($" {D} ");
					}
				}
				Console.ForegroundColor = ConsoleColor.Cyan;
				foreach (string F in Directory.GetFiles(FullPath))
				{
					if (F.Contains(' '))
					{
						Console.Write($" '{F}' ");
					}
					else
					{
						Console.Write($" {F} ");
					}
				}

				Console.ResetColor();
				Console.WriteLine();
			}
		}
		public class RM : Script
		{
			public RM() : base("rm", "Remove a file/directory.")
			{
				AdvancedDescription =
					"rm [flags] [full path]\n" +
					"=========================\n" +
					"	-r | Remove Directory";
			}

			public override void Invoke(string[] Args)
			{
				if (Args.Length < 1)
				{
					Console.WriteLine("Insufficient arguments.");
					return;
				}

				string Target = Args[0][1] == ':' && Args[0][2] == '\\' ? Args[0] : Environment.CurrentDirectory + Args[0];

				if (Args.Length > 1 && Args[0] == "-r")
				{
					Directory.Delete(Target);
				}
				else
				{
					File.Delete(Target);
				}
			}
		}
	}
}