using PrismRuntime.SShell.Structure;
using Console = System.Console;
using Cosmos.System;
using Cosmos.Core;

namespace PrismRuntime.SShell.Scripts
{
	public static class Unix
	{
		public class PowerOff : Script
		{
			public PowerOff() : base("poweroff", "Powers down the system.") { }

			public override ReturnCode Invoke(string[] Args)
			{
				Power.Shutdown();
				return ReturnCode.Success;
			}
		}
		public class Reboot : Script
		{
			public Reboot() : base("reboot", "Reboots the system.") { }

			public override ReturnCode Invoke(string[] Args)
			{
				Power.Reboot();
				return ReturnCode.Success;
			}
		}
		public class Clear : Script
		{
			public Clear() : base("clear", "Clear the terminal screen.") { }

			public override ReturnCode Invoke(string[] Args)
			{
				Console.Clear();
				return ReturnCode.Success;
			}
		}
		public class Halt : Script
		{
			public Halt() : base("halt", "Halt the CPU.") { }

			public override ReturnCode Invoke(string[] Args)
			{
				CPU.Halt();
				return ReturnCode.Success;
			}
		}
		public class Cat : Script
		{
			public Cat() : base("cat", "Echo the contents of a file to the terminal.") { }

			public override ReturnCode Invoke(string[] Args)
			{
				Console.WriteLine(File.ReadAllText(Args[0]));
				return ReturnCode.Success;
			}
		}
		public class CP : Script
		{
			public CP() : base("cp", "Copies a File somewhere.") { }

			public override ReturnCode Invoke(string[] Args)
			{
				if (File.Exists(Args[0]) && !File.Exists(Args[1]))
				{
					File.Copy(Args[0], Args[1]);
					return ReturnCode.Success;
				}

				return ReturnCode.Unknown;
			}
		}
		public class LS : Script
		{
			public LS() : base("ls", "List the contents of a directory.") { }

			public override ReturnCode Invoke(string[] Args)
			{
				string FullPath = Args.Length == 0 ? "0:"/*Environment.CurrentDirectory*/ : Args[0];
				List<string> Entries = new();

				foreach (string D in Directory.GetDirectories(FullPath))
				{
					Entries.Add(D);
				}
				foreach (string F in Directory.GetFiles(FullPath))
				{
					Entries.Add(F);
				}

				// Entries.Sort();

				foreach (string S in Entries)
				{
					if (Directory.Exists(FullPath + S))
					{
						Console.ForegroundColor = ConsoleColor.Blue;
						if (S.Contains(' '))
						{
							Console.Write($" '{S}' ");
						}
						else
						{
							Console.Write($" {S} ");
						}
						continue;
					}
					if (File.Exists(FullPath + S))
					{
						Console.ForegroundColor = ConsoleColor.Cyan;
						if (S.Contains(' '))
						{
							Console.Write($" '{S}' ");
						}
						else
						{
							Console.Write($" {S} ");
						}
						continue;
					}
				}

				Console.ResetColor();
				Console.WriteLine();

				return ReturnCode.Success;
			}
		}
		public class RM : Script
		{
			public RM() : base("rm", "Remove a file/directory.") { }

			public override ReturnCode Invoke(string[] Args)
			{
				if (Args.Length > 1 && Args[0] == "-r")
				{
					Directory.Delete(Args[1]);
				}
				else
				{
					File.Delete(Args[0]);
				}
				return ReturnCode.Success;
			}
		}
	}
}