using System.Collections.Generic;
using System;

namespace PrismOS.Libraries.Text.Generic
{
    public class Lexer : List<Property>, IDisposable
    {
        public static bool CheckSkip(char C)
        {
            return C == '\n' || C == '\r' || C == '\t' || C == ' ';
        }

        public void Dispose()
        {
            for (int I = 0; I < Count; I++)
            {
                this[I].Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}