using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace JSONParser
{
    class Program
    {
        public static void Main()
        {
            CharReader reader = new CharReader("0:\\test.json");
            Tokenizer tok = new Tokenizer();
            Parser parser = new Parser();
            List<Token> toks = tok.Tokenize(ref reader);

            JSONObject obj = (JSONObject)parser.Parse(toks);

            Console.WriteLine("\nPrinting JSON object properties:");

            string first_name = (string)obj.Get("firstName");
            Console.WriteLine("First Name: " + first_name);

            string last_name = (string)obj.Get("lastName");
            Console.WriteLine("Last Name: " + last_name);

            JSONObject address = (JSONObject)obj.Get("address");
            string street = (string)address.Get("streetAddress");
            Console.WriteLine("Street Address: " + street);
            
        }
    }
}
