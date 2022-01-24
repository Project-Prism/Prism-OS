using System;
using System.Collections.Generic;
using static PrismOS.Hexi.Runtime;

namespace PrismOS.Hexi
{
	internal delegate void MethodDefinition(Executable exe, byte[] args);

	internal class Function
	{
		public string Name;
		public Funcs Type;
		public int Arguments;
		public MethodDefinition Definition;

		public Function(string name, Funcs type, int args, MethodDefinition def)
		{
			Name = name;
			Type = type;
			Arguments = args;
			Definition = def;
		}
	}

	internal class Compiler
	{
		internal static byte[] Compile(string[] lines)
		{
			var bytes = new List<byte>();

			for (var ln = 0; ln < lines.Length; ln++)
				if (lines[ln].Contains('(') && lines[ln].EndsWith(");"))
				{
					var stage1 = lines[ln].Replace(");", string.Empty).Split('(');
					var function = stage1[0].Split('.');
					var args = ParseArguments(stage1[1]);

					var found = false;

					for (var i = 0; i < Functions.Length; i++)
					{
						var func = Functions[i];

						if (func.Name == function[0])
						{
							// Add function type
							bytes.Add((byte)func.Type);

							for (var j = 0; j < func.Arguments; j++)
							{
								var arg = args[j];

								// Add arguments
								if (int.TryParse(arg, out var param))
									bytes.Add((byte)param);
								else if (arg.StartsWith("b[") && arg.EndsWith("]"))
								{
									var count = AddBytes(ref bytes, arg) - 1;
									Functions[i] = new Function(func.Name, func.Type, func.Arguments + count, func.Definition);
								}
								else
								{
									Console.WriteLine("[ERROR] Unknown type for argument '" + arg + "'.");
									return null;
								}
							}

							found = true;
							break;
						}
					}

					if (!found)
					{
						Console.WriteLine("[ERROR] Unknown function '" + function + "'.");
						return null;
					}
				}

			// Done compiling
			return bytes.ToArray();
		}

		internal static int AddBytes(ref List<byte> bytes, string array)
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

		internal static List<string> ParseArguments(string args)
		{
			var result = new List<string>();
			var arg = string.Empty;

			for (var i = 0; i < args.Length; i++)
			{
				var c = args[i];

				if (c == 'b' && args[i + 1] == '[')
				{
					var end = args.IndexOf("]");
					var array = args.Substring(i, end - i);

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
				} else arg += c;
			}

			if (result.Count == 0)
				result.Add(arg);

			return result;
		}
	}
}
