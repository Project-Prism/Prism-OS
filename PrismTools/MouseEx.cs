using Cosmos.System;

namespace PrismTools
{
	/// <summary>
	/// A basic mouse extention class, used to detect extra mouse information.
	/// </summary>
	public static class MouseEx
	{
		/// <summary>
		/// A method to check if the mouse is within the given coordinates,
		/// </summary>
		/// <param name="X">The X position to check.</param>
		/// <param name="Y">The Y position to check.</param>
		/// <param name="Width">The Horizontal size to check.</param>
		/// <param name="Height">The Vertical size to check.</param>
		/// <returns>True if the mouse is within the coordinates, Otherwise false.</returns>
		public static bool IsMouseWithin(int X, int Y, ushort Width, ushort Height)
		{
			return
				MouseManager.X >= X && MouseManager.X <= X + Width &&
				MouseManager.Y >= Y && MouseManager.Y <= Y + Height;
		}

		/// <summary>
		/// Checks if the mouse has any buttons pressed.
		/// </summary>
		/// <returns>True if any buttons are pressed, Otherwise False.</returns>
		public static bool IsClickPressed()
		{
			return MouseManager.MouseState != MouseState.None;
		}

		/// <summary>
		/// A method to check if a click event is ready to be fired.
		/// </summary>
		/// <returns>True if a control's click event is ready to fire, Otherwise False.</returns>
		public static bool IsClickReady()
		{
			return
				MouseManager.MouseState == MouseState.None &&
				MouseManager.LastMouseState == MouseState.Left;
		}
	}
}