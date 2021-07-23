using System;
using System.Collections.Generic;
using System.Threading;

namespace Hexi_Language
{
    internal class Parser
    {
        public static void Parse(string code, bool clear = true)
        {
            if (clear) // clear variable cache
            {
                Functions.variables.Clear();
            }
            string mini = code.Replace(System.Environment.NewLine, "").Replace("\t", ""); // remove newlines and tab space

            /* Split at semicolon
             * Doesn't use string.Split so we can specify to stop splitting at ( )
             */
            List<string> lines_ = new List<string>();
            List<char> string_ = new List<char>();
            List<char> carray = new List<char>();
            carray.AddRange(mini);
            bool stopsc = false;
            int i = 0;
            while (i < carray.Count - 1)
            {
                switch (carray[i])
                {
                    case '(':
                        stopsc = true;
                        break;

                    case ')':
                        stopsc = false;
                        break;

                    case ';':
                        if (!stopsc)
                        {
                            lines_.Add(string.Join("", string_.ToArray()));
                            string_.Clear();
                            carray.RemoveAt(i);
                        }
                        break;
                }
                string_.Add(carray[i]);
                i++;
            }
            lines_.Add(string.Join("", string_.ToArray()));
            string_.Clear();
            string[] lines = lines_.ToArray();

            foreach (string line in lines)
            {
                string function = line.Split(null)[0]; // get function
                string withoutfunction = line.Remove(0, line.IndexOf(' ') + 1); // string without function

                ConsoleColor cache = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("-- " + line); // write out what is being parsed (for debugging)
                Console.ForegroundColor = cache;

                switch (function)
                {
                    case "print":
                        Functions.Print(GetArguments(withoutfunction));
                        break;

                    case "var":
                        Functions.Var(GetArguments(withoutfunction));
                        break;

                    case "if":
                        Functions.If_(GetIfArguments(withoutfunction)); // get special arguments for if statement
                        break;
                }
            }
        }

        private static dynamic[] GetArguments(string argsunsplit)
        {
            List<dynamic> argret = new List<dynamic>();

            string[] args = argsunsplit.Split(',');
            foreach (string arg in args)
            {
                argret.Add(GetArgument(arg));
            }

            return argret.ToArray(); // return arguments as array
        }

        private static dynamic[] GetIfArguments(string argsunsplit)
        {
            List<dynamic> argret = new List<dynamic>();

            string withinop = GetWithin(argsunsplit, "[", "]"); // get text within [ ]
            string withincode = GetWithin(argsunsplit, "(", ")"); // get text within ( )
            argret.Add(withincode); // code to run on condition
            string[] op_args = withinop.Split(' ');
            argret.Add(GetArgument(op_args[0])); //compare
            argret.Add(op_args[1]); // operator
            argret.Add(GetArgument(op_args[2])); // compare

            return argret.ToArray(); // return arguments as array
        }

        private static void Error(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Thread.Sleep(-1);
        }

        private static dynamic GetArgument(string arg)
        {
            if (Types.TYPE_STRING(arg)) // if string
            {
                return GetWithin(arg, "\"", "\""); // get text within quotes (aka string)
            }
            else if (Types.TYPE_BOOLEAN(arg)) // if boolean
            {
                switch (arg)
                {
                    case "true":
                        return true;

                    case "false":
                        return false;
                }
            }
            else if (Types.TYPE_VAR(arg)) // if variable
            {
                Types.Variable var = new Types.Variable();
                var.name = arg.Remove(0, 1);
                return var;
            }
            else if (Types.TYPE_INTEGER(arg)) // if integer
            {
                double.TryParse(arg, out double number);
                return number;
            }
            else if (Types.TYPE_MATH(arg)) // if math operation
            {
                Types.MathOp mathop = new Types.MathOp();
                new Types.MathOp(); mathop.operation = GetWithin(arg, "{", "}");
                return mathop;
            }
            else
            {
                return arg;
            }

            return null;
        }

        private static string GetWithin(string arg, string char1, string char2)
        {
            int posFrom = arg.IndexOf(char1);
            int posTo = arg.IndexOf(char2, posFrom + 1);
            return arg.Substring(posFrom + 1, posTo - posFrom - 1);
        }
    }
}