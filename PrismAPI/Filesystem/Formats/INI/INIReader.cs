namespace PrismAPI.Filesystem.Formats.INI;

public class INIReader
{
    public INIReader(string Source)
    {
        this.Source = Source;
    }

    #region Methods

    #region Unsigned

    public bool TryReadUShort(string Key, out ushort Value)
    {
        if (TryReadString(Key, out string S))
        {
            return ushort.TryParse(S, out Value);
        }
        Value = 0;
        return false;
    }
    public bool TryReadUInt(string Key, out uint Value)
    {
        if (TryReadString(Key, out string S))
        {
            return uint.TryParse(S, out Value);
        }
        Value = 0;
        return false;
    }
    public bool TryReadULong(string Key, out ulong Value)
    {
        if (TryReadString(Key, out string S))
        {
            return ulong.TryParse(S, out Value);
        }
        Value = 0;
        return false;
    }

    #endregion

    #region Signed

    public bool TryReadShort(string Key, out short Value)
    {
        if (TryReadString(Key, out string S))
        {
            return short.TryParse(S, out Value);
        }
        Value = 0;
        return false;
    }
    public bool TryReadInt(string Key, out int Value)
    {
        if (TryReadString(Key, out string S))
        {
            return int.TryParse(S, out Value);
        }
        Value = 0;
        return false;
    }
    public bool TryReadLong(string Key, out long Value)
    {
        if (TryReadString(Key, out string S))
        {
            return long.TryParse(S, out Value);
        }
        Value = 0;
        return false;
    }

    #endregion

    #region General

    public bool TryReadString(string Key, out string Value)
    {
        for (int I = 0; I < Source.Length; I++)
        {
            if (Source[I] == '=' && Validate(Key, I))
            {
                I++;
                Value = string.Empty;
                while (I < Source.Length && Source[I] != ' ' && Source[I] != '\n' && Source[I] != '\r')
                {
                    Value += Source[I++];
                }
                return true;
            }
        }
        Value = string.Empty;
        return false;
    }
    public bool TryReadDouble(string Key, out double Value)
    {
        if (TryReadString(Key, out string S))
        {
            return double.TryParse(S, out Value);
        }
        Value = 0;
        return false;
    }
    public bool TryReadFloat(string Key, out float Value)
    {
        if (TryReadString(Key, out string S))
        {
            return float.TryParse(S, out Value);
        }
        Value = 0;
        return false;
    }
    public bool TryReadByte(string Key, out byte Value)
    {
        if (TryReadString(Key, out string S))
        {
            return byte.TryParse(S, out Value);
        }
        Value = 0;
        return false;
    }
    public bool TryReadChar(string Key, out char Value)
    {
        if (TryReadString(Key, out string S))
        {
            return char.TryParse(S, out Value);
        }
        Value = '\0';
        return false;
    }

    #endregion

    #region Misc

    public bool TryWrite(string Key, object Value)
    {
        for (int I = 0; I < Source.Length; I++)
        {
            if (Source[I] == '=' && Validate(Key, I))
            {
                int II = Source.IndexOfAny(new char[] { '\n', '\r' }, ++I);
                Source = Source.Remove(I, II - I);
                Source = Source.Insert(I, Value.ToString() + "");
                return true;
            }
        }
        return false;
    }

    public bool Validate(string Key, int Index)
    {
        if (Index - Key.Length < 0 || Index + Key.Length >= Source.Length)
        { // Return At Invalid Index
            return false;
        }

        return Source[(Index - Key.Length)..Index] == Key;
    }

    public override string ToString()
    {
        return Source;
    }

    #endregion

    #endregion

    #region Fields

    internal string Source;

    #endregion
}