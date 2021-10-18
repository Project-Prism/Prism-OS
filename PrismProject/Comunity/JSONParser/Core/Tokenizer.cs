using System;
using System.Collections.Generic;
using System.Text;

namespace JSONParser
{
    public class Tokenizer
    {
        public CharReader Reader;
        public List<Token> Tokens;

        public List<Token> Tokenize(ref CharReader reader)
        {
            Reader = reader;
            Tokens = new List<Token>();

            for (;;)
            {
                if (!HandleChar()) { Console.WriteLine("Error parsing json"); break; }
                if (!Reader.HasMore()) { break; }
            }

            Console.WriteLine("Finished tokenizing json");
            return Tokens;
        }

        private void AddToken(Token t)
        {
            Tokens.Add(t);
            Console.WriteLine("Added token: " + t.ToString());
        }

        private bool HandleChar()
        {
            char ch = Reader.Next();
            bool unhandled = false;

            switch (ch)
            {
                case '\0': { AddToken(new Token(TokenType.EndDocument, "EOF")); return true; }
                case '{': { AddToken(new Token(TokenType.BeginObject, "{")); return true; }
                case '}': { AddToken(new Token(TokenType.EndObject, "}")); return true; }
                case '[': { AddToken(new Token(TokenType.BeginArray, "[")); return true; }
                case ']': { AddToken(new Token(TokenType.EndArray, "]")); return true; }
                case ':': { AddToken(new Token(TokenType.Colon, ":")); return true; }
                case ',': { AddToken(new Token(TokenType.Comma, ",")); return true; }
                case 'n': { return ReadNull(); }
                case 't': { return ReadBoolean(); }
                case 'f': 
                    {
                        return ReadBoolean(); 
                    }
                case '"': { return ReadString(); }
                case '-': { return ReadNumber(); }
                case ' ': { return true; }
                default: { unhandled = true; break; }
            }

            if (unhandled && char.IsControl(ch)) { return true; }
            if (unhandled && char.IsDigit(ch)) { return ReadNumber(); }
            return false;
        }

        private bool ReadNull()
        {
            if (!(Reader.Next() == 'u' && Reader.Next() == 'l' && Reader.Next() == 'l')) { Console.WriteLine("Error parsing null"); return false; }
            AddToken(new Token(TokenType.Null, "null"));
            return true;
        }

        private bool ReadBoolean()
        {
            if (Reader.Next() == 'r' && Reader.Next() == 'u' && Reader.Next() == 'e') { AddToken(new Token(TokenType.Boolean, "true")); return true; }
            Reader.Back();
            if (Reader.Next() == 'a' && Reader.Next() == 'l' && Reader.Next() == 's' && Reader.Next() == 'e')
            { 
                AddToken(new Token(TokenType.Boolean, "false")); return true; 
            }
            Console.WriteLine("Error parsing boolean");
            return false;
        }

        private bool ReadString()
        {
            string value = string.Empty;
            for (;;)
            {
                char ch = Reader.Next();
                if (ch == '"') { AddToken(new Token(TokenType.String, value)); return true; }
                if (ch == '\0') { Console.WriteLine("Unterminated string"); return false; }
                else { value += ch; }
            }
        }

        private bool ReadNumber()
        {
            string value = string.Empty;
            Reader.Back();
            for (; ; )
            {
                char ch = Reader.Next();
                if (char.IsDigit(ch)) { value += ch; }
                else if (ch == '.') { value += '.'; }
                else if (ch == '-') { value += "-"; }
                else if (ch == ',' || ch == ';' || ch == ']' || ch == '}') { AddToken(new Token(TokenType.Number, value)); Reader.Back(); return true; }
                else { Console.WriteLine("Invalid character in number"); return false; }
            }
        }

        private bool IsEscape()
        {
            char ch = Reader.Next();
            return (ch == '"' || ch == '\\' || ch == 'u' || ch == 'r' || ch == 'n' || ch == 'b' || ch == 't' || ch == 'f');
        }

        private bool IsHex(char ch)
        {
            return ((ch >= '0' && ch <= '9') || ('a' <= ch && ch <= 'f') || ('A' <= ch && ch <= 'F'));
        }
    }
}
