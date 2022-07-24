using System.Collections.Generic;
using System;

namespace PrismOS.Libraries.Parsing.CSharp
{
    public class Variable
    {
        public Type Type { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
        public List<(Type, string)> Arguments { get; set; }

        public string GetMethod { get; set; }
        public string SetMethod { get; set; }

        public bool IsReadOnly { get; set; }
        public bool IsPointer { get; set; }
        public bool IsPublic { get; set; }
        public bool IsLambda { get; set; }
        public bool IsStatic { get; set; }
        public bool IsUnsafe { get; set; }
        public bool IsMethod { get; set; }
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
            { "var", "System.Dynamic" },
            { "void", "System.Void" },
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

                    Type? T = Type.GetType(Types.ContainsKey(Builder) ? Types[Builder] : Builder);
                    if (T != null)
                    {
                        V.Type = T;
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
                    if (Contents[I + 1] == '>')
                    {
                        V.IsLambda = true;
                        I++;
                    }
                    V.IsNull = false;
                    V.Name = Builder;
                    Builder = "";
                    continue;
                }
                if (Contents[I] == '(')
                {
                    V.IsMethod = true;
                    throw new("Methods Not Implemented!");
                }
                if (Contents[I] == '{')
                {
                    throw new("Get/Set Accessors Not Implemented!");
                }

                // Default If Not Running Specified Function
                Builder += Contents[I];
            }

            throw new("Unexpected EOL (Missing ';')");
        }

        public override string ToString()
        {
            return
                $"IsReadOnly: {IsReadOnly}\n" +
                $"IsPointer: {IsPointer}\n" +
                $"IsPublic: {IsPublic}\n" +
                $"IsLambda: {IsLambda}\n" +
                $"IsStatic: {IsStatic}\n" +
                $"IsUnsafe: {IsUnsafe}\n" +
                $"IsMethod: {IsMethod}\n" +
                $"IsExtern: {IsExtern}\n" +
                $"IsConst: {IsConst}\n" +
                $"IsNull: {IsNull}\n" +
                $"Get: {GetMethod ?? "default"}\n" +
                $"Set: {SetMethod ?? "default"}";
        }
    }
}