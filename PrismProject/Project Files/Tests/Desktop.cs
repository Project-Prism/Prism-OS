using System;
using System.Collections.Generic;
using PrismProject.System2.Drivers;
using PrismProject.System2.Drawing;

namespace PrismProject.Software
{
    internal class Desktop
    {
        #region UIGen-setup
        private static readonly UILib drawing = new UILib();
        private static readonly int SH = Video.SH;
        public static List<WinLib.GuiWindow> Windows = new List<WinLib.GuiWindow>();
        public static WinLib.BaseGuiElement ActiveElement = null;
        #endregion

        public static void Start()
        {
            Testapp1.Start();
            Windows.Add(Testapp1.MainWindow);

            int clickX = -100, clickY = -100;
            bool clickDown = false;
            Video.Screen.Clear(Themes.desktop);

            while (true)
            {
                foreach (var window in Windows)
                {
                    if (clickDown && clickX > window.X && clickX < window.X + window.Width && clickY > window.Y && clickY < window.Y + SH / 25)
                    {
                        if (Math.Abs(clickX - Mouse.X) > 4 || Math.Abs(clickY - Mouse.Y) > 4)
                        {
                            window.Dragging(Mouse.X - Mouse.lastX, Mouse.Y - Mouse.lastY);
                        }
                    }

                    window.Render(drawing);
                }

                #region Mouse stuff

                if ((Mouse.State & Cosmos.System.MouseState.Left) == Cosmos.System.MouseState.Left)
                {
                    if (!clickDown)
                    {
                        clickX = Mouse.X;
                        clickY = Mouse.Y;
                        clickDown = true;
                    }
                }
                else if (clickDown)
                {
                    if (Math.Abs(clickX - Mouse.X) < 4 && Math.Abs(clickY - Mouse.Y) < 4)
                    {
                        ActiveElement = null;
                        foreach (var window in Windows)
                        {
                            if (clickX > window.X && clickX < window.X + window.Width && clickY > window.Y && clickY < window.Y + window.Height)
                            {
                                window.Click(clickX - window.X, clickY - window.Y - (SH / 25), 1);
                            }
                        }
                    }
                    clickX = -100;
                    clickY = -100;
                }
                if (ActiveElement != null)
                {
                    if (Cosmos.System.KeyboardManager.TryReadKey(out Cosmos.System.KeyEvent key))
                    { ActiveElement.Key(key); }
                }
                Mouse.Update();

                #endregion Mouse stuff
            }
        }
    }
}