using Cosmos.System;
using Cosmos.Core;

namespace PrismGL2D.Extentions
{
	public unsafe class VBEConsole : Graphics
	{
		public VBEConsole() : base(VBE.getModeInfo().width, VBE.getModeInfo().height)
		{
			Buffer = string.Empty;
		}

		#region Methods

		public void WriteLine(string Text)
		{
			Write(Text);
			WriteLine();
		}
		public void WriteLine()
		{
			Write('\n');
		}

		public void Write(string Text)
		{
			Buffer += Text;
		}
		public void Write(char Char)
		{
			Buffer += Char;
			Update();
		}

		public string ReadLine(bool ShowInput = true)
		{
			string Result = string.Empty;

			while (true)
			{
				if (PrismTools.KeyboardEx.TryReadKey(out ConsoleKeyInfo Key))
				{
					switch (Key.Key)
					{
						case ConsoleKey.Backspace:
							Result = Result[..^2];
							Buffer = Buffer[..^2];
							break;
						case ConsoleKey.Enter:
							WriteLine();
							return Result;
						default:
							if (ShowInput)
							{
								Write(Key.KeyChar);
							}

							Result += Key.KeyChar;
							break;
					}
				}
			}
		}

		public new void Clear()
		{
			Buffer = string.Empty;
		}

		public void Update()
		{
			base.Clear();
			DrawString(0, 0, Buffer, Font.Fallback, Color.White);
			CopyTo((uint*)VBE.getLfbOffset());
		}

		#endregion

		#region Fields

		public string Buffer;

		#endregion
	}
}