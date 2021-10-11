using System;
using System.Collections.Generic;

namespace PrismProject.Core.Methods
{
    class LZWDecoder
    {
        /// <summary>
        /// Decode data stored in the byte array
        /// </summary>
        /// <param name="data">Array of data to be decoded</param>
        /// <param name="length">How many bytes from the data should be parsed</param>
        /// <param name="maxKeySize">Maximum number of bits the index can have</param>
        /// <param name="dictionarySize">Size of the intial dictionary</param>
        /// <param name="dictionaryBaseOffset">In case for custom initial dictionaries an offset for the intial values can be given</param>
        /// <param name="minIndexSize">The initial index size when starting the decoding process</param>
        /// <param name="isGIF">If the data comes from a GIF. In that case additional codes are present</param>
        /// <param name="LSB">If the data is Least-Significant-Bit first or Most-Significant-Bit first. Is overwritten by isGif</param>
        /// <returns></returns>
        public static byte[] Decode(byte[] data, int maxKeySize = 16, int dictionarySize = 256, int dictionaryBaseOffset = 0, int minIndexSize = 8, bool isGIF = false, bool LSB = false)
        {
            int indexSize = minIndexSize;
            if (maxKeySize > 16)
            {
                throw new ArgumentOutOfRangeException();
            }
            List<byte> output = new List<byte>();
            Dictionary<ushort, byte[]> dictionary = new Dictionary<ushort, byte[]>();
            InitialiseDictionary(dictionarySize, dictionaryBaseOffset, dictionary);

            if (isGIF)
            {
                dictionary[(ushort)dictionary.Count] = null;
                dictionary[(ushort)dictionary.Count] = null; // there are two special values CC and EOF
                LSB = true;
            }

            Types.BinaryStream input = new Types.BinaryStream(data, data.Length) { LSB = LSB };
            if (isGIF)
            {
                input.Position += indexSize;
            }

            byte[] indexArr;
            if (maxKeySize > 8)
            {
                indexArr = new byte[maxKeySize / 8];
            }
            else
            {
                indexArr = new byte[1];
            }
            ushort indexVal;
            byte[] oldValue = new byte[0];
            while (input.CanRead && input.Length - input.Position >= indexSize)
            {
                input.Read(indexArr, 0, indexSize);
                indexVal = ToValue(indexArr, indexSize, LSB);
                if (isGIF && indexVal == dictionarySize)
                {
                    //clear dictionary
                    InitialiseDictionary(dictionarySize, dictionaryBaseOffset, dictionary);
                    if (isGIF)
                    {
                        dictionary[(ushort)dictionary.Count] = null;
                        dictionary[(ushort)dictionary.Count] = null; // there are two special values CC and EOF
                    }
                    indexSize = minIndexSize;
                }
                else if (isGIF && indexVal == dictionarySize + 1)
                {
                    //end
                    break;
                }
                else if (dictionary.ContainsKey(indexVal))
                {
                    output.AddRange(dictionary[indexVal]);
                    if (dictionary.Count < 4096 && oldValue.Length != 0)
                    {
                        var B = dictionary[indexVal][0];
                        dictionary[(ushort)dictionary.Count] = Combine(oldValue, B);
                    }
                    oldValue = dictionary[indexVal];
                }
                else
                {
                    if (dictionary.Count < 4096 && oldValue.Length != 0)
                    {
                        var B = oldValue[0];
                        dictionary[indexVal] = Combine(oldValue, B);
                    }
                    output.AddRange(dictionary[indexVal]);
                    oldValue = dictionary[indexVal];
                }
                if (dictionary.Count > (1 << indexSize) - 1)
                {
                    indexSize++;
                    if (indexSize > maxKeySize)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
            return output.ToArray();
        }

        private static void InitialiseDictionary(int dictionarySize, int dictionaryBaseOffset, Dictionary<ushort, byte[]> dictionary)
        {
            dictionary.Clear();
            for (ushort i = 0; i < dictionarySize; i++)
            {
                dictionary[(ushort)dictionary.Count] = new byte[1] { (byte)(i + dictionaryBaseOffset) };
            }
        }

        private static ushort ToValue(byte[] aArray, int indexSize, bool LSB)
        {
            if (LSB)
            {
                if (aArray.Length == 1 || indexSize <= 8)
                {
                    return aArray[0];
                }
                else
                {
                    return BitConverter.ToUInt16(aArray, 0);
                }
            }
            else
            {
                if (aArray.Length == 1 || indexSize <= 8)
                {
                    return aArray[0];
                }
                else
                {
                    return (ushort)((aArray[0] << 8) + aArray[1]);
                }
            }
        }

        private static byte[] Combine(byte[] a, byte b)
        {
            byte[] c = new byte[a.Length + 1];
            Array.Copy(a, c, a.Length);
            c[a.Length] = b;
            return c;
        }
    }
}