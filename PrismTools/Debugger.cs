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

		#region Methods

		/// <summary>
		/// Log an partial message to the console.
		/// Use one of the following methods to complete it:
		/// <list type="table">
		///		<item><see cref="Success"/></item>
		///		<item><see cref="Warn"/></item>
		///		<item><see cref="Fail"/></item>
		/// </list>
		/// </summary>
		/// <param name="Message">Message to display.</param>
		public void WritePartial(string Message)
		{
			Console.Write($"{Category} > {Message}...");
		}

		/// <summary>
		/// Logs a full info message to the console.
		/// </summary>
		/// <param name="Message">Message details.</param>
		/// <param name="Severity">The severity of the message.</param>
		public void WriteFull(string Message, Severity Severity)
		{
			switch (Severity)
			{
				case Severity.Success:
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write("[ OK ] ");
					break;
				case Severity.Warn:
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.Write("[ WARN ] ");
					break;
				case Severity.Fail:
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write("[ FAIL ] ");
					break;
				case Severity.Info:
					Console.Write("[ INFO ] ");
					break;
			}

			Console.ResetColor();
			Console.WriteLine($"{Category} > {Message}");
		}

		/// <summary>
		/// Logs a success tag to the console.
		/// </summary>
		public static void Success()
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(SuccessPart);
			Console.ResetColor();
		}

		/// <summary>
		/// Logs a warning tag to the console.
		/// </summary>
		public static void Warn()
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(WarnPart);
			Console.ResetColor();
		}

		/// <summary>
		/// Logs an error tag to the console.
		/// </summary>
		public static void Fail()
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(FailPart);
			Console.ResetColor();
		}

		#endregion

		#region Fields

		private static readonly string SuccessPart = " [ OK ]";
		private static readonly string WarnPart = " [ WARN ]";
		private static readonly string FailPart = " [ FAIL ]";

		/// <summary>
		/// Category marker for the debugger instance.
		/// </summary>
		public string Category;

		#endregion
	}
}