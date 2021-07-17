using System.Collections.Generic;

namespace PrismProject
{
    class Parser
    {
        public static void Parse(string code)
        {
            string mini = code.Replace(System.Environment.NewLine, "");
            string[] lines = mini.Split(';');

            foreach (string line in lines)
            {
                string function = line.Split(null)[0];
                string withoutfunction = line.Remove(0, line.IndexOf(' ') + 1);

                if (function == "console.out")
                {
                    dynamic[] args = GetArguments(withoutfunction);
                    Functions.consoleout(args);
                }
            }
        }

        private static dynamic[] GetArguments(string argsunsplit)
        {
            List<dynamic> argret = new List<dynamic>();

            string[] args = argsunsplit.Split(',');
            foreach (string arg in args)
            {
                if (arg[0] == '"' || arg[arg.Length - 1] == '"') // if string
                {
                    argret.Add(GetString(arg));
                }
                else if (arg == "true" | arg == "false")
                {
                    switch (arg)
                    {
                        case "true":
                            argret.Add(true);
                            break;
                        case "false":
                            argret.Add(false);
                            break;
                    }
                }
            }

            return argret.ToArray();
        }

        private static string GetString(string arg)
        {
            int from = arg.IndexOf("\"") + "\"".Length;
            int to = arg.LastIndexOf("\"");

            return arg.Substring(from, to - from);
        }
    }
}
