using System.Collections.Generic;
using static PrismOS.UI.Canvas;

namespace PrismOS.UI.Components
{
    public class Label : IBase
    {
        internal delegate void Event();
        internal Event OnClickDown;
        internal Event OnClickUp;
        internal Event OnHover;
        internal Event OnDraw;
        internal Event OnRemove;
        internal Event OnAdd;

        public int X, Y;
        public string Text;

        public Label(int X, int Y, string Text)
        {
            this.X = X;
            this.Y = Y;
            this.Text = Text;
        }

        public void Draw()
        {
            OnDraw();
            // Need to add
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
            Bytes.Add((byte)Text.Length);
            foreach (char Char in Text)
            {
                Bytes.Add((byte)Char);
            }
            return Bytes.ToArray();
        }
        public static IBase FromBytes(byte[] Bytes)
        {
            return new Window(0, 0, Bytes[0], Bytes[1]);
        }
    }
}
