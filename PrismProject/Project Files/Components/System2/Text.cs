namespace PrismProject.System2
{
    internal class Text
    {
        public static void CheckLength(string text, int MinLength, int MaxLength)
        {
            if (text.Length < MinLength)
                System.Console.WriteLine("Insufficient arguments.");
            else if (text.Length > MaxLength)
                System.Console.WriteLine("too many arguments");
        }
    }
}
