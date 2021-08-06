using System;
using System.Collections.Generic;
using System.Data;
using static Hexi_Language.Parser;

namespace Hexi_Language
{
    internal class Functions
    {
        public static List<Types.Variable> variables = new List<Types.Variable>();

        private static dynamic GetRealValue(dynamic arg)
        {
            if (arg is Types.Variable)
            {
                foreach (Types.Variable variable in variables)
                {
                    if (variable.name == arg.name)
                    {
                        return variable.property;
                    }
                }
                return null;
            }
            if (arg is Types.MathOp)
            {
                string operation = arg.operation;
                DataTable table = new DataTable();
                table.Columns.Add("expression", typeof(string), operation);
                DataRow row = table.NewRow();
                table.Rows.Add(row);
                return double.Parse((string)row["expression"]);
            }
            return arg;
        }

        public static void Print(dynamic[] args)
        {
            string text = Convert.ToString(GetRealValue(args[0]));
            if (args.Length > 1)
            {
                if (!args[1]) // no newline
                {
                    Console.Write(text);
                    return;
                }
            }
            Console.WriteLine(text);
        }

        public static void Var(dynamic[] args)
        {
            Types.Variable var;
            var.name = args[0];
            var.property = GetRealValue(args[1]);
            int i = 0;
            while (i < variables.Count)
            {
                if (args[0] == variables[i].name) // check if variable exists
                {
                    variables[i] = var;
                    return;
                }
                i++;
            }
            variables.Add(var);
        }

        public static void If_(dynamic[] args)
        {
            dynamic comp1 = GetRealValue(args[1]);
            dynamic comp2 = GetRealValue(args[3]);
            string op = args[2];

            switch (op)
            {
                case "==":
                    if (comp1 == comp2)
                    {
                        Parse(args[0], false);
                    }
                    break;

                case "!=":
                    if (comp1 != comp2)
                    {
                        Parse(args[0], false);
                    }
                    break;
            }
        }
    }
}