using Console = System.Console;
using Cosmos.System.FileSystem;
using PrismAPI.Audio;
using PrismAPI.Filesystem.Formats.ELF.ELFHeader;
using PrismAPI.Filesystem;
using Cosmos.System;
using Cosmos.Core;

namespace PrismAPI.Runtime.SShell.Scripts;

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
		public HexDump() : base("hexdump", "Prints all the bytes of a file as 'readable' hexadecimal.")
		{
			AdvancedDescription = "hexdump [NumberOfBytes] [path/to/file]";
		}

		public override void Invoke(string[] Args)
		{
			// Check if improper arguments were specifed.
			if (Args.Length < 2 || Args[0].Length == 0 || Args[1].Length == 0)
			{
				Console.WriteLine("Please specify the proper options for 'hexdump'. Run 'man hexdump' for more info.");
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
				Console.WriteLine("Please specify the proper options for 'readelf'. Run 'man readelf' for more info.");
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
					Console.WriteLine($"Is Valid: {Header.IsValid}");
					Console.WriteLine($"Type: {Header.Type}");
					Console.WriteLine($"Machine Type: {Header.MachineType}");
					Console.WriteLine($"Machine Version: {Header.MachineVersion}");
					Console.WriteLine($"Entry Point: 0x{Header.EntryPoint}");
				}
			}
			else
			{
				Console.WriteLine("Incorrect arguments were specified.");
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
	public class LSBLK : Script
	{
		public LSBLK() : base(nameof(LSBLK).ToLower(), "Lists all of the connected block devices.") { }

		public override void Invoke(string[] Args)
		{
			foreach (Disk Current in FilesystemManager.VFS.GetDisks())
			{
				foreach (ManagedPartition Partition in Current.Partitions)
				{
					Console.WriteLine($"{Partition.RootPath} > {Current.Size / 1073741824} GB");
				}
			}
		}
	}
	public class Clear : Script
	{
		public Clear() : base("clear", "Clears the terminal.") { }

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
				Directory.CreateDirectory(Args[I]);
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
				Console.WriteLine("Insufficient arguments.");
				return;
			}

			for (int I = 0; I < Args.Length; I++)
			{
				File.Create(Args[I]);
			}
		}
	}
	public class MKFS : Script
	{
		public MKFS() : base(nameof(MKFS).ToLower(), "Initialize a disk with a file system.")
		{
			AdvancedDescription = "mkfs [type] [disk ID]";
		}

		public override void Invoke(string[] Args)
		{
			if (Args.Length < 2)
			{
				Console.WriteLine("Insufficient arguments.");
				return;
			}

			if (int.TryParse(Args[1], out int ID))
			{
				FilesystemManager.Format(ID, Args[0].ToUpper());
			}
			else
			{
				Console.WriteLine("Invalid disk ID!");
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
		public Cat() : base("cat", "Reads the contents of a file to the terminal.")
		{
			AdvancedDescription = "cat [full path]";
		}

		public override void Invoke(string[] Args)
		{
			if (Args.Length != 1)
			{
				Console.WriteLine("Incorrect amount of arguments.");
			}

			Console.WriteLine(File.ReadAllText(Args[0]));
		}
	}
	public class Man : Script
	{
		public Man() : base("man", "Lists all the commands or prints advanced information about a specific command.")
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
							Console.WriteLine("Command does not have an advanced description.");
							return;
						}

						Console.WriteLine($"{S.ScriptName} : {S.AdvancedDescription}");
						return;
					}
				}
				Console.WriteLine($"man: Could not find a command manual for '{Args[0]}'.");
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
		public PWD() : base("pwd", "Prints the current working directory.") { }

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
				Console.WriteLine("Insufficient arguments.");
				return;
			}

			if (File.Exists(Args[0]) && !File.Exists(Args[1]))
			{
				File.Copy(Args[0], Args[1]);
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
				Console.WriteLine("Insufficient arguments.");
				return;
			}

			Environment.CurrentDirectory = Args[0];
		}
	}
	public class LS : Script
	{
		public LS() : base("ls", "Lists the contents of a directory.")
		{
			AdvancedDescription = "ls [full path, optional]";
		}

		public override void Invoke(string[] Args)
		{
			string fullPath = Args.Length == 0 || Args[0].Length == 0 ? Environment.CurrentDirectory : Args[0];

			Console.ForegroundColor = ConsoleColor.Blue;
			foreach (string D in Directory.GetDirectories(fullPath))
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
			foreach (string F in Directory.GetFiles(fullPath))
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
		public RM() : base("rm", "Removes a file or directory.")
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

			if (Args.Length > 1 && Args[0] == "-r")
			{
				Directory.Delete(Args[1]);
			}
			else
			{
				File.Delete(Args[1]);
			}
		}
	}
}
