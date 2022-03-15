using System;

namespace PrismOS.Libraries.Runtime
{
    public unsafe class Process : IDisposable
    {
        public Process(byte[] RawData)
        {
            ROM = RawData;
            RAM = new byte[30000];
            Out = "";
        }

        public enum Code
        {
            INT   = 0x0,
            CPY   = 0x1,
            ADD   = 0x2,
            SUB   = 0x3,
            DIV   = 0x4,
            MUL   = 0x5,
            CMP   = 0x6,
            JE    = 0x7,
            JNE   = 0x8,
            JG    = 0x9,
            JL    = 0xA,
            JMP   = 0xB,
            ERR   = 0xC,
            MOV   = 0xD,
            RET   = 0xE,
            ADDV  = 0xF,
            SUBV  = 0x10,
            DEVV  = 0x11,
            MULV  = 0x12,
            JPE   = 0x13,
            JPNE  = 0x14,
            JPG   = 0x15,
            JPL   = 0x16,
            JPMP  = 0x17,
            PUSH  = 0x18,
            POP   = 0x19,
            INC   = 0x1A,
            DEC   = 0x1B,
            TIMES = 0x1C,
            GTCLA = 0x1D,
            STREG = 0x1E,
        }
        public string Out, In;
        private int Pos, LPos;
        private readonly byte[] ROM, RAM;

        public void Cycle()
        {
            if (Pos == ROM.Length)
                return;
            byte From, To;
            switch (ROM[Pos++])
            {
                #region Move
                case (byte)Code.MOV:
                    From = ROM[Pos++];
                    To = ROM[Pos++];
                    RAM[To] = ROM[From];
                    break;
                #endregion

                #region Copy
                case (byte)Code.CPY:
                    From = ROM[Pos++];
                    To = ROM[Pos++];
                    RAM[To] = RAM[From];
                    break;
                #endregion

                #region Inc

                case (byte)Code.INC:
                    RAM[ROM[Pos++]]++;
                    break;

                #endregion

                #region Dec

                case (byte)Code.DEC:
                    RAM[(byte)ROM[Pos++]]--;
                    break;

                #endregion

                #region Add

                case (byte)Code.ADD:
                    RAM[ROM[Pos++]] += ROM[Pos++];
                    break;

                #endregion

                #region Sub

                case (byte)Code.SUB:
                    RAM[ROM[Pos++]] -= ROM[Pos++];
                    break;

                #endregion

                #region Mul

                case (byte)Code.MUL:
                    RAM[ROM[Pos]] = (byte)((byte)RAM[ROM[Pos++]] * ROM[Pos++]);
                    break;

                #endregion

                #region Div

                case (byte)Code.DIV:
                    RAM[ROM[Pos]] = (byte)((int)RAM[ROM[Pos++]] / ROM[Pos++]);
                    break;

                #endregion

                #region Jmp

                case (byte)Code.JMP:
                    LPos = Pos;
                    Pos = ROM[Pos];
                    break;

                #endregion

                #region Ret

                case (byte)Code.RET:
                    Pos = LPos;
                    break;

                #endregion

                #region Int

                case (byte)Code.INT:
                    switch (ROM[Pos++])
                    {
                        case 0xD0:
                            Out += (char)ROM[Pos++];
                            break;
                        case 0xD1:
                            RAM[ROM[Pos++]] = (byte)(byte)Console.ReadKey().KeyChar;
                            break;
                    }
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