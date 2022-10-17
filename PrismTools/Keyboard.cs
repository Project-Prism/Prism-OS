using Cosmos.System;

namespace PrismTools
{
	public static class Keyboard
	{
		public static bool TryReadKey(out ConsoleKeyInfo Key)
		{
			if (KeyboardManager.TryReadKey(out var KeyX))
			{
				Key = new(KeyX.KeyChar, (ConsoleKey)KeyX.Key, KeyX.Modifiers == ConsoleModifiers.Shift, KeyX.Modifiers == ConsoleModifiers.Alt, KeyX.Modifiers == ConsoleModifiers.Control);
				return true;
			}

			Key = default;
			return false;
		}
	}
}