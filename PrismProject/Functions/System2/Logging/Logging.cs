namespace PrismProject.Functions.System2
{
    class Logging
    {
        public enum Type
        {
            Notice,
            Warning,
            Error,
            Crash,
            Other
        }
        public static void AppendLog(Type Err_type, string error)
        {
            IO.Disk.WriteFile("0:\\Log.txt", "\n[" + Err_type.ToString() + "] " + error, true);
        }
    }
}
