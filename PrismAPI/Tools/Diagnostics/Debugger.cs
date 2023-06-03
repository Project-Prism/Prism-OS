namespace PrismAPI.Tools.Diagnostics;

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
    /// Logs a full info message to the console.
    /// </summary>
    /// <param name="Message">Message details.</param>
    /// <param name="Severity">The severity of the message.</param>
    public void WriteFull(string Message, Severity Severity)
    {
        // Print out the category
        Console.Write($"{Category} ");

        // Switch to the correct colors.
        switch (Severity)
        {
            case Severity.Success:
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case Severity.Warn:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case Severity.Fail:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case Severity.Info:
                Console.ForegroundColor = ConsoleColor.Cyan;
                break;
        }

        // Write status, then reset.
        Console.Write('>');
        Console.ResetColor();

        // Print out the main message.
        Console.WriteLine($" {Message}");
    }

    /// <summary>
    /// Log an partial message to the console.
    /// Use <see cref="Finalize(Severity)"/> to mark it's status.
    /// </summary>
    /// <param name="Message">Message to display.</param>
    public void WritePartial(string Message)
    {
        // Print out the category
        Console.Write($"{Category} > {Message}");
    }

    /// <summary>
    /// Finalizes a log status, use after <see cref="WritePartial(string)"/>.
    /// </summary>
    /// <param name="Severity">The severity to give the message.</param>
    public void Finalize(Severity Severity)
    {
        // Offset the position to reset the '>' character.
        Console.CursorLeft = Category.Length + 1;

        // Switch to the correct colors.
        switch (Severity)
        {
            case Severity.Success:
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case Severity.Warn:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case Severity.Fail:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case Severity.Info:
                Console.ForegroundColor = ConsoleColor.Cyan;
                break;
        }

        // Re-write the value and reset the color.
        Console.Write('>');
        Console.ResetColor();

        // Increment the console line.
        Console.CursorLeft = 0;
        Console.CursorTop++;
    }

    #endregion

    #region Fields

    /// <summary>
    /// Category marker for the debugger instance.
    /// </summary>
    public string Category;

    #endregion
}