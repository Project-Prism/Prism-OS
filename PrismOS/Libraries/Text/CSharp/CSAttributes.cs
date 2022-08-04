using System.Collections.Generic;
using Cosmos.Core;
using System;

namespace PrismOS.Libraries.Text.CSharp
{
    public class CSAttributes : IDisposable
    {
        public CSAttributes()
        {
            Variables = new();
            Arguments = new();
            Classes = new();
            Calls = new();
        }

        public Type Type { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }

        // Use-Specific Variables, Won't Be Used All The Time
        public List<CSAttributes> Variables { get; set; }
        public List<CSAttributes> Arguments { get; set; }
        public List<CSAttributes> Classes { get; set; }
        public List<CSAttributes> Usings { get; set; }
        public List<CSAttributes> Calls { get; set; }

        // Generic Values
        public bool HasArguments { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsPointer { get; set; }
        public bool IsPublic { get; set; }
        public bool IsLambda { get; set; }
        public bool IsStatic { get; set; }
        public bool IsUnsafe { get; set; }
        public bool IsMethod { get; set; }
        public bool IsExtern { get; set; }
        public bool IsConst { get; set; }
        public bool IsClass { get; set; }
        public bool IsNull { get; set; }
        public bool IsCall { get; set; }

        public static CSAttributes ParseClass(string Contents)
        {
            CSAttributes A = new();
            string Builder = "";

            bool IsDeeper = false;
            for (int I = 0; Contents[I] != '}' || IsDeeper; I++)
            {
                if (Contents[I] == '\n' || Contents[I] == '\t')
                {
                    continue;
                }
                if (Contents[I] == ' ')
                {
                    if (Builder == "namespace")
                    {
                        Builder = "";
                        while (Contents[I] != '{')
                        {
                            A.Classes[^1].Name += Contents[I++];
                        }
                        continue;
                    }
                    if (Builder == "using")
                    {
                        Builder = "";
                        A.Classes[^1].Usings.Add(new());
                        while (Contents[I] != ';')
                        {
                            A.Classes[^1].Usings[^1].Name += Contents[I++];
                        }
                        continue;
                    }
                    if (Builder == "public")
                    {
                        A.IsPublic = true;
                        continue;
                    }
                    if (Builder == "static")
                    {
                        A.IsStatic = true;
                        continue;
                    }
                    if (Builder == "const")
                    {
                        A.IsConst = true;
                        continue;
                    }
                    if (Builder == "class")
                    {
                        A.Classes.Add(ParseClass(Contents[I..Contents.Length]));
                        Builder = "";
                        continue;
                    }
                }
            }

            return A;
        }

        public void Dispose()
        {
            GCImplementation.Free(Variables);
            GCImplementation.Free(Arguments);
            GCImplementation.Free(Classes);
            GCImplementation.Free(Usings);
            GCImplementation.Free(Calls);
            GCImplementation.Free(Value);
            GCImplementation.Free(Type);
            GCImplementation.Free(Name);
            GC.SuppressFinalize(this);
        }
    }
}