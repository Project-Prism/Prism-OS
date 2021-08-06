namespace Hexi_Language
{
    internal class Types
    {
        public struct Variable
        {
            public string name;
            public dynamic property;
        }

        public struct MathOp
        {
            public string operation;
        }

        public static bool TYPE_STRING(string type)
        {
            if (type[0] == '"' & type[type.Length - 1] == '"') // if within " "
            {
                return true;
            }
            return false;
        }

        public static bool TYPE_BOOLEAN(string type)
        {
            if (type == "true" | type == "false") // if either true or false
            {
                return true;
            }
            return false;
        }

        public static bool TYPE_VAR(string type)
        {
            if (type[0] == '$') // if first character is $
            {
                return true;
            }
            return false;
        }

        public static bool TYPE_INTEGER(string type)
        {
            return double.TryParse(type, out _);
        }

        public static bool TYPE_MATH(string type)
        {
            if (type[0] == '{' & type[type.Length - 1] == '}') // if within { }
            {
                return true;
            }
            return false;
        }
    }
}