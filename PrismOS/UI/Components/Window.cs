using System.Collections.Generic;

namespace PrismOS.UI.Components
{
    public class Window : IBase
    {
        internal delegate void Event();
        internal Event OnClickDown;
        internal Event OnClickUp;
        internal Event OnHover;
        internal Event OnDraw;
        internal Event OnRemove;
        internal Event OnAdd;

        public List<IBase> Children;
        public int X, Y, Width, Height;

        public Window(int X, int Y, int Width, int Height)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
        }

        public void Draw()
        {
            OnDraw();
            //Canvas.DrawFilledRectangle(new Cosmos.System.Graphics.Pen(System.Drawing.Color.White), X, Y, Width, Height);
        }
        public void ClickDown()
        {
            OnClickDown();
        }
        public void ClickUp()
        {
            OnClickUp();
        }
        public void Hover()
        {
            OnHover();
        }
        public void Destroy()
        {
            OnRemove();
        }
        public byte[] ToBytes()
        {
            List<byte> Bytes = new();
            Bytes.Add((byte)Width);
            Bytes.Add((byte)Height);
            return Bytes.ToArray();
        }
        public static IBase FromBytes(byte[] Bytes)
        {
            return new Window(0, 0, Bytes[0], Bytes[1]);
        }
    }
}
