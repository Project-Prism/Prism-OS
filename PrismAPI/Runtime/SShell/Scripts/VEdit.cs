namespace PrismAPI.Runtime.SShell.Scripts;

public class VEdit : Script
{
	public VEdit() : base("vedit", "A basic text editor.")
	{
		AdvancedDescription = "vedit [file name]";
	}

	public override void Invoke(string[] Args)
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
				if (Key.Modifiers == ConsoleModifiers.Control)
				{
					while (Buffer[^1] != ' ')
					{
						if (Buffer.Length > 0)
						{
							Buffer = Buffer[..^1];
						}
					}

					Console.Clear();
					Console.Write(Buffer);
					continue;
				}

				if (Buffer.Length > 0)
				{
					Buffer = Buffer[..^1];
					Console.Clear();
					Console.Write(Buffer);
				}
				continue;
			}
			if (Key.Key == ConsoleKey.Escape)
			{
				if (File.Exists(Args[0]))
				{
					File.Delete(Args[0]);
				}
				File.WriteAllBytes(Args[0], System.Text.Encoding.UTF8.GetBytes(Buffer));
				Console.Clear();
				return;
			}
			if (Key.Key == ConsoleKey.Enter)
			{
				Console.Write('\n');
				Buffer += '\n';
				continue;
			}

			Buffer += Key.KeyChar;
			Console.Write(Key.KeyChar);
		}
	}
}