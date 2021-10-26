using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System.Drawing;

namespace Prism.Graphics
{
    /// <summary>
    /// Extended display functions, basicly canvas on steroids.
    /// currently untested & unfinished, but this is the final canvas method prism will use.
    /// </summary>
    internal static class ED1
    {
        /// <summary>
        /// Standard canvas control
        /// </summary>
        public static class Standard
        {
            #region Config
            /// <summary>
            /// Screen width
            /// </summary>
            public static int SWidth = 740;
            /// <summary>
            /// Screen height
            /// </summary>
            public static int SHeight = 480;
            /// <summary>
            /// Screen mode
            /// </summary>
            public static Mode Mode = new Mode(740, 480, ColorDepth.ColorDepth32);
            /// <summary>
            /// Screen contoler
            /// </summary>
            public static Canvas Display = FullScreenCanvas.GetFullScreenCanvas(Mode);
            #endregion Config

            #region Drawing
            public static void Clear(Color color)
            {
                Display.Clear(color);
            }

            public static void DrawRectangle(int X, int Y, int Width, int Height, Color color)
            {
                Display.DrawFilledRectangle(new Pen(color), X, Y, Width, Height);
            }

            public static void DrawCircle(int X, int Y, int R, Color color)
            {
                Display.DrawFilledCircle(new Pen(color), X, Y, R);
            }

            public static void DrawImage(int X, int Y, Bitmap aBitmap)
            {
                Display.DrawImageAlpha(aBitmap, X, Y);
            }

            public static void DrawVirtualScreen(StandardVirtual bitmap)
            {
                Display.DrawImageAlpha(bitmap.VirtualDisplay, 0, 0);
            }
            #endregion Drawing
        }

        /// <summary>
        /// virtual (buffer) canvas that doesnt show up on screen until it it manualy drawn
        /// </summary>
        public class StandardVirtual
        {
            #region Config
            public Bitmap VirtualDisplay;
            public StandardVirtual()
            {
                VirtualDisplay = new Bitmap((uint)Standard.SWidth, (uint)Standard.SHeight, ColorDepth.ColorDepth32);
            }
            #endregion Config

            #region Drawing
            public void Clear(Color color)
            {
                for (int X = 0; X < (int)VirtualDisplay.Width; X++)
                {
                    for (int Y = 0; Y < (int)VirtualDisplay.Height; Y++)
                    {
                        VirtualDisplay.rawData[GetPointOffset(X, Y)] = color.ToArgb();
                    }
                }
            }
            #endregion Drawing

            #region Tools
            public Color GetPixel(int X, int Y)
            {
                return Color.FromArgb(VirtualDisplay.rawData[X + (Y * VirtualDisplay.Width)]);
            }
            public void SetPixel(int X, int Y, Color aColor)
            {
                VirtualDisplay.rawData[X + (Y * VirtualDisplay.Width)] = aColor.ToArgb();
            }
            public int GetPointOffset(int X, int Y)
            {
                int xBytePerPixel = 32 / 8;
                int stride = 32 / 8;
                int pitch = (int)VirtualDisplay.Width * xBytePerPixel;

                return (X * stride) + (Y * pitch);
            }
            #endregion Tools
        } // need to-do

        /// <summary>
        /// Prism OS assets
        /// </summary>
        public static class Assets
        {
            [ManifestResourceStream(ResourceName = "Prism.Graphics.Icons.Prism.bmp")] static readonly byte[] Byte_Prism;
            [ManifestResourceStream(ResourceName = "Prism.Graphics.Icons.Mouse.bmp")] static readonly byte[] Byte_Mouse;
            [ManifestResourceStream(ResourceName = "Prism.Graphics.Icons.Warning.bmp")] static readonly byte[] Byte_Warning;
        }
    }
}
