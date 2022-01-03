using System;
using static System.Text.Encoding;
using static PrismOS.Hexi.Runtime;
using static System.IO.File;
using Pen = Cosmos.System.Graphics.Pen;
using Color = System.Drawing.Color;

namespace PrismOS.Hexi
{
    public class Program
    {
        public Program(byte[] Bytes)
        {
            ByteCode = Bytes;
        }

        public byte[] ByteCode;
        public byte[] Memory;
        public int Index = 0;

        #region Temp
        public int X, Y, ARGB, IND;
        #endregion Temp

        public void Tick()
        {
            switch (ByteCode[Index++])
            {
                #region Console
                case (byte)Codes.Console.Print:
                    Console.Write(UTF8.GetString(ByteCode, ByteCode[Index], ByteCode[Index++]));
                    break;
                #endregion Console_Print

                #region Memory

                #region Set
                case (byte)Codes.Memory.Set:
                    int Count = ByteCode[Index++];
                    int Indx = ByteCode[Index++];
                    for (int i = 0; i < Count; i++)
                    {
                        Memory[Indx++] = ByteCode[Index++];
                    }
                    break;
                #endregion Set

                #region Allocate
                case (byte)Codes.Memory.Allocate:
                    Memory = new byte[ByteCode[Index++]];
                    break;
                #endregion Allocate

                #endregion Memory

                #region Program

                #region Start
                case (byte)Codes.Program.Start:
                    Programs.Add(new(ReadAllBytes(UTF8.GetString(Memory, ByteCode[Index++], ByteCode[Index++]))));
                    break;
                #endregion Start
                #region Stop
                case (byte)Codes.Program.Stop:
                    Programs.Remove(this);
                    break;
                #endregion Stop
                #region Jump
                case (byte)Codes.Program.Jump:
                    Index = ByteCode[Index];
                    break;
                #endregion Jump

                #endregion Program

                #region Graphics

                #region SetPixel
                case (byte)Codes.Graphics.SetPixel:
                    X = ByteCode[Index++];
                    Y = ByteCode[Index++];
                    ARGB = ByteCode[Index++];
                    UI.SaltUI.Canvas.DrawPoint(new Pen(Color.FromArgb(ARGB)), X, Y);
                    break;
                #endregion SetPixel
                #region GetPixel
                case (byte)Codes.Graphics.GetPixel:
                    X = ByteCode[Index++];
                    Y = ByteCode[Index++];
                    IND = ByteCode[Index++];
                    Memory[IND] = (byte)UI.SaltUI.Canvas.GetPointColor(X, Y).ToArgb();
                    break;
                    #endregion GetPixel

                    #endregion Graphics
            }
        }
    }
}
