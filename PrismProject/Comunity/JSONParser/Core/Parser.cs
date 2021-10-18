using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace JSONParser
{
   
    public class JSONObject
    {
        public Dictionary<string, object> Map;

        public JSONObject()
        {
            Map = new Dictionary<string, object>();
         
        }

        public void Put(string key, object value) { Map.Add(key, value); }

        public object Get(string key) 
        { 
            if (!Map.ContainsKey(key)) { Console.WriteLine("Unable to locate key '" + key + "'"); return null; }
            return Map[key]; 
        }

        public Dictionary<string, object>.ValueCollection GetAllKeyValues() { return Map.Values; }

        public JSONObject GetJSONObject(string key)
        {
            if (!Map.ContainsKey(key)) { Console.WriteLine("Invalid key"); return null; }

            object obj = Map[key];
            if (obj.GetType() != typeof(JSONObject)) { Console.WriteLine("Value type is not JSON object"); return null; }
            return (JSONObject)obj;
        }
    }

    public class JSONArray
    {
        public List<object> Data;
        public int Length { get { return Data.Count; } }

        public JSONArray() { Data = new List<object>(); }

        public void Add(object obj) { Data.Add(obj); }

        public object Get(int index) { return Data[index]; }

        public JSONObject GetJSONObject(int index)
        {
            object obj = Data[index];
            if (obj.GetType() != typeof(JSONObject)) { Console.WriteLine("Value type is not JSON object"); return null; }
            return (JSONObject)obj;
        }

        public JSONArray GetJSONArray(int index)
        {
            object obj = Data[index];
            if (obj.GetType() != typeof(JSONArray)) { Console.WriteLine("Value type is not JSON array"); return null; }
            return (JSONArray)obj;
        }
    }

    public class Parser
    {
        public TokenReader Reader;
        private List<TokenType> Expected;

        public object Parse(List<Token> tokens)
        {
            Reader = new TokenReader(tokens);
            Expected = new List<TokenType>();
            return Parse();
        }

        private object Parse()
        {
            Token tok = Reader.Next();

            if (tok == null) { return new JSONObject(); }
            else if (tok.Type == TokenType.BeginObject) { return ParseJSONObject(); }
            else if (tok.Type == TokenType.BeginArray) { return ParseJSONArray(); }
            else { Console.WriteLine("Parse error, invalid token"); return new JSONObject(); }
        }

        private JSONObject ParseJSONObject()
        {
            JSONObject obj = new JSONObject();
            SetExpectedTokens(TokenType.String, TokenType.EndObject);
            string key = string.Empty;
            object value = null;

            while (Reader.HasMore())
            {
                Token tok = Reader.Next();

                switch (tok.Type)
                {
                    case TokenType.BeginObject:
                    {
                        if (!CheckExpectToken(tok.Type)) { return null; }
                        obj.Put(key, ParseJSONObject());
                        SetExpectedTokens(TokenType.Comma, TokenType.EndObject);
                        break;
                    }

                    case TokenType.EndObject:
                    {
                        if (!CheckExpectToken(tok.Type)) { return null; }
                        return obj;
                    }

                    case TokenType.BeginArray:
                    {
                        if (!CheckExpectToken(tok.Type)) { return null; }
                        obj.Put(key, ParseJSONArray());
                        SetExpectedTokens(TokenType.Comma, TokenType.EndObject);
                        break;
                    }

                    case TokenType.Null:
                    {
                        if (!CheckExpectToken(tok.Type)) { return null; }
                        obj.Put(key, null);
                        SetExpectedTokens(TokenType.Comma, TokenType.EndObject);
                        break;
                    }

                    case TokenType.Number:
                    {
                        if (!CheckExpectToken(tok.Type)) { return null; }
                        if (tok.Value.Contains(".")) { obj.Put(key, double.Parse(tok.Value)); }
                        else
                        {
                            long num = long.Parse(tok.Value);
                            if (num > int.MaxValue || num < int.MinValue) { obj.Put(key, num); }
                            else { obj.Put(key, (int)num); }
                        }
                        SetExpectedTokens(TokenType.Comma, TokenType.EndObject);
                        break;
                    }

                    case TokenType.Boolean:
                    {
                        if (!CheckExpectToken(tok.Type)) { return null; }
                        obj.Put(key, tok.Value == "true" ? true : false);
                        SetExpectedTokens(TokenType.Comma, TokenType.EndObject);
                        break;
                    }

                    case TokenType.String:
                    {
                        if (!CheckExpectToken(tok.Type)) { return null; }
                        Token pre_tok = Reader.PeekPrevious();
                        if (pre_tok.Type == TokenType.Colon)
                        {
                            value = tok.Value;
                            obj.Put(key, value);
                            SetExpectedTokens(TokenType.Comma, TokenType.EndObject);
                        }
                        else { key = tok.Value; SetExpectedTokens(TokenType.Colon); }
                        break;
                    }

                    case TokenType.Colon:
                    {
                        if (!CheckExpectToken(tok.Type)) { return null; }
                        SetExpectedTokens(TokenType.Null, TokenType.Number, TokenType.Boolean, TokenType.String, TokenType.BeginObject, TokenType.BeginArray);
                        break;
                    }

                    case TokenType.Comma:
                    {
                        if (!CheckExpectToken(tok.Type)) { return null; }
                        SetExpectedTokens(TokenType.String);
                        break;
                    }

                    case TokenType.EndDocument:
                    {
                        if (!CheckExpectToken(tok.Type)) { return null; }
                        return obj;
                    }

                    default:
                    {
                        Console.WriteLine("Unexpected token");
                        return null;
                    }
                }
            }

            Console.WriteLine("Invalid token");
            return null;
        }

        private JSONArray ParseJSONArray()
        {
            JSONArray array = new JSONArray();
            SetExpectedTokens(TokenType.BeginArray, TokenType.EndArray, TokenType.BeginObject, TokenType.Null, TokenType.Number, TokenType.Boolean, TokenType.String);

            while (Reader.HasMore())
            {
                Token tok = Reader.Next();

                switch (tok.Type)
                {
                    case TokenType.BeginObject:
                    {
                        if (!CheckExpectToken(tok.Type)) { return null; }
                        array.Add(ParseJSONObject());
                        SetExpectedTokens(TokenType.Comma, TokenType.EndArray);
                        break;
                    }

                    case TokenType.BeginArray:
                    {
                        if (!CheckExpectToken(tok.Type)) { return null; }
                        array.Add(ParseJSONArray());
                        SetExpectedTokens(TokenType.Comma, TokenType.EndArray);
                        break;
                    }

                    case TokenType.EndArray:
                    {
                        if (!CheckExpectToken(tok.Type)) { return null; }
                        return array;
                    }

                    case TokenType.Null:
                    {
                        if (!CheckExpectToken(tok.Type)) { return null; }
                        array.Add(null);
                        SetExpectedTokens(TokenType.Comma, TokenType.EndArray);
                        break;
                    }

                    case TokenType.Number:
                    {
                        if (!CheckExpectToken(tok.Type)) { return null; }
                        if (tok.Value.Contains(".")) { array.Add(double.Parse(tok.Value)); }
                        else
                        {
                            long num = long.Parse(tok.Value);
                            if (num > int.MaxValue || num < int.MinValue) { array.Add(num); }
                            else { array.Add((int)num); }
                        }
                        SetExpectedTokens(TokenType.Comma, TokenType.EndArray);
                        break;
                    }

                    case TokenType.Boolean:
                    {
                        if (!CheckExpectToken(tok.Type)) { return null; }
                        array.Add(tok.Value == "true" ? true : false);
                        SetExpectedTokens(TokenType.Comma, TokenType.EndArray);
                        break;
                    }

                    case TokenType.String:
                    {
                        if (!CheckExpectToken(tok.Type)) { return null; }
                        array.Add(tok.Value);
                        SetExpectedTokens(TokenType.Comma, TokenType.EndArray);
                        break;
                    }

                    case TokenType.Comma:
                    {
                        if (!CheckExpectToken(tok.Type)) { return null; }
                        SetExpectedTokens(TokenType.String, TokenType.Null, TokenType.Number, TokenType.Boolean, TokenType.BeginArray, TokenType.BeginObject);
                        break;
                    }

                    case TokenType.EndDocument:
                    {
                        if (!CheckExpectToken(tok.Type)) { return null; }
                        return array;
                    }

                    default: { Console.WriteLine("Unexpected token"); return null; }
                }
            }

            Console.WriteLine("Invalid token");
            return null;
        }

        private void SetExpectedTokens(params TokenType[] toks)
        {
            Expected.Clear();
            foreach (TokenType tok in toks) { Expected.Add(tok); }
        }

        private bool CheckExpectToken(TokenType type)
        {
            int found = 0;
            for (int i = 0; i < Expected.Count; i++)
            {
                if (Expected[i] == type) { found++; }
            }
            
            if (found > 0) { return true; } else 
            { 
                Console.WriteLine("Parse error, invalid token"); return false; 
            }
        }
    }
}
