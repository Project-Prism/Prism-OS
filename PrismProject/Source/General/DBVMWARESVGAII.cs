using Cosmos.Core;
using Cosmos.HAL;
using Cosmos.System.Graphics;
using System;
using System.Drawing;

namespace PrismProject.Source.Graphics
{
    public class DBVMWARESVGAII
    {
        public enum Register : ushort
        {
            ID = 0,
            Enable = 1,
            Width = 2,
            Height = 3,
            MaxWidth = 4,
            MaxHeight = 5,
            Depth = 6,
            BitsPerPixel = 7,
            PseudoColor = 8,
            RedMask = 9,
            GreenMask = 10,
            BlueMask = 11,
            BytesPerLine = 12,
            FrameBufferStart = 13,
            FrameBufferOffset = 14,
            VRamSize = 15,
            FrameBufferSize = 16,
            Capabilities = 17,
            MemStart = 18,
            MemSize = 19,
            ConfigDone = 20,
            Sync = 21,
            Busy = 22,
            GuestID = 23,
            CursorID = 24,
            CursorX = 25,
            CursorY = 26,
            CursorOn = 27,
            HostBitsPerPixel = 28,
            ScratchSize = 29,
            MemRegs = 30,
            NumDisplays = 31,
            PitchLock = 32,
            FifoNumRegisters = 293
        }
        private enum ID : uint
        {
            Magic = 0x900000,
            V0 = Magic << 8,
            V1 = (Magic << 8) | 1,
            V2 = (Magic << 8) | 2,
            Invalid = 0xFFFFFFFF
        }
        public enum FIFO : uint
        {   // values are multiplied by 4 to access the array by byte index
            Min = 0,
            Max = 4,
            NextCmd = 8,
            Stop = 12
        }
        private enum FIFOCommand
        {
            Update = 1,
            RECT_FILL = 2,
            RECT_COPY = 3,
            DEFINE_BITMAP = 4,
            DEFINE_BITMAP_SCANLINE = 5,
            DEFINE_PIXMAP = 6,
            DEFINE_PIXMAP_SCANLINE = 7,
            RECT_BITMAP_FILL = 8,
            RECT_PIXMAP_FILL = 9,
            RECT_BITMAP_COPY = 10,
            RECT_PIXMAP_COPY = 11,
            FREE_OBJECT = 12,
            RECT_ROP_FILL = 13,
            RECT_ROP_COPY = 14,
            RECT_ROP_BITMAP_FILL = 15,
            RECT_ROP_PIXMAP_FILL = 16,
            RECT_ROP_BITMAP_COPY = 17,
            RECT_ROP_PIXMAP_COPY = 18,
            DEFINE_CURSOR = 19,
            DISPLAY_CURSOR = 20,
            MOVE_CURSOR = 21,
            DEFINE_ALPHA_CURSOR = 22
        }
        private enum IOPortOffset : byte
        {
            Index = 0,
            Value = 1,
            Bios = 2,
            IRQ = 3
        }
        [Flags]
        private enum Capability
        {
            /// <summary>
            /// None.
            /// </summary>
            None = 0,
            /// <summary>
            /// Rectangle fill.
            /// </summary>
            RectFill = 1,
            /// <summary>
            /// Rectangle copy.
            /// </summary>
            RectCopy = 2,
            /// <summary>
            /// Rectangle pattern fill.
            /// </summary>
            RectPatFill = 4,
            /// <summary>
            /// Lecacy off screen.
            /// </summary>
            LecacyOffscreen = 8,
            /// <summary>
            /// Raster operation.
            /// </summary>
            RasterOp = 16,
            /// <summary>
            /// Cruser.
            /// </summary>
            Cursor = 32,
            /// <summary>
            /// Cursor bypass.
            /// </summary>
            CursorByPass = 64,
            /// <summary>
            /// Cursor bypass2.
            /// </summary>
            CursorByPass2 = 128,
            /// <summary>
            /// Eigth bit emulation.
            /// </summary>
            EigthBitEmulation = 256,
            /// <summary>
            /// Alpha cursor.
            /// </summary>
            AlphaCursor = 512,
            /// <summary>
            /// Glyph.
            /// </summary>
            Glyph = 1024,
            /// <summary>
            /// Glyph clipping.
            /// </summary>
            GlyphClipping = 0x00000800,
            /// <summary>
            /// Offscreen.
            /// </summary>
            Offscreen1 = 0x00001000,
            /// <summary>
            /// Alpha blend.
            /// </summary>
            AlphaBlend = 0x00002000,
            /// <summary>
            /// Three D.
            /// </summary>
            ThreeD = 0x00004000,
            /// <summary>
            /// Extended FIFO.
            /// </summary>
            ExtendedFifo = 0x00008000,
            /// <summary>
            /// Multi monitors.
            /// </summary>
            MultiMon = 0x00010000,
            /// <summary>
            /// Pitch lock.
            /// </summary>
            PitchLock = 0x00020000,
            /// <summary>
            /// IRQ mask.
            /// </summary>
            IrqMask = 0x00040000,
            /// <summary>
            /// Display topology.
            /// </summary>
            DisplayTopology = 0x00080000,
            /// <summary>
            /// GMR.
            /// </summary>
            Gmr = 0x00100000,
            /// <summary>
            /// Traces.
            /// </summary>
            Traces = 0x00200000,
            /// <summary>
            /// GMR2.
            /// </summary>
            Gmr2 = 0x00400000,
            /// <summary>
            /// Screen objects.
            /// </summary>
            ScreenObject2 = 0x00800000
        }

