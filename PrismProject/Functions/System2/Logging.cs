using System.Collections.Generic;

namespace PrismProject.Functions.System2
{
    class Logging
    {
        public static List<string> Log = new List<string>();

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
            Log.Add("\n[" + Err_type.ToString() + "] " + error);
        }

        public static void Stash()
        {
            string Data = "";
            foreach (string line in Log)
            {
                Data = "\n" + line;
            }
            IO.Disk.WriteFile("0:\\Log.txt", Data, true);
        }
    }
}
