using System;
using System.Collections.Generic;
using System.Text;

namespace PrismOS.Libraries.Compression
{
    public static class LZW
    {
        public static byte[] LZWCompress(byte[] RawData)
        {
            // build the dictionary
            Dictionary<string, int> Dictionary = new();
            List<int> Compressed = new();
            string W = "";

            for (int I = 0; I < 256; I++)
            {
                Dictionary.Add(((char)I).ToString(), I);
            }

            foreach (char C in RawData)
            {
                string WC = W + C;
                if (Dictionary.ContainsKey(WC))
                {
                    W = WC;
                }
                else
                {
                    // write w to output
                    Compressed.Add(Dictionary[W]);
                    // wc is a new sequence; add it to the dictionary
                    Dictionary.Add(WC, Dictionary.Count);
                    W = C.ToString();
                }
            }

            // write remaining output if necessary
            if (!string.IsNullOrEmpty(W))
                Compressed.Add(Dictionary[W]);

            byte[] ReturnBytes = new byte[Compressed.Count * sizeof(int)];
            Buffer.BlockCopy(Compressed.ToArray(), 0, ReturnBytes, 0, ReturnBytes.Length);
            return ReturnBytes;
        }

        public static byte[] LZWDecompress(byte[] CompressedRawData)
        {
            Dictionary<int, string> Dictionary = new();

            int[] CompressedTemp = new int[CompressedRawData.Length * sizeof(byte)];
            Buffer.BlockCopy(CompressedRawData, 0, CompressedTemp, 0, CompressedTemp.Length);
            List<int> Compressed = new();
            for (int I = 0; I < CompressedTemp.Length; I++)
            {
                Compressed.Add(CompressedTemp[I]);
            }

            // build the dictionary
            for (int I = 0; I < 256; I++)
            {
                Dictionary.Add(I, ((char)I).ToString());
            }

            string W = Dictionary[Compressed[0]];
            Compressed.RemoveAt(0);
            StringBuilder decompressed = new(W);

            foreach (int k in Compressed)
            {
                string entry = null;
                if (Dictionary.ContainsKey(k))
                    entry = Dictionary[k];
                else if (k == Dictionary.Count)
                    entry = W + W[0];

                decompressed.Append(entry);

                // new sequence; add it to the dictionary
                Dictionary.Add(Dictionary.Count, W + entry[0]);

                W = entry;
            }

            return Encoding.ASCII.GetBytes(decompressed.ToString());
        }
    }
}