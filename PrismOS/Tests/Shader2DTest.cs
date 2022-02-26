using PrismOS.Graphics;
using System.Drawing;

namespace PrismOS.Tests
{
    public class Shader2DTest
    {
        public int BX = 500, BY = 500;
        public float Brightness = 1.0f;
        public Color LampColor = Color.Yellow;

        public void Update(Canvas Canvas)
        {
            int R = 0;
            for (float I = 255; I > 0; I -= Brightness)
            {
                Canvas.DrawCircle(BX, BY, R++, Color.FromArgb((int)I, LampColor));
            }
        }
    }
}