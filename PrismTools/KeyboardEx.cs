using Cosmos.System;

namespace PrismTools
{
	/// <summary>
	/// Keyboard conversion class.
	/// </summary>
	public static class KeyboardEx
	{
		/// <summary>
		/// Attempts to read a key, returns true if a key is pressed.
		/// </summary>
		/// <param name="Key">Key read, if key is available.</param>
		/// <returns>True when key is read.</returns>
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

		/// <summary>
		/// A non-blocking key read method.
		/// </summary>
		/// <returns>The currently pressed key.</returns>
		public static ConsoleKeyInfo ReadKey()
		{
			TryReadKey(out ConsoleKeyInfo Key);
			return Key;
		}
	}
}