using PrismRuntime.SShell.Structure;

namespace PrismRuntime.SShell.Scripts
{
	public class VEdit : Script
	{
		public VEdit() : base("vedit", "A basic text editor.") { }

		public override ReturnCode Invoke(string[] Args)
		{
			string Buffer = string.Empty;
			Console.Clear();
			
			if (File.Exists(Args[0]))
			{
				Buffer = File.ReadAllText(Args[0]);
				Console.Write(Buffer);
			}

			while (true)
			{
				ConsoleKeyInfo Key = Console.ReadKey();

				if (Key.Key == ConsoleKey.Backspace)
				{
					Buffer = Buffer[..^1];
					Console.Clear();
					Console.Write(Buffer);
					continue;
				}
				if (Key.Key == ConsoleKey.Escape)
				{
					File.WriteAllBytes(Args[0], System.Text.Encoding.UTF8.GetBytes(Buffer));
					Console.Clear();
					return ReturnCode.Success;
				}
				if (Key.Key == ConsoleKey.Enter)
				{
					Console.Write('\n');
					Buffer += '\n';
					continue;
				}

				Buffer += Key.KeyChar;
			}
		}
	}
}