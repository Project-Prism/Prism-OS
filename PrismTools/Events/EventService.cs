using PrismTools.IO;

namespace PrismTools.Events
{
	public static class EventService
	{
		#region Metods

		/// <summary>
		/// The on-tick method, must run every frame.
		/// </summary>
		public static void OnTick()
		{
			if (KeyboardEx.TryReadKey(out ConsoleKeyInfo Key))
			{
				for (int I = 0; I < KeyboardEvents.Count; I++)
				{
					KeyboardEvents[I].Invoke(Key);
				}
			}
		}

		/// <summary>
		/// Registers a new callback to an event to be fired whenever a key is pressed.
		/// </summary>
		/// <param name="CallBack">Action to call uppon a key press.</param>
		public static void RegisterKeyboardEvent(Action<ConsoleKeyInfo> CallBack)
		{
			KeyboardEvents.Add(CallBack);
		}

		#endregion

		#region Fields

		private static List<Action<ConsoleKeyInfo>> KeyboardEvents { get; set; } = new();

		#endregion
	}
}