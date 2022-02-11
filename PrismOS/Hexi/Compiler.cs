using System;
using System.IO;
using System.Collections.Generic;
using PrismOS.Hexi.Core;

namespace PrismOS.Hexi
{
    public class Compiler
    {
		public Compiler()
        {
			// Add core functions
			Functions.Add("Console.Write", (byte)Binary.OPCodes.Write);
			Functions.Add("Console.WriteLine", (byte)Binary.OPCodes.Write);
			Functions.Add("Console.Clear", (byte)Binary.OPCodes.Clear);
			Functions.Add("Program.Jump", (byte)Binary.OPCodes.Jump);
		}

		public Dictionary<string, byte> Functions = new();

		public void Compile(string Input, string Output)
		{
			var Bytes = new List<byte>();
			int Index = 0;
			foreach (string Line in File.ReadAllText(Input).Replace("	", "").Replace("\n", "").Split(";"))
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
								AddBytes(ref Bytes, arg);
							}
							else
							{
								Console.WriteLine("[ERROR] Unknown type for argument '" + arg + "'.");
								return;
							}
						}
					}
					else
                    {
						Console.WriteLine("[ERROR] Unknown function '" + function[^1] + "'.");
						return;
					}
				}
				Index++;
			}

			// Done compiling
			File.WriteAllBytes(Output, Bytes.ToArray());
		}

		private static void AddBytes(ref List<byte> bytes, string array)
		{
			string nstr = array.Replace("b[", string.Empty)
									 .Replace("]", string.Empty)
									 .Replace(" ", string.Empty);
			bytes.Add((byte)nstr.Length);

			foreach (var str in nstr.Split(','))
			{
				bytes.Add((byte)int.Parse(str));
			}
		}

		private List<string> ParseArguments(string args)
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