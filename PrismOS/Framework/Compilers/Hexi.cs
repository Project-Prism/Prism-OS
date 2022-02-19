using System;
using System.IO;
using System.Collections.Generic;
using static PrismOS.Framework.Process;

namespace PrismOS.Framework.Compilers
{
    public static class Hexi
    {
		public static byte[] Compile(string Input)
		{
			Dictionary<string, byte> Functions = new();
			Functions.Add("Console.Write", (byte)OPCodes.Write);
			Functions.Add("Console.WriteLine", (byte)OPCodes.Write);
			Functions.Add("Console.Clear", (byte)OPCodes.Clear);
			Functions.Add("Program.Jump", (byte)OPCodes.ROMJump);

			var Bytes = new List<byte>();
			int Index = 0;
			foreach (string Line in Input.Replace("	", "").Replace("\n", "").Split(";"))
			{
				string MLine = Line;
				if (MLine.Length == 0)
				{
					continue;
				}
				if (MLine.StartsWith("//"))
				{
					continue;
				}
				if (MLine.Contains("//"))
				{
					MLine = MLine.Remove(MLine.IndexOf("//"), MLine.Length - MLine.IndexOf("//"));
				}
				if (MLine.Contains('(') && MLine.EndsWith(")"))
				{
					var stage1 = MLine.Replace(")", string.Empty).Split('(');
					var function = stage1[0];
					var args = ParseArguments(stage1[1]);

					if (Functions.ContainsKey(function))
					{
						Bytes.Add(Functions[function]);

						for (var j = 0; j < args.Count; j++)
						{
							var arg = args[j];

							// Add arguments
							if (int.TryParse(arg, out var param))
							{
								// Detected int value
								Bytes.Add((byte)param);
							}
							else if (arg.StartsWith("\"") && arg.EndsWith("\""))
							{
								// String
								Bytes.Add((byte)(arg.Length - 2));
								for (int I = 1; I < arg.Length - 2; I++)
								{
									Bytes.Add((byte)arg[I]);
								}
							}
							else if (arg.StartsWith("b[") && arg.EndsWith("]"))
							{
								string nstr = arg.Replace("b[", string.Empty)
									 .Replace("]", string.Empty)
									 .Replace(" ", string.Empty);
								Bytes.Add((byte)nstr.Length);

								foreach (var str in nstr.Split(','))
								{
									Bytes.Add((byte)int.Parse(str));
								}
							}
							else
							{
								Console.WriteLine("[ERROR] Unknown type for argument '" + arg + "'.");
								return null;
							}
						}
					}
					else
                    {
						Console.WriteLine("[ERROR] Unknown function '" + function[^1] + "'.");
						return null;
					}
				}
				Index++;
			}

			// Done compiling
			return Bytes.ToArray();
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
				else
				{
					arg += c;
				}
			}

			if (result.Count == 0)
				result.Add(arg);

			return result;
		}
	}
}