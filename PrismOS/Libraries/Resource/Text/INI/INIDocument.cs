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

        public bool TryReadString(string Key, out string Value)
        {
            for (int I = 0; I < Source.Length; I++)
            {
                if (Source[I] == '=' && Validate(Key, I))
                {
                    int II = Source.IndexOfAny(new char[] { '\n', '\r', '\0' }, ++I);
                    Value = Source[I..II];
                    return true;
                }
            }
            Value = string.Empty;
            return false;
        }
        public bool TryReadByte(string Key, out byte Value)
        {
            for (int I = 0; I < Source.Length; I++)
            {
                if (Source[I] == '=' && Validate(Key, I))
                {
                    int II = Source.IndexOfAny(new char[] { '\n', '\r' }, ++I);
                    return byte.TryParse(Source[I..II], out Value);
                }
            }
            Value = 0;
            return false;
        }
        public bool TryReadChar(string Key, out char Value)
        {
            for (int I = 0; I < Source.Length; I++)
            {
                if (Source[I] == '=' && Validate(Key, I))
                {
                    int II = Source.IndexOfAny(new char[] { '\n', '\r' }, ++I);
                    return char.TryParse(Source[I..II], out Value);
                }
            }
            Value = '\0';
            return false;
        }
        public bool TryReaddouble(string Key, out double Value)
        {
            for (int I = 0; I < Source.Length; I++)
            {
                if (Source[I] == '=' && Validate(Key, I))
                {
                    int II = Source.IndexOfAny(new char[] { '\n', '\r' }, ++I);
                    return double.TryParse(Source[I..II], out Value);
                }
            }
            Value = '\0';
            return false;
        }
        public bool TryReadFloat(string Key, out float Value)
        {
            for (int I = 0; I < Source.Length; I++)
            {
                if (Source[I] == '=' && Validate(Key, I))
                {
                    int II = Source.IndexOfAny(new char[] { '\n', '\r' }, ++I);
                    return float.TryParse(Source[I..II], out Value);
                }
            }
            Value = '\0';
            return false;
        }

        #endregion

        #region Signed

        public bool TryReadShort(string Key, out short Value)
        {
            for (int I = 0; I < Source.Length; I++)
            {
                if (Source[I] == '=' && Validate(Key, I))
                {
                    int II = Source.IndexOfAny(new char[] { '\n', '\r' }, ++I);
                    return short.TryParse(Source[I..II], out Value);
                }
            }
            Value = 0;
            return false;
        }
        public bool TryReadInt(string Key, out int Value)
        {
            for (int I = 0; I < Source.Length; I++)
            {
                if (Source[I] == '=' && Validate(Key, I))
                {
                    int II = Source.IndexOfAny(new char[] { '\n', '\r' }, ++I);
                    return int.TryParse(Source[I..II], out Value);
                }
            }
            Value = 0;
            return false;
        }
        public bool TryReadLong(string Key, out long Value)
        {
            for (int I = 0; I < Source.Length; I++)
            {
                if (Source[I] == '=' && Validate(Key, I))
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

        public bool TryReadUShort(string Key, out ushort Value)
        {
            for (int I = 0; I < Source.Length; I++)
            {
                if (Source[I] == '=' && Validate(Key, I))
                {
                    int II = Source.IndexOfAny(new char[] { '\n', '\r' }, ++I);
                    return ushort.TryParse(Source[I..II], out Value);
                }
            }
            Value = 0;
            return false;
        }
        public bool TryReadUInt(string Key, out uint Value)
        {
            for (int I = 0; I < Source.Length; I++)
            {
                if (Source[I] == '=' && Validate(Key, I))
                {
                    int II = Source.IndexOfAny(new char[] { '\n', '\r' }, ++I);
                    return uint.TryParse(Source[I..II], out Value);
                }
            }
            Value = 0;
            return false;
        }
        public bool TryReadULong(string Key, out ulong Value)
        {
            for (int I = 0; I < Source.Length; I++)
            {
                if (Source[I] == '=' && Validate(Key, I))
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

        public bool TryWrite(string Key, object Value)
        {
            for (int I = 0; I < Source.Length; I++)
            {
                if (Source[I] == '=' && Validate(I, Key))
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

        public bool Validate(string Key, int Index)
        {
            if ((Index - Key.Length) == 0 || Index + Key.Length > Source.Length)
            { // Return At Invalid Index
                return false;
            }
            if (Source[(Index - Key.Length)..Index] == Key)
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