using System.Text;

namespace PrismAPI.Tools.Compression;

/// <summary>
/// LZW (De)compression class.
/// </summary>
public static class LZW
{
    /// <summary>
    /// Compresses an array or bytes using LZW compression.
    /// </summary>
    /// <param name="Binary">The binary data to be compressed.</param>
    /// <returns>The compressed version of the input.</returns>
    public static byte[] LZWCompress(byte[] Binary)
    {
        // build the dictionary
        Dictionary<string, int> Dictionary = new();
        List<int> Compressed = new();
        string W = "";

        for (int I = 0; I < 256; I++)
        {
            Dictionary.Add(((char)I).ToString(), I);
        }

        for (int I = 0; I < Binary.Length; I++)
        {
            string WC = W + (char)Binary[I];
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
                W = ((char)Binary[I]).ToString();
            }
        }

        // write remaining output if necessary
        if (!string.IsNullOrEmpty(W))
            Compressed.Add(Dictionary[W]);

        byte[] ReturnBytes = new byte[Compressed.Count * sizeof(int)];
        Buffer.BlockCopy(Compressed.ToArray(), 0, ReturnBytes, 0, ReturnBytes.Length);
        return ReturnBytes;
    }

    /// <summary>
    /// Decompresses an array of bytes originaly compressed with LZW compression.
    /// </summary>
    /// <param name="Binary">The binary data to be decompressed.</param>
    /// <returns>the decompressed version of the input.</returns>
    public static byte[] LZWDecompress(byte[] Binary)
    {
        Dictionary<int, string> Dictionary = new();

        int[] CompressedTemp = new int[Binary.Length * sizeof(char)];
        Buffer.BlockCopy(Binary, 0, CompressedTemp, 0, CompressedTemp.GetLength(0));
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
            string entry = string.Empty;
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