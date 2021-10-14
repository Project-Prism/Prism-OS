using System.Collections.Generic;

namespace PrismProject.Services
{
    class Log_Service
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
            Filesystem.Functions.WriteFile("0:\\Log.txt", Data, true);
        }
    }
}