        private readonly IOPort IndexPort;
        private readonly IOPort ValuePort;
        private readonly IOPort BiosPort;
        private readonly IOPort IRQPort;
        public MemoryBlock Video_Memory;
        private MemoryBlock FIFO_Memory;
        private readonly PCIDevice device;
        public uint height { get; private set; }
        public uint width { get; private set; }
        public uint depth;
        private readonly uint capabilities;
        protected void InitializeFIFO()
        {
            FIFO_Memory = new MemoryBlock(ReadRegister(Register.MemStart), ReadRegister(Register.MemSize));
            FIFO_Memory[(uint)FIFO.Min] = (uint)Register.FifoNumRegisters * sizeof(uint);
            FIFO_Memory[(uint)FIFO.Max] = FIFO_Memory.Size;
            FIFO_Memory[(uint)FIFO.NextCmd] = FIFO_Memory[(uint)FIFO.Min];
            FIFO_Memory[(uint)FIFO.Stop] = FIFO_Memory[(uint)FIFO.Min];
            WriteRegister(Register.ConfigDone, 1);
        }
        public void SetMode(uint width, uint height, uint depth = 32)
        {
            // Depth is color depth in bytes.
            this.depth = (depth / 8);
            this.width = width;
            this.height = height;
            WriteRegister(Register.Width, width);
            WriteRegister(Register.Height, height);
            WriteRegister(Register.BitsPerPixel, depth);
            WriteRegister(Register.Enable, 1);
            InitializeFIFO();

            Init();
        }
        public void WriteRegister(Register register, uint value)
        {
            IndexPort.DWord = (uint)register;
            ValuePort.DWord = value;
        }
        public uint ReadRegister(Register register)
        {
            IndexPort.DWord = (uint)register;
            return ValuePort.DWord;
        }
        protected uint GetFIFO(FIFO cmd)
        {
            return FIFO_Memory[(uint)cmd];
        }
        protected uint SetFIFO(FIFO cmd, uint value)
        {
            return FIFO_Memory[(uint)cmd] = value;
        }
        protected void WaitForFifo()
        {
            WriteRegister(Register.Sync, 1);
            while (ReadRegister(Register.Busy) != 0) { }
        }
        protected void WriteToFifo(uint value)
        {
            if (((GetFIFO(FIFO.NextCmd) == GetFIFO(FIFO.Max) - 4) && GetFIFO(FIFO.Stop) == GetFIFO(FIFO.Min)) ||
                (GetFIFO(FIFO.NextCmd) + 4 == GetFIFO(FIFO.Stop)))
                WaitForFifo();

            SetFIFO((FIFO)GetFIFO(FIFO.NextCmd), value);
            SetFIFO(FIFO.NextCmd, GetFIFO(FIFO.NextCmd) + 4);

            if (GetFIFO(FIFO.NextCmd) == GetFIFO(FIFO.Max))
                SetFIFO(FIFO.NextCmd, GetFIFO(FIFO.Min));
        }
        public void Update(uint x, uint y, uint width, uint height)
        {
            WriteToFifo((uint)FIFOCommand.Update);
            WriteToFifo(x);
            WriteToFifo(y);
            WriteToFifo(width);
            WriteToFifo(height);
            WaitForFifo();
        }

