namespace PrismOS.Libraries.Resource.Text.INI
{
    public class INIDocument
    {
        public INIDocument(string Source)
        {
            this.Source = Source;
        }

        #region Definitions

        internal string Source;

        #endregion

        #region General

        public bool TryReadString(string Name, out string Value)
        {
            for (int I = 0; I < Source.Length; I++)
            {
                if (Source[I] == '=' && Validate(I, Name))
                {
                    int II = Source.IndexOfAny(new char[] { '\n', '\r', '\0' }, ++I);
                    Value = Source[I..II];
                    return true;
                }
            }
            Value = string.Empty;
            return false;
        }
        public bool TryReadByte(string Name, out byte Value)
        {
            for (int I = 0; I < Source.Length; I++)
            {
                if (Source[I] == '=' && Validate(I, Name))
                {
                    int II = Source.IndexOfAny(new char[] { '\n', '\r' }, ++I);
                    return byte.TryParse(Source[I..II], out Value);
                }
            }
            Value = 0;
            return false;
        }
        public bool TryReadChar(string Name, out char Value)
        {
            for (int I = 0; I < Source.Length; I++)
            {
                if (Source[I] == '=' && Validate(I, Name))
                {
                    int II = Source.IndexOfAny(new char[] { '\n', '\r' }, ++I);
                    return char.TryParse(Source[I..II], out Value);
                }
            }
            Value = '\0';
            return false;
        }

        #endregion

        #region Signed

        public bool TryReadShort(string Name, out short Value)
        {
            for (int I = 0; I < Source.Length; I++)
            {
                if (Source[I] == '=' && Validate(I, Name))
                {
                    int II = Source.IndexOfAny(new char[] { '\n', '\r' }, ++I);
                    return short.TryParse(Source[I..II], out Value);
                }
            }
            Value = 0;
            return false;
        }
        public bool TryReadInt(string Name, out int Value)
        {
            for (int I = 0; I < Source.Length; I++)
            {
                if (Source[I] == '=' && Validate(I, Name))
                {
                    int II = Source.IndexOfAny(new char[] { '\n', '\r' }, ++I);
                    return int.TryParse(Source[I..II], out Value);
                }
            }
            Value = 0;
            return false;
        }
        public bool TryReadLong(string Name, out long Value)
        {
            for (int I = 0; I < Source.Length; I++)
            {
                if (Source[I] == '=' && Validate(I, Name))
                {
                    int II = Source.IndexOfAny(new char[] { '\n', '\r' }, ++I);
                    return long.TryParse(Source[I..II], out Value);
                }
            }
            Value = 0;
            return false;
        }

        #endregion

        #region Unsigned

        public bool TryReadUShort(string Name, out ushort Value)
        {
            for (int I = 0; I < Source.Length; I++)
            {
                if (Source[I] == '=' && Validate(I, Name))
                {
                    int II = Source.IndexOfAny(new char[] { '\n', '\r' }, ++I);
                    return ushort.TryParse(Source[I..II], out Value);
                }
            }
            Value = 0;
            return false;
        }
        public bool TryReadUInt(string Name, out uint Value)
        {
            for (int I = 0; I < Source.Length; I++)
            {
                if (Source[I] == '=' && Validate(I, Name))
                {
                    int II = Source.IndexOfAny(new char[] { '\n', '\r' }, ++I);
                    return uint.TryParse(Source[I..II], out Value);
                }
            }
            Value = 0;
            return false;
        }
        public bool TryReadULong(string Name, out ulong Value)
        {
            for (int I = 0; I < Source.Length; I++)
            {
                if (Source[I] == '=' && Validate(I, Name))
                {
                    int II = Source.IndexOfAny(new char[] { '\n', '\r' }, ++I);
                    return ulong.TryParse(Source[I..II], out Value);
                }
            }
            Value = 0;
            return false;
        }

        #endregion

        #region Write

        public bool TryWrite(string Name, object Value)
        {
            for (int I = 0; I < Source.Length; I++)
            {
                if (Source[I] == '=' && Validate(I, Name))
                {
                    int II = Source.IndexOfAny(new char[] { '\n', '\r' }, ++I);
                    Source = Source.Remove(I, II - I);
                    Source = Source.Insert(I, Value.ToString());
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Misc

        public bool Validate(int I, string Name)
        {
            if ((I - Name.Length) == 0 || I + Name.Length > Source.Length)
            { // Return At Invalid Index
                return false;
            }
            if (Source[(I - Name.Length)..I] == Name)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return Source;
        }

        #endregion
    }
}