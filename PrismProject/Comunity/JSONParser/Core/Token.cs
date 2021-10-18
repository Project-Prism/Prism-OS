using System;
using System.Collections.Generic;
using System.Text;

namespace JSONParser
{
    public enum TokenType
    {
        Error = 0,
        BeginObject = 1,
        EndObject = 2,
        BeginArray = 4,
        EndArray = 8,
        Null = 16,
        Number = 32,
        String = 64,
        Boolean = 128,
        Colon = 256,
        Comma = 512,
        EndDocument = 1024,
    }

    public class Token
    {
        public TokenType Type;
        public string Value;

        public Token()
        {
            Type = TokenType.Error;
            Value = string.Empty;
        }

        public Token(TokenType type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        public override string ToString()
        {
            return "{ TYPE = '" + Type.ToString() + "'  VALUE = '" + Value + "' }";
        }
    }
}
