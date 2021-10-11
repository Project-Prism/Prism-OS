using System;
using System.Collections.Generic;

namespace PrismProject.Core.Types
{
    public class Gif
    {
        public int speed;
        public int gifVersion;
        public int minScreenWidth, minScreenHeight;
        public char[] comment;
        public ushort loopTimes;
        public List<Bitmap> images = new List<Bitmap>();
        public DisposalMethod DisposalMethod;
        public ushort BackgroundColor;
        public ushort TransparentColor;
        public bool Transparency;
        public bool UserInput;
        public ushort DelayTime;
    }

    public struct Bitmap
    {
        public uint[] imageData;
        public uint width;
        public uint height;

        public bool interlaced;

        public int x;
        public int y;
    }

    //http://giflib.sourceforge.net/whatsinagif/animation_and_transparency.html
    public enum DisposalMethod : byte { DrawOver = 1, Restore = 2, Previous = 3, NoDisposalMethod = 0}

    public struct ColorTableEntry
    {
        public uint color;
        public ColorTableEntry(byte r, byte g, byte b)
        {
            color = 255 * 255 * (uint)r + 255 * (uint)g + b;
        }
    }

    public static class GifParser
    {
        public static Gif Parse(byte[] data)
        {
            Gif gif = new Gif();

            #region Header
            if (data[0] != 'G' || data[1] != 'I' || data[2] != 'F')
            {
                throw new Exception("Invalid Gif Format");
            }

            if (data[3] == '8' && data[4] == '7' && data[5] == 'a')
            {
                gif.gifVersion = 1;
            }
            else
            {
                gif.gifVersion = 2;
            }
            #endregion

            gif.minScreenWidth = BitConverter.ToUInt16(data, 6);
            gif.minScreenHeight = BitConverter.ToUInt16(data, 8);

            #region Logical Screen Descriptor
            int globalColorBits = ((data[10] & 0b111) + 1);
            int sizeGlobalColorTable = 1 << globalColorBits;
            bool colorTableSortFlag = (data[10] & 0b1000) == 0b1000;
            int colorResolution = data[10] & 0b1110000 >> 4;
            bool globalColorTableFlag = (data[10] & 0b10000000) == 0b10000000;
            gif.BackgroundColor = data[11];
            int aspectRatio = 1;
            #endregion

            int pos = 13;

            #region Global Color Table
            uint[] colorTable = new uint[sizeGlobalColorTable];
            if (globalColorTableFlag)
            {
                for (int i = 0; i < colorTable.Length; i++)
                {
                    colorTable[i] = (uint)(((uint)data[pos++] << 0x8000) + ((uint)data[pos++] << 0x80) + data[pos++]);
                }
            }
            #endregion
            while (data[pos] != 0x3b)
            {
                while (data[pos] == 0x21)
                {
                    #region Comment Block
                    //TODO: Support them everywhere
                    if (data[pos + 1] == 0xFE)
                    {
                        pos += 2;
                        byte length = data[pos++];
                        char[] comment = new char[length];
                        for (int i = 0; i < length; i++)
                        {
                            comment[i] = (char)data[pos++];
                        }
                        if (data[pos++] != 0)
                        {
                            throw new Exception("Comment block should be over");
                        }
                        if (gif.comment != null)
                        {
                            throw new NotSupportedException("GIFs with more than one comment section are not supported");
                        }
                    }
                    #endregion

                    #region Graphics Control Extension
                    if (data[pos + 1] == 0xF9)
                    {
                        pos += 2;
                        if (data[pos++] != 4)
                        {
                            throw new Exception("Invalid byte size");
                        }
                        byte packed = data[pos++];
                        gif.Transparency = (packed & 1) == 1;
                        gif.UserInput = (packed & 2) == 2;
                        gif.DisposalMethod = (DisposalMethod)(packed >> 2);
                        gif.DelayTime = BitConverter.ToUInt16(data, pos);
                        pos += 2;
                        gif.TransparentColor = data[pos++];
                        if (data[pos++] != 0)
                        {
                            throw new Exception("Should be at end of GCE");
                        }
                    }
                    #endregion

                    #region Plain Text Extension
                    if (data[pos + 1] == 0x01)
                    {
                        pos += 2;
                        var length = data[pos++];
                        pos += length;
                        if (data[pos++] != 0)
                        {
                            throw new Exception("Should be at end of PTE");
                        }
                    }
                    #endregion

                    #region Application Extension
                    if (data[pos + 1] == 0xFF)
                    {
                        pos += 2;
                        var length = data[pos++];
                        if (length != 0x0B)
                        {
                            throw new Exception("Invalid Length: Can only handle NETSCAPE Application Extensions");
                        }
                        pos += 11; //Skip NETSCAPE2.0
                        if (data[pos++] != 3)
                        {
                            throw new Exception("Unexpected value");
                        }
                        if (data[pos++] != 1)
                        {
                            throw new Exception("Unexpected value");
                        }
                        gif.loopTimes = BitConverter.ToUInt16(data, pos);
                        pos += 2;
                        if (data[pos++] != 0)
                        {
                            throw new Exception("Unexpected value");
                        }
                    }
                    #endregion
                }


                #region Images
                if (data[pos] == 0x2c)
                {
                    pos++;
                    Bitmap image = new Bitmap
                    {
                        x = BitConverter.ToUInt16(data, pos)
                    };
                    pos += 2;
                    image.y = BitConverter.ToUInt16(data, pos);
                    pos += 2;
                    image.width = BitConverter.ToUInt16(data, pos);
                    pos += 2;
                    image.height = BitConverter.ToUInt16(data, pos);
                    pos += 2;
                    image.imageData = new uint[image.width * image.height];

                    bool localColorTableFlag = (data[pos] & 0b1000_0000) == 0b1000_0000;
                    image.interlaced = (data[pos] & 0b0100_0000) == 0b0100_0000;
                    bool sort = (data[pos] & 0b0010_0000) == 0b0010_0000;
                    int localColorBits = ((data[pos] & 0b111) + 1);
                    int localColorTableSize = 1 << localColorBits;
                    pos++;
                    
                    uint[] localColorTable = new uint[localColorTableSize];
                    if (localColorTableFlag)
                    {
                        for (int i = 0; i < localColorTableSize; i++)
                        {
                            localColorTable[i] = (uint)(((uint)data[pos++] << 0x8000) + ((uint)data[pos++] << 0x80) + data[pos++]);
                        }
                    }

                    int compressPos = 0;
                    int minimumCodeSize;
                    if (data[pos] != 0)
                    {
                        minimumCodeSize = data[pos++] + 1;
                    }
                    else
                    {
                        throw new Exception("Invalid code size");
                    }
                    int c = pos;
                    //determine length of all blocks
                    int compressedLength = 0;
                    while (data[c] != 0)
                    {
                        compressedLength += data[c];
                        c += data[c] + 1;
                    }
                    byte[] compressedData = new byte[compressedLength];
                    c = 0;
                    //read data from all blocks
                    while (data[pos] != 0)
                    {
                        byte length = data[pos++];
                        for (int i = 0; i < length; i++)
                        {
                            compressedData[c++] = data[pos++];
                        }

                    }
                    //decompress it
                    if (localColorTableFlag)
                    {
                        byte[] decompressed = Methods.LZWDecoder.Decode(compressedData, dictionarySize: localColorTableSize, minIndexSize: minimumCodeSize, isGIF: true);
                        for (int i = 0; i < decompressed.Length; i++)
                        {
                            image.imageData[compressPos++] = localColorTable[decompressed[i]];
                        }
                    }
                    else
                    {
                        byte[] decompressed = Methods.LZWDecoder.Decode(compressedData, dictionarySize: sizeGlobalColorTable, minIndexSize: minimumCodeSize, isGIF: true);
                        for (int i = 0; i < decompressed.Length; i++)
                        {
                            image.imageData[compressPos] = colorTable[decompressed[i]];
                            compressPos++;
                        }
                    }
                    gif.images.Add(image);
                    pos++;
                }
                #endregion
            }

            return gif;
        }
    }
}
