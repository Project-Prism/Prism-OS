using System;

namespace PrismOS.Core
{
    public unsafe class Process : IDisposable
    {
        public Process(byte[] RawData)
        {
            ROM = RawData;
            RAM = new byte*[30000];
            Out = "";
        }

        public enum Code : byte
        {
            Move, Copy, Clear,
            Inc, Dec, Add, Sub, Mul, Div,
            Jmp, Ret, Int,
        }
        public string Out, In;
        private int Pos, LPos;
        private readonly byte[] ROM;
        private readonly byte*[] RAM;

        public void Cycle()
        {
            if (Pos == ROM.Length)
                return;
            byte From, To;
            switch (ROM[Pos++])
            {
                #region Move
                case (byte)Code.Move:
                    From = ROM[Pos++];
                    To = ROM[Pos++];
                    RAM[To] = (byte*)ROM[From];
                    break;
                #endregion

                #region Copy
                case (byte)Code.Copy:
                    From = ROM[Pos++];
                    To = ROM[Pos++];
                    RAM[To] = RAM[From];
                    break;
                #endregion

                #region Clear

                case (byte)Code.Clear:
                    RAM[(byte)ROM[Pos++]] = (byte*)0x0;
                    break;

                #endregion

                #region Inc

                case (byte)Code.Inc:
                    RAM[ROM[Pos++]]++;
                    break;

                #endregion

                #region Dec

                case (byte)Code.Dec:
                    RAM[(byte)ROM[Pos++]]--;
                    break;

                #endregion

                #region Add

                case (byte)Code.Add:
                    RAM[(byte)ROM[Pos++]] += (byte)ROM[Pos++];
                    break;

                #endregion

                #region Sub

                case (byte)Code.Sub:
                    RAM[(byte)ROM[Pos++]] -= (byte)ROM[Pos++];
                    break;

                #endregion

                #region Mul

                case (byte)Code.Mul:
                    RAM[ROM[Pos]] = (byte*)((int)RAM[ROM[Pos++]] * ROM[Pos++]);
                    break;

                #endregion

                #region Div

                case (byte)Code.Div:
                    RAM[ROM[Pos]] = (byte*)((int)RAM[ROM[Pos++]] / ROM[Pos++]);
                    break;

                #endregion

                #region Jmp

                case (byte)Code.Jmp:
                    LPos = Pos;
                    Pos = ROM[Pos];
                    break;

                #endregion

                #region Ret

                case (byte)Code.Ret:
                    Pos = LPos;
                    break;

                #endregion

                #region Int

                case (byte)Code.Int:
                    switch (ROM[Pos++])
                    {
                        case 0xD0:
                            Out += (char)ROM[Pos++];
                            break;
                        case 0xD1:
                            RAM[ROM[Pos++]] = (byte*)(byte)Console.ReadKey().KeyChar;
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