using System;
using System.Collections.Generic;
using System.IO;
using static System.Text.Encoding;

namespace PrismOS.Storage
{
    /// <summary>
    /// Arcfile, made by terminal.cs. (Version 2.0)
    /// https://github.com/terminal-cs/Arc
    /// </summary>
    public class ArcFile : IDisposable
    {
        private Dictionary<string, byte[]> Pairs { get; } = new();
        private string Path { get; }

        /// <summary>
        /// An easy way to store values in a file.
        /// </summary>
        /// <param name="xPath"></param>
        public ArcFile(string xPath)
        {
            Path = xPath;
            if (File.Exists(Path))
            {
                foreach (string Ln in File.ReadAllLines(Path))
                {
                    string[] Split = Ln.Split(" => ");
                    Pairs.Add(Split[0], UTF8.GetBytes(Split[1]));
                }
            }
            else
            {
                File.Create(Path);
            }
        }

        /// <summary>
        /// Remo the specified key if it is present.
        /// </summary>
        /// <param name="Key"></param>
        public void Remove(string Key) => Pairs.Remove(Key);

        /// <summary>
        /// Assign a value to a key.
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void Assign(string Key, byte[] Value) => Pairs[Key] = Value;

        /// <summary>
        /// Collect the value from a key.
        /// </summary>
        /// <param name="Key"></param>
        /// <returns>The value of the key if it is present.</returns>
        public virtual byte[] Collect(string Key) => Pairs[Key];

        /// <summary>
        /// Export the Arc file.
        /// </summary>
        public void Export()
        {
            string Final = "";
            foreach(KeyValuePair<string, byte[]> Pair in Pairs)
            {
                Final += Pair.Key + " => " + Pair.Value;
            }
        }

        /// <summary>
        /// Export and Dispose of the Arc file.
        /// </summary>
        public void Dispose()
        {
            Export();
            //GC.SuppressFinalize(this);
            //GC.Collect();
        }
    }
}