        public uint GetPixel(uint x, uint y)
        {
            return Video_Memory[((y * width + x) * depth)];
        }
        public void Disable()
        {
            WriteRegister(Register.Enable, 0);
        }
        public DBVMWARESVGAII()
        {
            device = (PCI.GetDevice(VendorID.VMWare, DeviceID.SVGAIIAdapter));
            device.EnableMemory(true);
            uint basePort = device.BaseAddressBar[0].BaseAddress;
            IndexPort = new IOPort((ushort)(basePort + (uint)IOPortOffset.Index));
            ValuePort = new IOPort((ushort)(basePort + (uint)IOPortOffset.Value));
            BiosPort = new IOPort((ushort)(basePort + (uint)IOPortOffset.Bios));
            IRQPort = new IOPort((ushort)(basePort + (uint)IOPortOffset.IRQ));

            WriteRegister(Register.ID, (uint)ID.V2);
            if (ReadRegister(Register.ID) != (uint)ID.V2)
                return;

            Video_Memory = new MemoryBlock(ReadRegister(Register.FrameBufferStart), ReadRegister(Register.VRamSize));

            capabilities = ReadRegister(Register.Capabilities);
            InitializeFIFO();
        }

        public uint FrameSize;
        public uint FrameOffset;
        private void Init()
        {
            FrameSize = ReadRegister(Register.FrameBufferSize);
            FrameOffset = ReadRegister(Register.FrameBufferOffset);
        }

        public void SetPixel(int x, int y, Color color)
        {
            if (x < width)
            {
                Video_Memory[(((uint)y * width + (uint)x) * depth) + FrameSize] = (uint)color.ToArgb();
            }
        }
        public void SetVRAM(int[] colors, int Offset)
        {
            Video_Memory.Copy(Offset, colors, 0, colors.Length);
        }
        public void Clear(Color color)
        {
            Video_Memory.Fill(FrameSize, FrameSize, (uint)color.ToArgb());
        }
        public void Update()
        {
            try
            {
                Video_Memory.MoveDown(FrameOffset, FrameSize, FrameSize);
            }
            catch (Exception)
            {
            }
            Update(0, 0, width, height);
        }

