using System;
using System.Collections.Generic;
using System.Drawing;
using Cosmos.Core;
using Cosmos.Debug.Kernel;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;

namespace PrismProject.Source.Graphics.Drivers.VMWare
{
    public class VMWareSVGAII
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
        {
            Min = 0,
            Max = 4,
            NextCmd = 8,
            Stop = 12
        }
        private enum FIFOCommand
        {
            /// <summary>
            /// Update.
            /// </summary>
            Update = 1,
            /// <summary>
            /// Rectange fill.
            /// </summary>
            RECT_FILL = 2,
            /// <summary>
            /// Rectange copy.
            /// </summary>
            RECT_COPY = 3,
            /// <summary>
            /// Define bitmap.
            /// </summary>
            DEFINE_BITMAP = 4,
            /// <summary>
            /// Define bitmap scanline.
            /// </summary>
            DEFINE_BITMAP_SCANLINE = 5,
            /// <summary>
            /// Define pixmap.
            /// </summary>
            DEFINE_PIXMAP = 6,
            /// <summary>
            /// Define pixmap scanline.
            /// </summary>
            DEFINE_PIXMAP_SCANLINE = 7,
            /// <summary>
            /// Rectange bitmap fill.
            /// </summary>
            RECT_BITMAP_FILL = 8,
            /// <summary>
            /// Rectange pixmap fill.
            /// </summary>
            RECT_PIXMAP_FILL = 9,
            /// <summary>
            /// Rectange bitmap copy.
            /// </summary>
            RECT_BITMAP_COPY = 10,
            /// <summary>
            /// Rectange pixmap fill.
            /// </summary>
            RECT_PIXMAP_COPY = 11,
            /// <summary>
            /// Free object.
            /// </summary>
            FREE_OBJECT = 12,
            /// <summary>
            /// Rectangle raster operation fill.
            /// </summary>
            RECT_ROP_FILL = 13,
            /// <summary>
            /// Rectangle raster operation copy.
            /// </summary>
            RECT_ROP_COPY = 14,
            /// <summary>
            /// Rectangle raster operation bitmap fill.
            /// </summary>
            RECT_ROP_BITMAP_FILL = 15,
            /// <summary>
            /// Rectangle raster operation pixmap fill.
            /// </summary>
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
            None = 0,
            RectFill = 1,
            RectCopy = 2,
            RectPatFill = 4,
            LecacyOffscreen = 8,
            RasterOp = 16,
            Cursor = 32,
            CursorByPass = 64,
            CursorByPass2 = 128,
            EigthBitEmulation = 256,
            AlphaCursor = 512,
            Glyph = 1024,
            GlyphClipping = 0x00000800,
            Offscreen1 = 0x00001000,
            AlphaBlend = 0x00002000,
            ThreeD = 0x00004000,
            ExtendedFifo = 0x00008000,
            MultiMon = 0x00010000,
            PitchLock = 0x00020000,
            IrqMask = 0x00040000,
            DisplayTopology = 0x00080000,
            Gmr = 0x00100000,
            Traces = 0x00200000,
            Gmr2 = 0x00400000,
            ScreenObject2 = 0x00800000
        }
        private readonly IOPort IndexPort;
        private readonly IOPort ValuePort;
        private readonly IOPort BiosPort;
        private readonly IOPort IRQPort;
        internal MemoryBlock Video_Memory;
        private MemoryBlock FIFO_Memory;
        private readonly Cosmos.HAL.PCIDevice device;
        private uint height;
        private uint width;
        private uint depth;
        private readonly uint capabilities;
        private readonly uint FIFO_CAP;
        readonly Debugger debugger;
        public VMWareSVGAII()
        {
            device = (Cosmos.HAL.PCI.GetDevice(Cosmos.HAL.VendorID.VMWare, Cosmos.HAL.DeviceID.SVGAIIAdapter));
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
            debugger = new Debugger("", "");
            debugger.Send(ReadRegister(Register.FrameBufferStart) + ":" + ReadRegister(Register.VRamSize));
            capabilities = ReadRegister(Register.Capabilities);
            InitializeFIFO();
            //This calculates a approximate offset of the FIFO Cap limit used in WaitForFifo 
            FIFO_CAP = (GetFIFO(FIFO.Max) - GetFIFO(FIFO.Min)) / 4;
        }
        protected void InitializeFIFO()
        {
            FIFO_Memory = new MemoryBlock(ReadRegister(Register.MemStart), ReadRegister(Register.MemSize));
            FIFO_Memory[(uint)FIFO.Min] = (uint)Register.FifoNumRegisters * sizeof(uint);
            FIFO_Memory[(uint)FIFO.Max] = FIFO_Memory.Size;
            FIFO_Memory[(uint)FIFO.NextCmd] = FIFO_Memory[(uint)FIFO.Min];
            FIFO_Memory[(uint)FIFO.Stop] = FIFO_Memory[(uint)FIFO.Min];
            WriteRegister(Register.ConfigDone, 1);
        }
        public void SetMode(int width, int height, uint depth = 32)
        {
            //Disable the Driver before writing new values and initiating it again to avoid a memory exception
            Disable();
            // Depth is color depth in bytes.
            this.depth = (depth / 8);
            this.width = (uint)width;
            this.height = (uint)height;
            WriteRegister(Register.Width, (uint)width);
            WriteRegister(Register.Height, (uint)height);
            WriteRegister(Register.BitsPerPixel, depth);
            Enable();
            InitializeFIFO();
        }
        protected void WriteRegister(Register register, uint value)
        {
            IndexPort.DWord = (uint)register;
            ValuePort.DWord = value;
        }
        protected uint ReadRegister(Register register)
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
            if (cmd == FIFO.Max)
            {
                debugger.Send("Setting max to " + value);
            }
            return FIFO_Memory[(uint)cmd] = value;
        }
        protected void WaitForFifo()
        {
            WriteRegister(Register.Sync, 1);
            while (ReadRegister(Register.Busy) != 0) { }
        }

        readonly uint[] lastLocations = new uint[128];
        uint startPos = 0;
        protected void WriteToFifo(uint value)
        {
            if (((GetFIFO(FIFO.NextCmd) == GetFIFO(FIFO.Max) - 4) && GetFIFO(FIFO.Stop) == GetFIFO(FIFO.Min)) ||
                (GetFIFO(FIFO.NextCmd) + 4 == GetFIFO(FIFO.Stop)))
            {
                WaitForFifo();
            }
            if (GetFIFO(FIFO.NextCmd) == (uint)FIFO.Max)
            {
                debugger.SendInternal(startPos);
                for (int i = 0; i < 128; i++)
                {
                    debugger.SendInternal(lastLocations[i]);
                }
            }
            SetFIFO((FIFO)GetFIFO(FIFO.NextCmd), value);
            SetFIFO(FIFO.NextCmd, GetFIFO(FIFO.NextCmd) + 4);
            lastLocations[startPos] = GetFIFO(FIFO.NextCmd);
            startPos = (startPos + 1) % 128;

            if (GetFIFO(FIFO.NextCmd) == GetFIFO(FIFO.Max))
            {
                debugger.Send("Setting FIFO.NextCmd to " + GetFIFO(FIFO.Min));
                SetFIFO(FIFO.NextCmd, GetFIFO(FIFO.Min));
            }
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
        public void SetPixel(int x, int y, Pen color)
        {
            Video_Memory[(((uint)y * width + (uint)x) * depth)] = (uint)color.Color.ToArgb();
        }
        public uint GetPixel(int x, int y)
        {
            return Video_Memory[(((uint)y * width + (uint)x) * depth)];
        }
        public void Copy(uint x, uint y, uint newX, uint newY, uint width, uint height)
        {
            if ((capabilities & (uint)Capability.RectCopy) != 0)
            {
                WriteToFifo((uint)FIFOCommand.RECT_COPY);
                WriteToFifo(x);
                WriteToFifo(y);
                WriteToFifo(newX);
                WriteToFifo(newY);
                WriteToFifo(width);
                WriteToFifo(height);
                WaitForFifo();
            }
            else
                throw new NotImplementedException("VMWareSVGAII Copy()");
        }
        public void Fill(uint x, uint y, uint width, uint height, Pen color)
        {
            if ((capabilities & (uint)Capability.RectFill) != 0)
            {
                WriteToFifo((uint)FIFOCommand.RECT_FILL);
                WriteToFifo((uint)color.Color.ToArgb());
                WriteToFifo(x);
                WriteToFifo(y);
                WriteToFifo(width);
                WriteToFifo(height);
                WaitForFifo();
            }
            else
            {
                if ((capabilities & (uint)Capability.RectCopy) != 0)
                {
                    // fill first line and copy it to all other
                    uint xTarget = (x + width);
                    uint yTarget = (y + height);

                    for (uint xTmp = x; xTmp < xTarget; xTmp++)
                    {
                        SetPixel((int)xTmp, (int)y, color);
                    }
                    // refresh first line for copy process
                    Update(x, y, width, 1);
                    for (uint yTmp = y + 1; yTmp < yTarget; yTmp++)
                    {
                        Copy(x, y, x, yTmp, width, 1);
                    }
                }
                else
                {
                    uint xTarget = (x + width);
                    uint yTarget = (y + height);
                    for (uint xTmp = x; xTmp < xTarget; xTmp++)
                    {
                        for (uint yTmp = y; yTmp < yTarget; yTmp++)
                        {
                            SetPixel((int)xTmp, (int)yTmp, color);
                        }
                    }
                    Update(x, y, width, height);
                }
            }
        }
        public void Enable()
        {
            WriteRegister(Register.Enable, 1);
        }
        public void Disable()
        {
            WriteRegister(Register.Enable, 0);
        }
        public List<Mode> AvailableModes { get; }
        public Mode DefaultGraphicMode { get; }
        public Mode Mode { get; set; }
        public void Clear()
        {
            Clear(Color.Black);
        }
        public virtual void Clear(Color color)
        {
            Pen pen = new Pen(color);

            for (int x = 0; x < Mode.Rows; x++)
            {
                for (int y = 0; y < Mode.Columns; y++)
                {
                    Fill((uint)x, (uint)y, width, height, pen);
                    //SetPixel(x, y, pen);
                }
            }
        }
        private void DrawHorizontalLine(Pen pen, int dx, int x1, int y1)
            {
                int i;

                for (i = 0; i < dx; i++)
                {
                   SetPixel(x1 + i, y1, pen);
                }
            }
        private void DrawVerticalLine(Pen pen, int dy, int x1, int y1)
            {
                int i;

                for (i = 0; i < dy; i++)
                {
                    SetPixel(x1, y1 + i, pen);
                }
            }
        private void DrawDiagonalLine(Pen pen, int dx, int dy, int x1, int y1)
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
                    SetPixel(px, py, pen);
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
                        SetPixel(px, py, pen);
                    }
                }
            }
        public virtual void DrawLine(Pen pen, int x1, int y1, int x2, int y2)
            {
                if (pen == null)
                {
                    throw new ArgumentOutOfRangeException(nameof(pen));
                }

                // trim the given line to fit inside the canvas boundries
                TrimLine(ref x1, ref y1, ref x2, ref y2);

                int dx, dy;

                dx = x2 - x1;      /* the horizontal distance of the line */
                dy = y2 - y1;      /* the vertical distance of the line */

                if (dy == 0) /* The line is horizontal */
                {
                    DrawHorizontalLine(pen, dx, x1, y1);
                    return;
                }

                if (dx == 0) /* the line is vertical */
                {
                    DrawVerticalLine(pen, dy, x1, y1);
                    return;
                }

                /* the line is neither horizontal neither vertical, is diagonal then! */
                DrawDiagonalLine(pen, dx, dy, x1, y1);
            }
        public void DrawLine(Pen pen, Cosmos.System.Graphics.Point p1, Cosmos.System.Graphics.Point p2)
            {
                DrawLine(pen, p1.X, p1.Y, p2.X, p2.Y);
            }
        public void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
            {
                throw new NotImplementedException();
            }
        public virtual void DrawCircle(Pen pen, int x_center, int y_center, int radius)
            {
                if (pen == null)
                {
                    throw new ArgumentNullException(nameof(pen));
                }
                ThrowIfCoordNotValid(x_center + radius, y_center);
                ThrowIfCoordNotValid(x_center - radius, y_center);
                ThrowIfCoordNotValid(x_center, y_center + radius);
                ThrowIfCoordNotValid(x_center, y_center - radius);
                int x = radius;
                int y = 0;
                int e = 0;

                while (x >= y)
                {
                    SetPixel(x_center + x, y_center + y, pen);
                    SetPixel(x_center + y, y_center + x, pen);
                    SetPixel(x_center - y, y_center + x, pen);
                    SetPixel(x_center - x, y_center + y, pen);
                    SetPixel(x_center - x, y_center - y, pen);
                    SetPixel(x_center - y, y_center - x, pen);
                    SetPixel(x_center + y, y_center - x, pen);
                    SetPixel(x_center + x, y_center - y, pen);

                    y++;
                    if (e <= 0)
                    {
                        e += 2 * y + 1;
                    }
                    if (e > 0)
                    {
                        x--;
                        e -= 2 * x + 1;
                    }
                }
            }
        public virtual void DrawCircle(Pen pen, Cosmos.System.Graphics.Point point, int radius)
            {
                DrawCircle(pen, point.X, point.Y, radius);
            }
        public virtual void DrawFilledCircle(Pen pen, int x0, int y0, int radius)
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

                        SetPixel(i, y0 + y, pen);
                        SetPixel(i, y0 - y, pen);
                    }
                    for (int i = x0 - y; i <= x0 + y; i++)
                    {
                        SetPixel(i, y0 + x, pen);
                        SetPixel(i, y0 - x, pen);
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
        public virtual void DrawFilledCircle(Pen pen, Cosmos.System.Graphics.Point point, int radius)
            {
                DrawFilledCircle(pen, point.X, point.Y, radius);
            }
        public virtual void DrawEllipse(Pen pen, int x_center, int y_center, int x_radius, int y_radius)
            {
                if (pen == null)
                {
                    throw new ArgumentNullException(nameof(pen));
                }
                ThrowIfCoordNotValid(x_center + x_radius, y_center);
                ThrowIfCoordNotValid(x_center - x_radius, y_center);
                ThrowIfCoordNotValid(x_center, y_center + y_radius);
                ThrowIfCoordNotValid(x_center, y_center - y_radius);
                int a = 2 * x_radius;
                int b = 2 * y_radius;
                int b1 = b & 1;
                int dx = 4 * (1 - a) * b * b;
                int dy = 4 * (b1 + 1) * a * a;
                int err = dx + dy + b1 * a * a;
                int e2;
                int y = 0;
                int x = x_radius;
                a *= 8 * a;
                b1 = 8 * b * b;

                while (x >= 0)
                {
                    SetPixel(x_center + x, y_center + y, pen);
                    SetPixel(x_center - x, y_center + y, pen);
                    SetPixel(x_center - x, y_center - y, pen);
                    SetPixel(x_center + x, y_center - y, pen);
                    e2 = 2 * err;
                    if (e2 <= dy) { y++; err += dy += a; }
                    if (e2 >= dx || 2 * err > dy) { x--; err += dx += b1; }
                }
            }
        public virtual void DrawEllipse(Pen pen, Cosmos.System.Graphics.Point point, int x_radius, int y_radius)
            {
                DrawEllipse(pen, point.X, point.Y, x_radius, y_radius);
            }
        public virtual void DrawFilledEllipse(Pen pen, Cosmos.System.Graphics.Point point, int height, int width)
            {
                for (int y = -height; y <= height; y++)
                {
                    for (int x = -width; x <= width; x++)
                    {
                        if (x * x * height * height + y * y * width * width <= height * height * width * width)
                        {
                            SetPixel(point.X + x, point.Y + y, pen);
                        }
                    }
                }
            }
        public virtual void DrawFilledEllipse(Pen pen, int x, int y, int height, int width)
            {
                DrawFilledEllipse(pen, new Cosmos.System.Graphics.Point(x, y), height, width);
            }
        public virtual void DrawPolygon(Pen pen, params Cosmos.System.Graphics.Point[] points)
            {
                if (points.Length < 3)
                {
                    throw new ArgumentException("A polygon requires more than 3 points.");
                }
                if (pen == null)
                {
                    throw new ArgumentNullException(nameof(pen));
                }
                for (int i = 0; i < points.Length - 1; i++)
                {
                    DrawLine(pen, points[i], points[i + 1]);
                }
                DrawLine(pen, points[0], points[points.Length - 1]);
            }
        public virtual void DrawSquare(Pen pen, Cosmos.System.Graphics.Point point, int size)
            {
                DrawRectangle(pen, point.X, point.Y, size, size);
            }
        public virtual void DrawSquare(Pen pen, int x, int y, int size)
            {
                DrawRectangle(pen, x, y, size, size);
            }
        public virtual void DrawRectangle(Pen pen, Cosmos.System.Graphics.Point point, int width, int height)
            {
                DrawRectangle(pen, point.X, point.Y, width, height);
            }
        public virtual void DrawRectangle(Pen pen, int x, int y, int width, int height)
            {
                /*
                 * we must draw four lines connecting any vertex of our rectangle to do this we first obtain the position of these
                 * vertex (we call these vertexes A, B, C, D as for geometric convention)
                 */
                if (pen == null)
                {
                    throw new ArgumentNullException(nameof(pen));
                }
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
                DrawLine(pen, xa, ya, xb, yb);

                /* Draw a line between A and C */
                DrawLine(pen, xa, ya, xc, yc);

                /* Draw a line between B and D */
                DrawLine(pen, xb, yb, xd, yd);

                /* Draw a line between C and D */
                DrawLine(pen, xc, yc, xd, yd);
            }
        public virtual void DrawFilledRectangle(Pen pen, Cosmos.System.Graphics.Point point, int width, int height)
        {
            DrawFilledRectangle(pen, point.X, point.Y, width, height);
        }
        public virtual void DrawFilledRectangle(Pen pen, int x_start, int y_start, int width, int height)
            {
                if (height == -1)
                {
                    height = width;
                }

                for (int y = y_start; y < y_start + height; y++)
                {
                    DrawLine(pen, x_start, y, x_start + width - 1, y);
                }
            }
        public virtual void DrawTriangle(Pen pen, Cosmos.System.Graphics.Point point0, Cosmos.System.Graphics.Point point1, Cosmos.System.Graphics.Point point2)
            {
                DrawTriangle(pen, point0.X, point0.Y, point1.X, point1.Y, point2.X, point2.Y);
            }
        public virtual void DrawTriangle(Pen pen, int v1x, int v1y, int v2x, int v2y, int v3x, int v3y)
            {
                DrawLine(pen, v1x, v1y, v2x, v2y);
                DrawLine(pen, v1x, v1y, v3x, v3y);
                DrawLine(pen, v2x, v2y, v3x, v3y);
            }
        public virtual void DrawRectangle(Pen pen, float x_start, float y_start, float width, float height)
            {
                throw new NotImplementedException();
            }
        public virtual void DrawImage(Image image, int x, int y)
            {
                for (int _x = 0; _x < image.Width; _x++)
                {
                    for (int _y = 0; _y < image.Height; _y++)
                    {
                        SetPixel(x + _x, y + _y, new Pen(Color.FromArgb(image.rawData[_x + _y * image.Width])));
                    }
                }
            }
        private int[] ScaleImage(Image image, int newWidth, int newHeight)
            {
                int[] pixels = image.rawData;
                int w1 = (int)image.Width;
                int h1 = (int)image.Height;
                int[] temp = new int[newWidth * newHeight];
                int x_ratio = (int)((w1 << 16) / newWidth) + 1;
                int y_ratio = (int)((h1 << 16) / newHeight) + 1;
                int x2, y2;
                for (int i = 0; i < newHeight; i++)
                {
                    for (int j = 0; j < newWidth; j++)
                    {
                        x2 = ((j * x_ratio) >> 16);
                        y2 = ((i * y_ratio) >> 16);
                        temp[(i * newWidth) + j] = pixels[(y2 * w1) + x2];
                    }
                }
                return temp;
            }
        public virtual void DrawImage(Image image, int x, int y, int w, int h)
            {
                int[] pixels = ScaleImage(image, w, h);
                for (int _x = 0; _x < w; _x++)
                {
                    for (int _y = 0; _y < h; _y++)
                    {
                        SetPixel(x + _x, y + _y, new Pen(Color.FromArgb(pixels[_x + _y * w])));
                    }
                }
            }
        public void DrawImageAlpha(Image image, int x, int y)
            {
                for (int _x = 0; _x < image.Width; _x++)
                {
                    for (int _y = 0; _y < image.Height; _y++)
                    {
                        Global.mDebugger.SendInternal(image.rawData[_x + _y * image.Width]);
                        SetPixel(x + _x, y + _y, new Pen(Color.FromArgb(image.rawData[_x + _y * image.Width])));
                    }
                }
            }
        public void DrawImage(Image image, Cosmos.System.Graphics.Point point)
            {
                DrawImage(image, point.X, point.Y);
            }
        public void DrawImageAlpha(Image image, Cosmos.System.Graphics.Point point)
            {
                DrawImageAlpha(image, point.X, point.Y);
            }
        public void DrawString(string str, Font aFont, Pen pen, Cosmos.System.Graphics.Point point)
            {
                DrawString(str, aFont, pen, point.X, point.Y);
            }
        public void DrawString(string str, Font aFont, Pen pen, int x, int y)
            {
                foreach (char c in str)
                {
                    DrawChar(c, aFont, pen, x, y); ;
                    x += aFont.Width;
                }
            }
        public void DrawChar(char c, Font aFont, Pen pen, Cosmos.System.Graphics.Point point)
            {
                DrawChar(c, aFont, pen, point.X, point.Y);
            }
        public void DrawChar(char c, Font aFont, Pen pen, int x, int y)
            {
                int p = aFont.Height * (byte)c;

                for (int cy = 0; cy < aFont.Height; cy++)
                {
                    for (byte cx = 0; cx < aFont.Width; cx++)
                    {
                        if (aFont.ConvertByteToBitAddres(aFont.Data[p + cy], cx + 1))
                        {
                            SetPixel((ushort)((x) + (aFont.Width - cx)), (ushort)((y) + cy), pen);
                        }
                    }
                }
            }
        protected bool CheckIfModeIsValid(Mode mode)
            {
            foreach (var elem in AvailableModes)
                {
                    if (elem == mode)
                    {
                        return true; // All OK mode does exists in availableModes
                    }
                }
                return false;
            }
        protected void ThrowIfModeIsNotValid(Mode mode)
            {
                if (CheckIfModeIsValid(mode))
                {
                    return;
                }

                Global.mDebugger.SendInternal($"Mode {mode} is not found! Raising exception...");
                /* 'mode' was not in the 'availableModes' List ==> 'mode' in NOT Valid */
                throw new ArgumentOutOfRangeException(nameof(mode), $"Mode {mode} is not supported by this Driver");
            }
        protected void ThrowIfCoordNotValid(Cosmos.System.Graphics.Point point)
            {
                ThrowIfCoordNotValid(point.X, point.Y);
            }
        protected void ThrowIfCoordNotValid(int x, int y)
            {
                if (x < 0 || x >= Mode.Columns)
                {
                    throw new ArgumentOutOfRangeException(nameof(x), $"x ({x}) is not between 0 and {Mode.Columns}");
                }

                if (y < 0 || y >= Mode.Rows)
                {
                    throw new ArgumentOutOfRangeException(nameof(y), $"y ({y}) is not between 0 and {Mode.Rows}");
                }
            }
        protected void TrimLine(ref int x1, ref int y1, ref int x2, ref int y2)
            {
                // in case of vertical lines, no need to perform complex operations
                if (x1 == x2)
                {
                    x1 = Math.Min(Mode.Columns - 1, Math.Max(0, x1));
                    x2 = x1;
                    y1 = Math.Min(Mode.Rows - 1, Math.Max(0, y1));
                    y2 = Math.Min(Mode.Rows - 1, Math.Max(0, y2));

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
                else if (x1_out >= Mode.Columns)
                {
                    x1_out = Mode.Columns - 1;
                    y1_out = (Mode.Columns - 1) * m + c;
                }

                // handle x2
                if (x2_out < 0)
                {
                    x2_out = 0;
                    y2_out = c;
                }
                else if (x2_out >= Mode.Columns)
                {
                    x2_out = Mode.Columns - 1;
                    y2_out = (Mode.Columns - 1) * m + c;
                }

                // handle y1
                if (y1_out < 0)
                {
                    x1_out = -c / m;
                    y1_out = 0;
                }
                else if (y1_out >= Mode.Rows)
                {
                    x1_out = (Mode.Rows - 1 - c) / m;
                    y1_out = Mode.Rows - 1;
                }

                // handle y2
                if (y2_out < 0)
                {
                    x2_out = -c / m;
                    y2_out = 0;
                }
                else if (y2_out >= Mode.Rows)
                {
                    x2_out = (Mode.Rows - 1 - c) / m;
                    y2_out = Mode.Rows - 1;
                }

                // final check, to avoid lines that are totally outside bounds
                if (x1_out < 0 || x1_out >= Mode.Columns || y1_out < 0 || y1_out >= Mode.Rows)
                {
                    x1_out = 0; x2_out = 0;
                    y1_out = 0; y2_out = 0;
                }

                if (x2_out < 0 || x2_out >= Mode.Columns || y2_out < 0 || y2_out >= Mode.Rows)
                {
                    x1_out = 0; x2_out = 0;
                    y1_out = 0; y2_out = 0;
                }

                // replace inputs with new values
                x1 = (int)x1_out; y1 = (int)y1_out;
                x2 = (int)x2_out; y2 = (int)y2_out;
            }
        public Color AlphaBlend(Color to, Color from, byte alpha)
            {
                byte R = (byte)((to.R * alpha + from.R * (255 - alpha)) >> 8);
                byte G = (byte)((to.G * alpha + from.G * (255 - alpha)) >> 8);
                byte B = (byte)((to.B * alpha + from.B * (255 - alpha)) >> 8);
                return Color.FromArgb(R, G, B);
            }
    }
}