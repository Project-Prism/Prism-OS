namespace PrismOS.Libraries.Resource.Text.INI
{
    public struct ININode
    {
        public ININode()
        {
            IsSection = false;
            IsComment = false;
            IsNewLine = false;
            IsSpace = false;
            Value = "";
            Name = "";
        }

        #region Definitions

        internal bool IsSection;
        internal bool IsComment;
        internal bool IsNewLine;
        internal bool IsSpace;
        internal object Value;
        internal string Name;

        #endregion

        #region Unsigned Getters

        public bool TryGetULong(out ulong N)
        {
            try
            {
                return ulong.TryParse(Value.ToString(), out N);
            }
            catch
            {
                N = 0;
                return false;
            }
        }
        public bool TryGetUInt(out uint N)
        {
            try
            {
                return uint.TryParse(Value.ToString(), out N);
            }
            catch
            {
                N = 0;
                return false;
            }
        }
        public bool TryGetUShort(out ushort N)
        {
            try
            {
                return ushort.TryParse(Value.ToString(), out N);
            }
            catch
            {
                N = 0;
                return false;
            }
        }

        #endregion

        #region Signed Getters

        public bool TryGetString(out string S)
        {
            try
            {
                S = Value.ToString();
                return true;
            }
            catch
            {
                S = "";
                return false;
            }
        }
        public bool TryGetDouble(out double N)
        {
            try
            {
                return double.TryParse(Value.ToString(), out N);
            }
            catch
            {
                N = 0;
                return false;
            }
        }
        public bool TryGetFloat(out float N)
        {
            try
            {
                return float.TryParse(Value.ToString(), out N);
            }
            catch
            {
                N = 0;
                return false;
            }
        }
        public bool TryGetLong(out long N)
        {
            try
            {
                return long.TryParse(Value.ToString(), out N);
            }
            catch
            {
                N = 0;
                return false;
            }
        }
        public bool TryGetInt(out int N)
        {
            try
            {
                return int.TryParse(Value.ToString(), out N);
            }
            catch
            {
                N = 0;
                return false;
            }
        }
        public bool TryGetShort(out short N)
        {
            try
            {
                return short.TryParse(Value.ToString(), out N);
            }
            catch
            {
                N = 0;
                return false;
            }
        }
        public bool TryGetByte(out byte N)
        {
            try
            {
                return byte.TryParse(Value.ToString(), out N);
            }
            catch
            {
                N = 0;
                return false;
            }
        }
        public bool TryGetChar(out char C)
        {
            try
            {
                return char.TryParse(Value.ToString(), out C);
            }
            catch
            {
                C = '\0';
                return false;
            }
        }
        public bool TryGetBool(out bool B)
        {
            try
            {
                return bool.TryParse(Value.ToString(), out B);
            }
            catch
            {
                B = false;
                return false;
            }
        }

        #endregion

        #region Setters

        public void Setvalue(object Value)
        {
            this.Value = Value;
        }
        public void SetName(string Name)
        {
            this.Name = Name;
        }

        #endregion

        public bool GetIsSection()
        {
            return IsSection;
        }
        public bool GetIsComment()
        {
            return IsComment;
        }
        public bool GetIsNewLine()
        {
            return IsNewLine;
        }
        public bool GetIsSpace()
        {
            return IsSpace;
        }

        public override string ToString()
        {
            if (GetIsSection())
            {
                return '[' + Value.ToString() + ']';
            }
            if (GetIsComment())
            {
                return '#' + Value.ToString();
            }
            if (GetIsNewLine())
            {
                return "\n";
            }
            if (GetIsSpace())
            {
                return " ";
            }

            TryGetString(out string S);
            return Name + "=" + S;
        }
    }
}