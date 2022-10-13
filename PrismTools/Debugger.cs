namespace PrismTools
{
	/// <summary>
	/// Debugger class, used for logging errors.
	/// </summary>
	public class Debugger
	{
		/// <summary>
		/// Creates a new instance of the <see cref="Debugger"/> class.
		/// </summary>
		/// <param name="Category">Category to print as (e.g. Kernel, Graphics... etc)</param>
		public Debugger(string Category)
		{
			this.Category = Category;
		}

		#region Structure

		/// <summary>
		/// List of strings mapped to the <see cref="Severity"/> enum.
		/// </summary>
		public string[] SeverityStrings =
		{
			"FATAL",
			"WARN",
			"INFO",
			"OK",
		};
		/// <summary>
		/// Different severity modes to better debug output.
		/// </summary>
		public enum Severity
		{
			Critical,
			Warning,
			Info,
			Ok,
		}

		#endregion

		#region Methods

		/// <summary>
		/// Log an error to the console.
		/// </summary>
		/// <param name="Message">Message to display.</param>
		/// <param name="S">Sevetiry of the message.</param>
		public void Log(string Message, Severity S = Severity.Info)
		{
			switch (S)
			{
				case Severity.Warning:
					Console.ForegroundColor = ConsoleColor.Yellow;
					break;
				case Severity.Critical:
					Console.ForegroundColor = ConsoleColor.Red;
					break;
				case Severity.Ok:
					Console.ForegroundColor = ConsoleColor.Green;
					break;
			}
			Console.Write($"[ {SeverityStrings[(byte)S]} ] ");
			Console.ResetColor();
			Console.WriteLine($"{Category} > {Message}");
		}

		#endregion

		#region Fields

		/// <summary>
		/// Category marker for the debugger instance.
		/// </summary>
		public string Category;

		#endregion
	}
}