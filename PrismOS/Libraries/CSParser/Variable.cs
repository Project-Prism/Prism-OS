using System.Collections.Generic;
using System;

namespace PrismOS.Libraries.CSParser
{
    public class Variable
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public Type Type { get; set; }

        public bool IsReadOnly { get; set; }
        public bool IsPointer { get; set; }
        public bool IsPublic { get; set; }
        public bool IsStatic { get; set; }
        public bool IsUnsafe { get; set; }
        public bool IsExtern { get; set; }
        public bool IsConst { get; set; }
        public bool IsNull { get; set; }

        public static readonly Dictionary<string, string> Types = new()
        {
            { "string", "System.String" },
            { "byte", "System.Byte" },
            { "char", "System.Char" },
            { "short", "System.Int16" },
            { "int", "System.Int32" },
            { "long", "System.Int64" },
            { "ushort", "System.UInt16" },
            { "uint", "System.UInt32" },
            { "ulong", "System.UInt64" },
        };

        public static Variable Parse(string Contents)
        {
            string Builder = "";
            Variable V = new();

            for (int I = 0; I < Contents.Length; I++)
            {
                if (Contents[I] == ' ')
                {
                    if (Builder == "public")
                    {
                        V.IsPublic = true;
                        Builder = "";
                        continue;
                    }
                    if (Builder == "static")
                    {
                        if (V.IsConst)
                        {
                            throw new("Variable cannot be both static and const.");
                        }
                        V.IsStatic = true;
                        Builder = "";
                        continue;
                    }
                    if (Builder == "const")
                    {
                        if (V.IsStatic)
                        {
                            throw new("Variable cannot be both const and static.");
                        }
                        V.IsConst = true;
                        continue;
                    }
                    if (Builder == "readonly")
                    {
                        V.IsReadOnly = true;
                        Builder = "";
                        continue;
                    }
                    if (Builder == "unsafe")
                    {
                        V.IsUnsafe = true;
                        Builder = "";
                        continue;
                    }
                    if (Builder == "extern")
                    {
                        V.IsExtern = true;
                        Builder = "";
                        continue;
                    }

                    if (Types.ContainsKey(Builder))
                    {
                        Builder = Types[Builder];
                    }
                    if (Type.GetType(Builder) != null)
                    {
                        V.Type = Type.GetType(Builder);
                        V.IsPointer = Builder.EndsWith("*");
                        Builder = "";
                        continue;
                    }
                    continue;
                }
                if (Contents[I] == '"')
                {
                    while (Contents[I] != '"')
                    {
                        Builder += Contents[I++];
                    }
                    V.Value = Builder;
                    Builder = "";
                    continue;
                }
                if (Contents[I] == ';')
                {
                    if (V.Name == null)
                    {
                        V.Name = Builder;
                    }
                    if (V.Value == null)
                    {
                        V.Value = Builder;
                    }
                    if (V.Value == null)
                    {
                        V.IsNull = true && !V.IsExtern;
                    }
                    if (V.Type == null)
                    {
                        throw new("Type Not specified!");
                    }

                    return V;
                }
                if (Contents[I] == '=')
                {
                    V.IsNull = false;
                    V.Name = Builder;
                    Builder = "";
                    continue;
                }

                // Default If Not Running Specified Function
                Builder += Contents[I];
            }

            throw new("Unexpected EOL (Missing ';')");
        }
    }
}