using Cosmos.System;

namespace PrismTools;

/// <summary>
/// Keyboard conversion class.
/// </summary>
public static class KeyboardEx
{
	/// <summary>
	/// Initializes the KeyboardEx class uppon access.
	/// </summary>
	static KeyboardEx()
	{
		// Initialize callback list.
		Callbacks = new();

		// Create timer to call every 150 MS.
		Timer T = new((object? O) =>
		{
			if (TryReadKey(out ConsoleKeyInfo Key))
			{
				for (int I = 0; I < Callbacks.Count; I++)
				{
					Callbacks[I].Invoke(Key);
				}
			}
		}, null, 150, 0);
	}

	#region Methods

	/// <summary>
	/// Registers a new callback to an event to be fired whenever a key is pressed.
	/// </summary>
	/// <param name="CallBack">Action to call uppon a key press.</param>
	public static void RegisterCallback(Action<ConsoleKeyInfo> CallBack)
	{
		Callbacks.Add(CallBack);
	}

	/// <summary>
	/// Attempts to read a key, returns true if a key is pressed.
	/// </summary>
	/// <param name="Key">Key read, if key is available.</param>
	/// <returns>True when key is read.</returns>
	public static bool TryReadKey(out ConsoleKeyInfo Key)
	{
		if (KeyboardManager.TryReadKey(out var KeyX))
		{
			Key = new(KeyX.KeyChar, ConsoleKeyExExtensions.ToConsoleKey(KeyX.Key), KeyX.Modifiers == ConsoleModifiers.Shift, KeyX.Modifiers == ConsoleModifiers.Alt, KeyX.Modifiers == ConsoleModifiers.Control);
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
		ConsoleKeyInfo Key;
		while (!TryReadKey(out Key)) { }
		return Key;
	}

	#endregion

	#region Fields

	private static readonly List<Action<ConsoleKeyInfo>> Callbacks;

	#endregion
}