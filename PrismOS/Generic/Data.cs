using System;

namespace PrismOS.Generic
{
    public static class Data
    {
        public static string ArrayToString(object[] Array)
        {
            if (Array.GetType() == typeof(string[]))
            {
                string ReturnType = "";
                foreach (string Object in Array)
                {
                    ReturnType += Object + "\n";
                }
                return ReturnType;
            }
            if (Array.GetType() == typeof(int[]))
            {
                string ReturnType = "";
                foreach (string Object in Array)
                {
                    ReturnType += Object;
                }
                return ReturnType;
            }
            if (Array.GetType() == typeof(byte[]))
            {
                string ReturnType = "";
                foreach (char Object in Array)
                {
                    ReturnType += (char)Object;
                }
                return ReturnType;
            }
            else
            {
                throw new("ArrayHelper error: unsuported type: " + typeof(Array));
            }
        }

        public static string ParseString(string String)
        {
            String = String.Replace("\\n", "\n");
            String = String.Replace("\\t", "\t");
            return String;
        }
    }
}
