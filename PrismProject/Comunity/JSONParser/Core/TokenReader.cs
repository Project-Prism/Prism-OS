using System;
using System.Collections.Generic;
using System.Text;

namespace JSONParser
{
    public class TokenReader
    {
        public List<Token> Tokens;
        public int Position = 0;

        public TokenReader(List<Token> tokens)
        {
            Tokens = tokens;
            Position = 0;
        }

        public Token Peek() { return Position < Tokens.Count ? Tokens[Position] : null; }

        public Token PeekPrevious() { return Position - 1 < 0 ? null : Tokens[Position - 2]; }

        public Token Next() { return Tokens[Position++]; }

        public bool HasMore() { return Position < Tokens.Count; }
    }
}
