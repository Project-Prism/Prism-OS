using System;
using System.Collections.Generic;

namespace PrismOS.Libraries.Runtime
{
    public class BatchFile
    {
        public BatchFile(string RawText)
        {
            this.RawText = RawText;
        }

        public bool Echo;
        public int Line = 0;
        public string RawText;
        public Dictionary<string, string> Variables = new();

        public void Cycle()
        {
            string[] Params = RawText.Split('\n')[Line++].Split(' ');

            switch (Params[0].ToLower())
            {
                case "@echo":
                    if (Params.Length > 0)
                    {
                        Echo = Params[1].ToLower() == "on";
                    }
                    else
                    {
                        Console.WriteLine("Echo is " + (Echo ? "enabled." : "disabled."));
                    }
                    return;
                case "echo":
                    if (Params[1].StartsWith('$'))
                    {
                        Console.WriteLine(Variables[Params[1].Replace("$", "")]);
                    }
                    else
                    {
                        for (int I = 1; I < Params.Length; I++)
                        {
                            Console.Write(Params[I] + " ");
                        }
                        Console.WriteLine();
                    }
                    return;
                case "rem":
                    return;
                case "timeout":
                    if (Params[2].StartsWith('$'))
                    {
                        System.Threading.Thread.Sleep(int.Parse(Variables[Params[2]]));
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(int.Parse(Params[2]));
                    }
                    return;
                case "goto":
                    if (Params[1].StartsWith('$'))
                    {
                        Line = int.Parse(Variables[Params[1]]);
                    }
                    else
                    {
                        Line = int.Parse(Params[1]);
                    }
                    return;
                case "cls":
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Unimplemented: " + Params[0]);
                    return;
            }
        }
    }
}