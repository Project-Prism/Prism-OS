using Cosmos.HAL.Drivers;
using PrismOS.Generic;
using PrismOS.Graphics.Utilities;

namespace PrismOS.Graphics.GUI
{
    public class WindowManager
    {
        public WindowManager(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
            Screen = new(Width, Height);
            VBE = new((ushort)Width, (ushort)Height, 32);
        }

        public List<Window> Windows { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public VBEDriver VBE { get; }
        public VScreen Screen { get; }

        public void Draw()
        {
            foreach (Window Window in Windows)
            {
                Window.Draw();
                for (int IX = 0; IX < Width; IX++)
                {
                    for (int IY = 0; IY < Height; IY++)
                    {
                        Screen.SetPixel(Window.X + IX, Window.Y + IY, Window.Screen.GetPixel(IX, IY));
                    }
                }
                Cosmos.Core.Global.BaseIOGroups.VBE.LinearFrameBuffer.Copy(Screen.Buffer, 0, Screen.Buffer.Length);
            }
        }
    }
}