using System;
using System.IO;
using System.Collections.Generic;
using static PrismOS.Hexi.Misc.Function;
using PrismOS.Hexi.Misc;

namespace PrismOS.Hexi
{
	public static class Main
	{
		public static class Compiler
		{
			public static void Compile(string Input, string Output)
			{
				var bytes = new List<byte>();

				string[] Lines = File.ReadAllLines(Input);
				for (var ln = 0; ln < Lines.Length; ln++)
				{
					if (Lines[ln].Contains('(') && Lines[ln].EndsWith(");"))
					{
						var stage1 = Lines[ln].Replace(");", string.Empty).Split('(');
						var function = stage1[0].Split('.');
						var args = ParseArguments(stage1[1]);

						var found = false;

						for (var i = 0; i < Functions.Length; i++)
						{
							var func = Functions[i];

							if (func.Name == function[0])
							{
								// Add function type
								bytes.Add(func.Type);

								for (var j = 0; j < func.Arguments; j++)
								{
									var arg = args[j];

									// Add arguments
									if (int.TryParse(arg, out var param))
									{
										bytes.Add((byte)param);
									}
									else if (arg.StartsWith("b[") && arg.EndsWith("]"))
									{
										var count = AddBytes(ref bytes, arg) - 1;
										Functions[i] = new Function(func.Name, func.Type, func.Arguments + count, func.Definition);
									}
									else
									{
										Console.WriteLine("[ERROR] Unknown type for argument '" + arg + "'.");
										return;
									}
								}

								found = true;
								break;
							}
						}

						if (!found)
						{
							Console.WriteLine("[ERROR] Unknown function '" + function[0] + "'.");
							return;
						}
					}
				}

				// Done compiling
				File.WriteAllBytes(Output, bytes.ToArray());
			}

			private static int AddBytes(ref List<byte> bytes, string array)
			{
				var count = 0;

				foreach (var str in array.Replace("b[", string.Empty)
										 .Replace("]", string.Empty)
										 .Replace(" ", string.Empty)
										 .Split(','))
				{
					bytes.Add((byte)int.Parse(str));
					count++;
				}

				return count;
			}

			private static List<string> ParseArguments(string args)
			{
				var result = new List<string>();
				var arg = string.Empty;

				for (var i = 0; i < args.Length; i++)
				{
					var c = args[i];

					if (c == 'b' && args[i + 1] == '[')
					{
						var end = args.IndexOf("]");
						var array = args[i..end];

						result.Add(array + "]");

						i = end;
					}
					else if (c == ',')
					{
						result.Add(arg);

						arg = string.Empty;

						i += (args[i + 1] == ' ' ? 1 : 0);
					}
					else if (i == args.Length - 1)
					{
						if (arg.Length == 0)
							arg += c;

						result.Add(arg);

						break;
					}
					else arg += c;
				}

				if (result.Count == 0)
					result.Add(arg);

				return result;
			}
		}

		public static class Runtime
		{
			public static List<Executable> Executables { get; set; } = new();

			public static void RunProgram(string PathToFile)
            {
				Executables.Add(new(File.ReadAllBytes(PathToFile)));
            }

			public static void Tick()
			{
				foreach (Executable exe in Executables)
					exe.Tick();
			}
		}
	}
}