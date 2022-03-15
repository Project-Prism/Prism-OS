/*          TCS EXECUTABLE FORMAT
 *  2022, LUFTKATZE
 *  Worst EXE format ever
 *  Have fun :)
 */

using System;
using System.IO;
using System.Text;

namespace TCS
{
    static public class FILE
    {
        public static void SaveFile(double[] save, string path, FileMode flmode = FileMode.Create)
        {
            using var stream = File.Open(path, flmode);
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
            {
                for (int i = 0; i < save.Length; i++)
                {
                    writer.Write(save[i]);
                }
            }
            stream.Close();
        }

        public static double[] ReadFile(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }
            int flsiz = File.ReadAllBytes(path).Length / 8;
            using var stream = File.Open(path, FileMode.Open);
            double[] ret = new double[stream.Length];
            using (var reader = new BinaryReader(stream, Encoding.UTF32))
            {
                for (int i = 0; i < flsiz; i++)
                {
                    ret[i] = reader.ReadDouble();
                }
            }
            return ret;

        }
    }

    public struct Code
    {
        public const int INT = 0x0;
        public const int CPY = 0x1;
        public const int ADD = 0x2;
        public const int SUB = 0x3;
        public const int DEV = 0x4;
        public const int MUL = 0x5;
        public const int CMP = 0x6;
        public const int JE = 0x7;
        public const int JNE = 0x8;
        public const int JG = 0x9;
        public const int JL = 0xA;
        public const int JMP = 0xB;
        public const int ERR = 0xC;
        public const int MOV = 0xD;
        public const int RET = 0xE;
        public const int ADDV = 0xF;
        public const int SUBV = 0x10;
        public const int DEVV = 0x11;
        public const int MULV = 0x12;
        public const int JPE = 0x13;
        public const int JPNE = 0x14;
        public const int JPG = 0x15;
        public const int JPL = 0x16;
        public const int JPMP = 0x17;
        public const int PUSH = 0x18;
        public const int POP = 0x19;
        public const int INC = 0x1A;
        public const int DEC = 0x1B;
        public const int TIMES = 0x1C;
        public const int GTCLA = 0x1D;
        public const int STREG = 0x1E;
    }
    public struct Registers
    {
        public const int ax = 524280;
        public const int al = 524281;
        public const int ah = 524282;
        public const int ab = 524283;
        public const int ap = 524284;
        public const int st = 524289;
        public const int ret = 524290;
    }
    public struct CMP_Flags
    {
        public const int EQU = 1;
        public const int GRT = 2;
        public const int LOW = 3;
    }

    static public class EXE
    {
        static public double[] mem;
        static public string[] args;
        static public double VERSION = 0.3;
        private static readonly char[] str = new char[358];
        static bool VIS(int VC)
        {
            string streg = new(str);
            switch (VC)
            {
                case 0x0:       // End Of Code
                    return true;
                case 0x13:      // STD TCS FILES I/O
                    switch (mem[Registers.ax])
                    {
                        case 0x0:
                            break;
                        case 0x1:
                            Console.WriteLine(streg);
                            break;
                        case 0x2:
                            File.Create(streg);
                            break;
                        case 0x3:
                            try
                            {
                                File.Delete(streg);
                            }
                            catch (DirectoryNotFoundException)
                            {
                                mem[Registers.st] = 0;
                            }
                            break;
                        case 0x4:
                            if (File.Exists(streg))
                            {
                                mem[Registers.st] = 1;
                            }
                            else
                            {
                                mem[Registers.st] = 0;
                            }
                            break;
                        case 0x5:
                            mem[Registers.al] = FILE.ReadFile(streg)[Registers.ap];
                            break;
                        case 0x6:
                            double[] wb = new double[1];
                            wb[0] = mem[Registers.al];
                            try
                            {
                                FILE.SaveFile(wb, streg, FileMode.Append);
                                mem[Registers.st] = 1;
                            }
                            catch
                            {
                                mem[Registers.st] = 0;
                            }
                            break;
                        case 0x7:
                            double[] rb = new double[1];
                            rb[0] = mem[Registers.al];
                            try
                            {
                                FILE.SaveFile(rb, streg);
                                mem[Registers.st] = 1;
                            }
                            catch
                            {
                                mem[Registers.st] = 0;
                            }
                            break;
                        case 0x9:
                            if (Directory.Exists(streg))
                            {
                                mem[Registers.st] = 1;
                            }
                            else
                            {
                                mem[Registers.st] = 0;
                            }
                            break;
                        case 0xA:
                            try
                            {
                                Directory.CreateDirectory(streg);
                                mem[Registers.st] = 1;
                            }
                            catch
                            {
                                mem[Registers.st] = 0;
                            }
                            break;
                        case 0xB:
                            try
                            {
                                Directory.Delete(streg);
                                mem[Registers.st] = 1;
                            }
                            catch
                            {
                                mem[Registers.st] = 0;
                            }
                            break;
                        case 0xC:
                            try
                            {
                                Directory.GetCreationTime(streg);
                                mem[Registers.st] = 1;
                            }
                            catch
                            {
                                mem[Registers.st] = 0;
                            }
                            break;
                        case 0xD:
                            try
                            {
                                File.GetCreationTime(streg);
                                mem[Registers.st] = 1;
                            }
                            catch
                            {
                                mem[Registers.st] = 0;
                            }
                            break;
                        default:
                            Console.WriteLine("INVALID AX");
                            return false;
                    }
                    break;
                case 0x21:      // TCS & PrismOS API
                    switch (mem[Registers.ax])
                    {
                        case 0x0:
                            Console.Beep();
                            break;
                        case 0x1:
                            Console.Write((char)mem[Registers.al]);
                            break;
                        case 0x2:
                            mem[Registers.al] = Console.ReadKey().KeyChar;
                            break;
                        case 0x3:
                            Console.BackgroundColor = (ConsoleColor)mem[Registers.al];
                            break;
                        case 0x4:
                            Console.ForegroundColor = (ConsoleColor)mem[Registers.al];
                            break;
                        case 0x5:
                            mem[Registers.al] = (int)Console.BackgroundColor;
                            break;
                        case 0x6:
                            mem[Registers.al] = (int)Console.ForegroundColor;
                            break;
                        case 0x7:
                            mem[Registers.al] = Console.CursorLeft;
                            break;
                        case 0x8:
                            mem[Registers.al] = Console.CursorTop;
                            break;
                        case 0x9:
                            Console.CursorLeft = (int)mem[Registers.al];
                            break;
                        case 0xA:
                            Console.CursorTop = (int)mem[Registers.al];
                            break;
                        case 0xB:
                            Console.Clear();
                            break;
                        case 0xC:
                            Console.ResetColor();
                            break;
                        case 0xD:
                            mem[Registers.al] = VERSION;
                            break;
                        case 0xE:
                            mem[Registers.al] = new DateTime().Minute;
                            break;
                        case 0xF:
                            mem[Registers.al] = new DateTime().Second;
                            break;
                        case 0x10:
                            mem[Registers.al] = new DateTime().Hour;
                            break;
                        case 0x11:
                            mem[Registers.al] = new DateTime().Day;
                            break;
                        case 0x12:
                            mem[Registers.al] = new DateTime().Month;
                            break;
                        case 0x13:
                            mem[Registers.al] = new DateTime().Year;
                            break;
                        case 0xFF:
                            Console.Write(streg);
                            break;
                        default:
                            Console.WriteLine("INVALID AX");
                            return false;
                    }
                    break;
                case 0xFF:
                    switch (mem[Registers.ax])
                    {
                        case 0x1:
                            foreach (string s in Directory.GetDirectories(streg))
                            {
                                Console.WriteLine(s);
                            }
                            break;
                        case 0x2:
                            foreach (string s in Directory.GetFiles(streg))
                            {
                                Console.WriteLine(s);
                            }
                            break;
                        case 0x3:
                            mem[Registers.al] = streg[Registers.al];
                            break;
                        default:
                            Console.WriteLine("INVALID AX");
                            return false;
                    }
                    break;
                default:        // Unknow INT
                    Console.WriteLine("UNKNOW INT");
                    return false;
            }
            return false;
        }

        static public double[] Set(double[] set)
        {
            double[] tr = new double[Code.RET];
            Array.Clear(tr, 0, tr.Length);
            for (int i = 0; i < set.Length; i++)
            {
                tr[i] = set[i];
            }
            return tr;
        }

        private static bool ipp = true;
        private static readonly double[] stack = new double[550];
        private static int cmp = 0;
        private static readonly int[] fret = new int[40000];
        private static int retp = 0;
        static public double TCS()
        {
            if (!(mem[0] == 'T' && mem[1] == 'C' && mem[2] == 'S'))
            {
                return 55255;
            }
            for (int ii = 0; ii < 3; ii++)
            {
                for (int i = 0; i < mem.Length - 1; i++)
                {
                    mem[i] = mem[i + 1];
                }
            }
            for (int i = 0; ;)
            {
                switch (mem[i])
                {
                    case Code.ERR:
                        return mem[Code.RET];
                    case Code.TIMES:
                        int b = i;
                        for (int times = 0; times < mem[Registers.ax]; times++)
                        {
                            MPP(ref i);
                            i = b;
                        }
                        break;
                    default:
                        if (MPP(ref i))
                        {
                            return mem[Code.RET];
                        }
                        break;
                }
                if (ipp) { i++; }
                ipp = true;
            }
        }

        private static bool MPP(ref int i)
        {
            switch (mem[i])
            {
                case Code.INT:           // Interrupt
                    if (VIS((int)mem[i + 1]))
                    {
                        return true;
                    }
                    i++;
                    break;
                case Code.CPY:
                    mem[(int)mem[i + 1]] = mem[(int)mem[i + 2]];
                    i += 2;
                    break;
                case Code.ADD:
                    mem[Registers.ah] = (mem[(int)mem[i + 1]] + mem[(int)mem[i + 2]]);
                    i += 2;
                    break;
                case Code.SUB:
                    mem[Registers.ah] = (mem[(int)mem[i + 1]] - mem[(int)mem[i + 2]]);
                    i += 2;
                    break;
                case Code.DEV:
                    mem[Registers.ah] = (mem[(int)mem[i + 1]] / mem[(int)mem[i + 2]]);
                    i += 2;
                    break;
                case Code.MUL:
                    mem[Registers.ah] = (mem[(int)mem[i + 1]] * mem[(int)mem[i + 2]]);
                    i += 2;
                    break;
                case Code.CMP:
                    if (mem[(int)mem[i + 1]] == mem[(int)mem[i + 2]])
                    {
                        cmp = CMP_Flags.EQU;
                    }
                    else if (mem[(int)mem[i + 1]] > mem[(int)mem[i + 2]])
                    {
                        cmp = CMP_Flags.GRT;
                    }
                    else if (mem[(int)mem[i + 1]] < mem[(int)mem[i + 2]])
                    {
                        cmp = CMP_Flags.LOW;
                    }
                    i += 2;
                    break;
                case Code.JE:
                    if (cmp == CMP_Flags.EQU)
                    {
                        i = (int)mem[i + 1];
                        ipp = false;
                    }
                    else
                    {
                        i++;
                    }
                    break;
                case Code.JNE:
                    if (cmp != CMP_Flags.EQU)
                    {
                        i = (int)mem[i + 1];
                        ipp = false;
                    }
                    else
                    {
                        i++;
                    }
                    break;
                case Code.JG:
                    if (cmp == CMP_Flags.GRT)
                    {
                        i = (int)mem[i + 1];
                        ipp = false;
                    }
                    else
                    {
                        i++;
                    }
                    break;
                case Code.JL:
                    if (cmp == CMP_Flags.LOW)
                    {
                        i = (int)mem[i + 1];
                        ipp = false;
                    }
                    else
                    {
                        i++;
                    }
                    break;
                case Code.JMP:
                    i = (int)mem[i + 1];
                    ipp = false;
                    fret[retp] = i;
                    retp++;
                    break;
                case Code.MOV:
                    mem[(int)mem[i + 1]] = mem[i + 2];
                    i += 2;
                    break;
                case Code.RET:
                    i = fret[retp--];
                    ipp = false;
                    break;
                case Code.ADDV:
                    mem[Registers.ah] = (mem[i + 1] + mem[(int)mem[i + 2]]);
                    i += 2;
                    break;
                case Code.SUBV:
                    mem[Registers.ah] = (mem[i + 1] - mem[(int)mem[i + 2]]);
                    i += 2;
                    break;
                case Code.DEVV:
                    mem[Registers.ah] = (mem[i + 1] / mem[(int)mem[i + 2]]);
                    i += 2;
                    break;
                case Code.MULV:
                    mem[Registers.ah] = (mem[i + 1] * mem[(int)mem[i + 2]]);
                    i += 2;
                    break;
                case Code.JPE:
                    if (cmp == CMP_Flags.EQU)
                    {
                        i = (int)mem[(int)mem[i + 1]];
                        ipp = false;
                    }
                    else
                    {
                        i++;
                    }
                    break;
                case Code.JPNE:
                    if (cmp != CMP_Flags.EQU)
                    {
                        i = (int)mem[(int)mem[i + 1]];
                        ipp = false;
                    }
                    else
                    {
                        i++;
                    }
                    break;
                case Code.JPG:
                    if (cmp == CMP_Flags.GRT)
                    {
                        i = (int)mem[(int)mem[i + 1]];
                        ipp = false;
                    }
                    else
                    {
                        i++;
                    }
                    break;
                case Code.JPL:
                    if (cmp == CMP_Flags.LOW)
                    {
                        i = (int)mem[(int)mem[i + 1]];
                        ipp = false;
                    }
                    else
                    {
                        i++;
                    }
                    break;
                case Code.JPMP:
                    i = (int)mem[(int)mem[i + 1]];
                    ipp = false;
                    fret[retp] = i;
                    retp++;
                    break;
                case Code.PUSH:
                    stack[(int)mem[Registers.ap]] = mem[(int)mem[i + 1]];
                    mem[Registers.ap]++;
                    i++;
                    break;
                case Code.POP:
                    mem[(int)mem[i + 1]] = stack[(int)mem[Registers.ap]--];
                    i++;
                    break;
                case Code.INC:
                    mem[(int)mem[i + 1]]++;
                    i++;
                    break;
                case Code.DEC:
                    mem[(int)mem[i + 1]]--;
                    i++;
                    break;
                case Code.GTCLA:
                    mem[Registers.al] = args[i + 1][i + 2];
                    i += 2;
                    break;
                case Code.STREG:
                    str[i + 1] = (char)mem[(int)mem[i + 2]];
                    break;
                default:
                    Console.WriteLine($"ERROR! {i}");
                    return true;
            }
            return true;
        }
    }
}