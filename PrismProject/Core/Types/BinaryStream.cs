using System;
using System.IO;

namespace PrismProject.Core.Types
{
    /// <summary>
    /// This class allows reading from a byte array single bits
    /// </summary>
    public class BinaryStream : Stream
    {
        private byte[] data;
        private long bytePos;
        private byte bitPos;
        private int length = -1;
        /// <summary>
        /// If bytes are parsed from Right -> Left (MSB standard) or Left -> Right (LSB)
        /// </summary>
        public bool LSB { get; set; } = false;

        public BinaryStream(byte[] Data, int length = -1)
        {
            data = Data;
            bytePos = 0;
            bitPos = 0;
            this.length = length;
            if (length == -1)
            {
                this.length = data.Length;
            }
        }

        public override bool CanRead => length != bytePos;

        public override bool CanSeek => true;

        public override bool CanWrite => false;

        /// <summary>
        /// Length in bits
        /// </summary>
        public override long Length => length * 8;

        public override long Position
        {
            get => bytePos * 8 + bitPos;
            set
            {
                bitPos = (byte)(value % 8);
                bytePos = value / 8;
            }
        }

        public override void Flush()
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Will read count number of bits. The first byte will be filled from LSB first. So the first byte might not be fully filled but all
        /// later ones will be
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count">Number of bits to read</param>
        /// <returns>Number of bytes read rounded up</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (!CanRead)
            {
                return 0;
            }
            var bytesToRead = (int)Math.Min(buffer.Length - offset, length - bytePos);
            var bitsToRead = Math.Min(Math.Min(bytesToRead * 8, Length - bytePos * 8 - bitPos), count);
            if (!LSB)
            {
            buffer[offset] = 0;
            for (int i = 0; i < bitsToRead % 8; i++)
            {
                buffer[offset] <<= 1;
                buffer[offset] |= ReadBit();
            }
            if (bitsToRead % 8 != 0)
            {
                offset++;
            }
            }
            for (int i = 0; i < bitsToRead / 8; i++)
            {
                buffer[offset++] = ReadByte();
            }
            if (LSB)
            {
                if(bitsToRead % 8 != 0)
                {
                    buffer[offset] = 0;
                }
                for (int i = 0; i < bitsToRead % 8; i++)
                {
                    buffer[offset] |= (byte)(ReadBit() << i);
                }
            }


            return bytesToRead;
        }

        /// <summary>
        /// Read one byte from the stream
        /// </summary>
        /// <returns></returns>
        private byte ReadByte()
        {
            if (bitPos == 0)
            {
                return data[bytePos++];
            }
            else
            {
                byte val = 0;
                if (!LSB)
                {
                for (int i = 0; i < 8; i++)
                {
                    val <<= 1;
                    val |= ReadBit();
                }
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        val |= (byte)(ReadBit() << i);
                    }
                }
                return val;
            }
        }

        /// <summary>
        /// Read one bit from the stream
        /// </summary>
        /// <returns></returns>
        private byte ReadBit()
        {
            if (!CanRead)
            {
                throw new IOException();
            }
            var readByte = data[bytePos];
            byte bit;
            if (LSB)
            {
                bit = (byte)((readByte >> bitPos) & 1);
            }
            else
            {
                bit = (byte)((readByte & (1 << (7 - bitPos))) >> (7 - bitPos));
            }
            bitPos++;
            if (bitPos == 8)
            {
                bitPos = 0;
                bytePos++;
            }
            return bit;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    bytePos = offset / 8;
                    bitPos = (byte)(offset % 8);
                    break;
                case SeekOrigin.Current:
                    bytePos += offset / 8;
                    bitPos += (byte)(offset % 8);
                    break;
                case SeekOrigin.End:
                    bytePos = Length - offset / 8;
                    bitPos = (byte)(Length - offset % 8);
                    break;
                default:
                    break;
            }
            return Position;
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
    }
}