        public void DrawRectangle(Color color, int x, int y, int width, int height)
        {
            /* The check of the validity of x and y are done in DrawLine() */

            /* The vertex A is where x,y are */
            int xa = x;
            int ya = y;

            /* The vertex B has the same y coordinate of A but x is moved of width pixels */
            int xb = x + width;
            int yb = y;

            /* The vertex C has the same x coordiate of A but this time is y that is moved of height pixels */
            int xc = x;
            int yc = y + height;

            /* The Vertex D has x moved of width pixels and y moved of height pixels */
            int xd = x + width;
            int yd = y + height;

            /* Draw a line betwen A and B */
            DrawLine(color, xa, ya, xb, yb);

            /* Draw a line between A and C */
            DrawLine(color, xa, ya, xc, yc);

            /* Draw a line between B and D */
            DrawLine(color, xb, yb, xd, yd);

            /* Draw a line between C and D */
            DrawLine(color, xc, yc, xd, yd);
        }
        public void DrawFillRectangle(int x, int y, int width, int height, Color color)
        {
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    SetPixel(w + x, y + h, color);
                }
            }
        }
        public void DrawImage(Bitmap image, int x, int y)
        {
            for (int _x = 0; _x < image.Width; _x++)
            {
                for (int _y = 0; _y < image.Height; _y++)
                {
                    SetPixel(x + _x, y + _y, Color.FromArgb(image.rawData[_x + _y * image.Width]));
                }
            }
        }
        public void DrawLine(Color color, int x1, int y1, int x2, int y2)
        {
            // trim the given line to fit inside the canvas boundries
            TrimLine(ref x1, ref y1, ref x2, ref y2);

            int dx, dy;

            dx = x2 - x1;      /* the horizontal distance of the line */
            dy = y2 - y1;      /* the vertical distance of the line */

            if (dy == 0) /* The line is horizontal */
            {
                DrawHorizontalLine(color, dx, x1, y1);
                return;
            }

            if (dx == 0) /* the line is vertical */
            {
                DrawVerticalLine(color, dy, x1, y1);
                return;
            }

            /* the line is neither horizontal neither vertical, is diagonal then! */
            DrawDiagonalLine(color, dx, dy, x1, y1);
        }
        public void DrawCircle(Color color, int x0, int y0, int radius)
        {
            int x = radius;
            int y = 0;
            int xChange = 1 - (radius << 1);
            int yChange = 0;
            int radiusError = 0;

            while (x >= y)
            {
                for (int i = x0 - x; i <= x0 + x; i++)
                {

                    SetPixel(i, y0 + y, color);
                    SetPixel(i, y0 - y, color);
                }
                for (int i = x0 - y; i <= x0 + y; i++)
                {
                    SetPixel(i, y0 + x, color);
                    SetPixel(i, y0 - x, color);
                }

                y++;
                radiusError += yChange;
                yChange += 2;
                if (((radiusError << 1) + xChange) > 0)
                {
                    x--;
                    radiusError += xChange;
                    xChange += 2;
                }
            }
        }
        public void DrawFilledCircle(Color color, int x0, int y0, int radius)
        {
            int x = radius;
            int y = 0;
            int xChange = 1 - (radius << 1);
            int yChange = 0;
            int radiusError = 0;

            while (x >= y)
            {
                for (int i = x0 - x; i <= x0 + x; i++)
                {

                    SetPixel(i, y0 + y, color);
                    SetPixel(i, y0 - y, color);
                }
                for (int i = x0 - y; i <= x0 + y; i++)
                {
                    SetPixel(i, y0 + x, color);
                    SetPixel(i, y0 - x, color);
                }

                y++;
                radiusError += yChange;
                yChange += 2;
                if (((radiusError << 1) + xChange) > 0)
                {
                    x--;
                    radiusError += xChange;
                    xChange += 2;
                }
            }
        }

        private void DrawVerticalLine(Color color, int dy, int x1, int y1)
        {
            int i;

            for (i = 0; i < dy; i++)
            {
                SetPixel(x1, (y1 + i), color);
            }
        }
        private void DrawHorizontalLine(Color color, int dx, int x1, int y1)
        {
            for (int i = 0; i < dx; i++)
            {
                SetPixel(x1 + i, y1, color);
            }
        }
        protected void TrimLine(ref int x1, ref int y1, ref int x2, ref int y2)
        {
            if (x1 == x2)
            {
                x1 = (int)Math.Min(width - 1, Math.Max(0, x1));
                x2 = x1;
                y1 = (int)Math.Min(height - 1, Math.Max(0, y1));
                y2 = (int)Math.Min(height - 1, Math.Max(0, y2));

                return;
            }

            // never attempt to remove this part,
            // if we didn't calculate our new values as floats, we would end up with inaccurate output
            float x1_out = x1, y1_out = y1;
            float x2_out = x2, y2_out = y2;

            // calculate the line slope, and the entercepted part of the y axis
            float m = (y2_out - y1_out) / (x2_out - x1_out);
            float c = y1_out - m * x1_out;

            // handle x1
            if (x1_out < 0)
            {
                x1_out = 0;
                y1_out = c;
            }
            else if (x1_out >= width)
            {
                x1_out = width - 1;
                y1_out = (width - 1) * m + c;
            }

            // handle x2
            if (x2_out < 0)
            {
                x2_out = 0;
                y2_out = c;
            }
            else if (x2_out >= width)
            {
                x2_out = width - 1;
                y2_out = (width - 1) * m + c;
            }

            // handle y1
            if (y1_out < 0)
            {
                x1_out = -c / m;
                y1_out = 0;
            }
            else if (y1_out >= height)
            {
                x1_out = (height - 1 - c) / m;
                y1_out = height - 1;
            }

            // handle y2
            if (y2_out < 0)
            {
                x2_out = -c / m;
                y2_out = 0;
            }
            else if (y2_out >= height)
            {
                x2_out = (height - 1 - c) / m;
                y2_out = height - 1;
            }

            // final check, to avoid lines that are totally outside bounds
            if (x1_out < 0 || x1_out >= width || y1_out < 0 || y1_out >= height)
            {
                x1_out = 0; x2_out = 0;
                y1_out = 0; y2_out = 0;
            }

            if (x2_out < 0 || x2_out >= width || y2_out < 0 || y2_out >= height)
            {
                x1_out = 0; x2_out = 0;
                y1_out = 0; y2_out = 0;
            }

            // replace inputs with new values
            x1 = (int)x1_out; y1 = (int)y1_out;
            x2 = (int)x2_out; y2 = (int)y2_out;
        }
        private void DrawDiagonalLine(Color color, int dx, int dy, int x1, int y1)
        {
            int i, sdx, sdy, dxabs, dyabs, x, y, px, py;

            dxabs = Math.Abs(dx);
            dyabs = Math.Abs(dy);
            sdx = Math.Sign(dx);
            sdy = Math.Sign(dy);
            x = dyabs >> 1;
            y = dxabs >> 1;
            px = x1;
            py = y1;

            if (dxabs >= dyabs) /* the line is more horizontal than vertical */
            {
                for (i = 0; i < dxabs; i++)
                {
                    y += dyabs;
                    if (y >= dxabs)
                    {
                        y -= dxabs;
                        py += sdy;
                    }
                    px += sdx;
                    SetPixel(px, py, color);
                }
            }
            else /* the line is more vertical than horizontal */
            {
                for (i = 0; i < dyabs; i++)
                {
                    x += dxabs;
                    if (x >= dyabs)
                    {
                        x -= dyabs;
                        px += sdx;
                    }
                    py += sdy;
                    SetPixel(px, py, color);
                }
            }
        }
    }
}