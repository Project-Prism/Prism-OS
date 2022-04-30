using System.Collections.Generic;
using System.IO;

namespace PrismOS.Libraries.Utilities
{
    public class Registry : Dictionary<string, object>
    {
        public Registry(byte[] Binary)
        {
            BinaryReader Reader = new(new MemoryStream(Binary));
            int Count = Reader.ReadInt32();
            for (int I = 0; I < Count; I++)
            {
                int Length = Reader.ReadInt32();
                string Key = Reader.ReadString();
                object Value = Reader.ReadBytes(Length);
                Add(Key, Value);
            }
        }
        public Registry()
        {
        }

        public byte[] ToBinary()
        {
            BinaryWriter Writer = new(new MemoryStream());
            Writer.Write(Count);
            foreach (KeyValuePair<string, object> Pair in this)
            {
                Writer.Write(((byte[])Pair.Value).Length);
                Writer.Write(Pair.Key);
                Writer.Write((byte[])Pair.Value);
            }
            return ((MemoryStream)Writer.BaseStream).ToArray();
        }
    }
}