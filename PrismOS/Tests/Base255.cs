using System.Collections.Generic;
using System;
using Cosmos.System.Graphics;

namespace PrismOS.Tests
{
    public static class Base255
    {
        public static byte[] FromBase255String(string Base255)
        {
            List<byte> Bytes = new();
            foreach (char Char in Base255)
            {
                Bytes.Add((byte)Char);
            }
            return Bytes.ToArray();
        }

        public static string FromBytes(byte[] Bytes)
        {
            string S = "";
            foreach (byte Byte in Bytes)
            {
                S += (char)Byte;
            }
            return S;
        }

        public static string FromBase64String(string Base64)
        {
            return FromBytes(Convert.FromBase64String(Base64));
        }

        public static string ToBase64String(string Base255)
        {
            return Convert.ToBase64String(FromBase255String(Base255));
        }

        public static string FromFile(string File)
        {
            return FromBytes(System.IO.File.ReadAllBytes(File));
        }
    }
}