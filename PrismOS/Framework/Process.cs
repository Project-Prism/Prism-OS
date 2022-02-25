using System;

namespace PrismOS.Framework
{
    public class Process : IDisposable
    {
        public Process(string FromBase255String)
        {
            ROM = Tests.Base255.FromBase255String(FromBase255String);
            RAM = new byte[30000];
            Output = "";
        }
        public Process(byte[] RawData)
        {
            ROM = RawData;
            RAM = new byte[30000];
            Output = "";
        }

        public enum OPCodes : byte
        {
            Read =      0x00, // Read a single key and store it into memory.
            Write =     0x01, // Write to the output string.
            WriteLine = 0x02, // write to the output string, then add a newline.
            Clear =     0x03, // Clear the output string.
            ROMJump =   0x04, // Jump to a location in the program.
            ROMNxt =    0x05, // Jump to the next location in the ROM
            ROMLst =    0x06, // Jump to the last location in the ROM
            RAMJump =   0x07, // Jump to a location in memory.
            RAMSet =    0x08, // Set the currently selected memory address to a value.
            RAMInc =    0x09, // Increments the currently selected memory address by one.
            RAMDec =    0x10, // Decrements the currently selected memory address by one.
            RAMNxt =    0x11, // Increments the ram pointer by one.
            RAMLst =    0x12, // Decrements the ram pointer by one.
            Random =    0x13, // Sets the currently selected memory address to a random value.
        }
        private readonly Random Random = new();
        private readonly byte[] ROM;
        private readonly byte[] RAM;
        private int ROMPointer;
        private int RAMPointer;
        public string Output;

        public void Cycle()
        {
            if (ROMPointer == ROM.Length) return;

            switch (ROM[ROMPointer++])
            {
                #region Write
                case (byte)OPCodes.Write:
                    if (ROM[ROMPointer] == 0x0) // Select memmory address
                    {
                        ROMPointer++;
                        while (RAMPointer != RAMPointer + ROM[ROMPointer++])
                            Output += (char)RAM[RAMPointer++];
                    }
                    else if (ROM[ROMPointer] == 0x1) // Default to string in rom
                    {
                        ROMPointer++;
                        while (ROMPointer != ROMPointer + ROM[ROMPointer++])
                            Output += (char)ROM[ROMPointer++];
                    }
                    break;
                #endregion

                #region WriteLine
                case (byte)OPCodes.WriteLine:
                    ROMPointer++;
                    if (ROM[ROMPointer] == 0x0) // Select memmory address
                    {
                        ROMPointer++;
                        while (RAMPointer != RAMPointer + RAM[ROMPointer++])
                            Output += (char)RAM[RAMPointer++];
                    }
                    else if(ROM[ROMPointer] == 0x1)
                    {
                        ROMPointer++;
                        while (ROMPointer != ROMPointer + ROM[ROMPointer++])
                            Output += (char)ROM[ROMPointer++];
                    }
                    Output += '\n';
                    break;
                #endregion

                #region Read
                case (byte)OPCodes.Read:
                    if (!Console.KeyAvailable)
                        RAM[RAMPointer] = (byte)Console.ReadKey(true).KeyChar;
                    break;
                #endregion

                #region Clear
                case (byte)OPCodes.Clear:
                    Output = "";
                    break;
                #endregion

                #region ROMJump
                case (byte)OPCodes.ROMJump:
                    ROMPointer++;
                    if (ROM[ROMPointer] == 0x0)
                    {
                        ROMPointer = RAM[RAMPointer];
                    }
                    else if (ROM[ROMPointer] == 0x1)
                    {
                        ROMPointer++;
                        ROMPointer = ROM[ROMPointer];
                    }
                    break;
                #endregion

                #region ROMNxt
                case (byte)OPCodes.ROMNxt:
                    ROMPointer++;
                    break;
                #endregion

                #region ROMLst
                case (byte)OPCodes.ROMLst:
                    ROMPointer--;
                    break;
                #endregion

                #region RAMJump
                case (byte)OPCodes.RAMJump:
                    RAMPointer = ROM[ROMPointer++];
                    break;
                #endregion

                #region RAMSet
                case (byte)OPCodes.RAMSet:
                    RAM[RAMPointer] = ROM[ROMPointer++];
                    break;
                #endregion

                #region RAMInc
                case (byte)OPCodes.RAMInc:
                    RAM[RAMPointer]++;
                    break;
                #endregion

                #region RAMDec
                case (byte)OPCodes.RAMDec:
                    RAM[RAMPointer]--;
                    break;
                #endregion

                #region RAMNxt
                case (byte)OPCodes.RAMNxt:
                    RAMPointer++;
                    break;
                #endregion

                #region RAMLst
                case (byte)OPCodes.RAMLst:
                    RAMPointer--;
                    break;
                #endregion

                #region Random
                case (byte)OPCodes.Random:
                    RAM[RAMPointer] = (byte)Random.Next(0, 255);
                    break;
                    #endregion
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}