using PrismTools.IO;
using Cosmos.HAL;

namespace PrismGraphics.Extentions
{
	public class VBEConsole
	{
		public VBEConsole()
		{
			Global.PIT.RegisterTimer(new(() =>
			{
				if (IsCursorVisible && IsCursorEnabled)
				{
					// Draw Cursor
					Canvas.DrawFilledRectangle(X, Y, 1, Font.Fallback.Size, 0, Color.FromConsleColor(ForegroundColor));
					Canvas.Update();
					IsCursorVisible = false;
					return;
				}
				else
				{
					// Erase Cursor
					Canvas.DrawFilledRectangle(X, Y, 1, Font.Fallback.Size, 0, Color.FromConsleColor(BackgroundColor));
					Canvas.Update();
					IsCursorVisible = true;
					return;
				}
			}, 500000000, true));
		}

		#region Methods

		public static void Clear()
		{
			X = SpacingX;
			Y = SpacingY;
			Canvas.Clear();
			Buffer = string.Empty;
		}

		public static void Write(string Text)
		{
			// Erase Cursor
			Canvas.DrawFilledRectangle(X, Y, 1, Font.Fallback.Size, 0, Color.FromConsleColor(BackgroundColor));

			for (int I = 0; I < Text.Length; I++)
			{
				WriteCore(Text[I]);
			}

			// Draw Cursor
			Canvas.DrawFilledRectangle(X, Y, 1, Font.Fallback.Size, 0, Color.FromConsleColor(BackgroundColor));
			Canvas.Update();
		}
		public static void Write(char Char)
		{
			// Erase Cursor
			Canvas.DrawFilledRectangle(X, Y, 1, Font.Fallback.Size, 0, Color.FromConsleColor(BackgroundColor));

			WriteCore(Char);

			// Draw Cursor
			Canvas.DrawFilledRectangle(X, Y, 1, Font.Fallback.Size, 0, Color.FromConsleColor(BackgroundColor));
			Canvas.Update();
		}

		private static void WriteCore(char C)
		{
			Buffer += C;

			switch (C)
			{
				case '\n':
					X = SpacingX;
					Y += (int)Font.Fallback.Size;
					break;
				case '\b':
					X -= (int)Font.Fallback.MeasureString(Buffer[^1].ToString());
					break;
				case '\t':
					X += (int)(Font.Fallback.Size * 4);
					break;
				default:
					Canvas.DrawFilledRectangle(X, Y, Font.Fallback.MeasureString(C.ToString()), Font.Fallback.Size, 0, Color.FromConsleColor(BackgroundColor));
					Canvas.DrawChar(X, Y, C, Font.Fallback, Color.FromConsleColor(ForegroundColor), false);

					if (X >= Canvas.Width - SpacingX)
					{
						X = SpacingX;
						Y += (int)Font.Fallback.Size;
					}
					else
					{
						X += (int)Font.Fallback.MeasureString(C.ToString());
					}
					break;
			}
		}

		public static string ReadLine(bool RedirectOutput = false)
		{
			string Input = string.Empty;

			while (true)
			{
				if (KeyboardEx.TryReadKey(out ConsoleKeyInfo Key))
				{
					IsCursorEnabled = false;

					switch (Key.Key)
					{
						case ConsoleKey.Enter:
							Write("\n" + Input + "\n");
							Input = string.Empty;
							IsCursorEnabled = true;
							return Input;
						case ConsoleKey.Backspace:
							if (X > 0 || Y > 0)
							{
								if (X < 1)
								{
									Y -= (int)Font.Fallback.Size;
									X = (int)Font.Fallback.MeasureString(Buffer.Split('\n')[Y]);
								}
								else
								{
									X -= (int)Font.Fallback.MeasureString(Buffer[^1].ToString());
								}

								Buffer = Buffer[..^1];

								Canvas.DrawFilledRectangle(X, Y, Font.Fallback.Size, Font.Fallback.Size, 0, Color.FromConsleColor(BackgroundColor));
								Canvas.Update();
							}
							IsCursorEnabled = true;
							break;
						default:
							Input += Key.KeyChar;
							if (!RedirectOutput)
							{
								Write(Key.KeyChar);
							}
							IsCursorEnabled = true;
							break;
					}
				}
			}
		}

		#endregion

		#region Fields

		private static bool IsCursorVisible = true;
		private static bool IsCursorEnabled = true;
		private static readonly int SpacingX = 5;
		private static readonly int SpacingY = 5;
		private static string Buffer = string.Empty;
		private static VBECanvas Canvas = new();
		private static int X = 5;
		private static int Y = 5;

		public static ConsoleColor ForegroundColor = ConsoleColor.White;
		public static ConsoleColor BackgroundColor = ConsoleColor.Black;

		#endregion
	}
}