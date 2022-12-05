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

			public override void Invoke(string[] Args)
			{
				Power.Shutdown();
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
			public MKDir() : base("mkdir", "Make a directory in the current path.") { }

			public override void Invoke(string[] Args)
			{
				Directory.CreateDirectory(Args[0]);
			}
		}
		public class Touch : Script
		{
			public Touch() : base("touch", "Make a file with the input name in the current path.") { }

			public override void Invoke(string[] Args)
			{
				File.Create(Args[0]);
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
			public Cat() : base("cat", "Echo the contents of a file to the terminal.") { }

			public override void Invoke(string[] Args)
			{
				Console.WriteLine(File.ReadAllText(Args[0]));
			}
		}
		public class Man : Script
		{
			public Man() : base("man", "man { command name }") { }

			public override void Invoke(string[] Args)
			{
				if (Args.Length > 0)
				{
					foreach (Script S in Shell.Scripts)
					{
						if (S.Name == Args[0])
						{
							Console.WriteLine($"{S.Name} : {S.Description}");
							return;
						}
					}
					Console.WriteLine($"man: Could not find command manual for '{Args[0]}'.");
				}
				else
				{
					foreach (Script S in Shell.Scripts)
					{
						Console.WriteLine($"{S.Name} : {S.Description}");
					}
				}
			}
		}
		public class PWD : Script
		{
			public PWD() : base("pwd", "Print working directory.") { }

			public override void Invoke(string[] Args)
			{
				//Console.WriteLine(Environment.CurrentDirectory);
				Console.WriteLine("0:\\");
			}
		}
		public class CP : Script
		{
			public CP() : base("cp", "Copies a File somewhere.") { }

			public override void Invoke(string[] Args)
			{
				if (File.Exists(Args[0]) && !File.Exists(Args[1]))
				{
					File.Copy(Args[0], Args[1]);
				}
			}
		}
		public class LS : Script
		{
			public LS() : base("ls", "List the contents of a directory.") { }

			public override void Invoke(string[] Args)
			{
				string FullPath = Args.Length == 0 ? "0:"/*Environment.CurrentDirectory*/ : Args[0];

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
			public RM() : base("rm", "Remove a file/directory.") { }

			public override void Invoke(string[] Args)
			{
				if (Args.Length > 1 && Args[0] == "-r")
				{
					Directory.Delete(Args[1]);
				}
				else
				{
					File.Delete(Args[0]);
				}
			}
		}
	}
}