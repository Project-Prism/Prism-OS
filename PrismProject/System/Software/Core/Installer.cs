using System;
using System.Collections.Generic;
using System.Drawing;

namespace PrismProject
{
    internal class Installer
    {
        //Default theme colors
        public static Color Title_bar = Color.FromArgb(0, 120, 212);

        public static Color Title_text = Color.White;
        public static Color Window_main = Color.FromArgb(0, 25, 30, 35);
        public static Color Task_bar = Color.FromArgb(0, 120, 212);
        public static Color Button = Color.FromArgb(0, 30, 50, 40);
        public static Color Background = Color.FromArgb(40, 40, 40);
        public static Color Text = Color.White;

        //Define graphics variables
        private static readonly int screenX = Driver.screenX, screenY = Driver.screenY;

        private static readonly G_lib draw = new G_lib();
        private static readonly Cursor cursor = new Cursor();
        public static List<GuiWindow> Windows = new List<GuiWindow>();
        public static BaseGuiElement ActiveElement = null;

        public static void Start()
        {
            Driver.Clear(Background);

            var testWindow = new GuiWindow("Prism setup", screenX / 4, screenY / 4, screenX / 2, screenX / 2);
            testWindow.AddChild(new GuiButton("Install now", (self) => { Install(); }, true, screenX / 3, 500, Convert.ToInt32(screenX / 1.5), 32));
            Windows.Add(testWindow);
            int clickX = -100, clickY = -100;
            bool clickDown = false;

            while (Kernel.canvasRunning)
            {
                foreach (var window in Windows)
                {
                    if (clickDown && clickX > window.X && clickX < window.X + window.Width && clickY > window.Y && clickY < window.Y + screenY / 25)
                    {
                        if (Math.Abs(clickX - Cursor.X) > 4 || Math.Abs(clickY - Cursor.Y) > 4)
                        {
                            window.Dragging(Cursor.X - Cursor.lastX, Cursor.Y - Cursor.lastY);
                        }
                    }

                    window.Render(draw);
                }
                if ((Cursor.State & Cosmos.System.MouseState.Left) == Cosmos.System.MouseState.Left)
                {
                    if (!clickDown)
                    {
                        clickX = Cursor.X;
                        clickY = Cursor.Y;
                        clickDown = true;
                    }
                }
                else if (clickDown)
                {
                    clickDown = false;
                    if (Math.Abs(clickX - Cursor.X) < 4 && Math.Abs(clickY - Cursor.Y) < 4)
                    {
                        ActiveElement = null;
                        foreach (var window in Windows)
                        {
                            if (clickX > window.X && clickX < window.X + window.Width && clickY > window.Y && clickY < window.Y + window.Height)
                            {
                                window.Click(clickX - window.X, clickY - window.Y - (screenY / 25), 1);
                            }
                        }
                    }
                    clickX = -100;
                    clickY = -100;
                }
                if (ActiveElement != null)
                {
                    
                    if (Cosmos.System.KeyboardManager.TryReadKey(out Cosmos.System.KeyEvent key))
                    {
                        ActiveElement.Key(key);
                    }
                }
                cursor.Update();
            }
        }

        public static void Install()
        {
            Driver.Clear(Background);

            var testWindow = new GuiWindow("Prism setup", screenX / 4, screenY / 4, screenX / 2, screenX / 2);
            Windows.Add(testWindow);
            int clickX = -100, clickY = -100;
            bool clickDown = false;

            while (Kernel.canvasRunning)
            {
                foreach (var window in Windows)
                {
                    if (clickDown && clickX > window.X && clickX < window.X + window.Width && clickY > window.Y && clickY < window.Y + screenY / 25)
                    {
                        if (Math.Abs(clickX - Cursor.X) > 4 || Math.Abs(clickY - Cursor.Y) > 4)
                        {
                            window.Dragging(Cursor.X - Cursor.lastX, Cursor.Y - Cursor.lastY);
                        }
                    }

                    window.Render(draw);
                }
                if ((Cursor.State & Cosmos.System.MouseState.Left) == Cosmos.System.MouseState.Left)
                {
                    if (!clickDown)
                    {
                        clickX = Cursor.X;
                        clickY = Cursor.Y;
                        clickDown = true;
                    }
                }
                else if (clickDown)
                {
                    clickDown = false;
                    if (Math.Abs(clickX - Cursor.X) < 4 && Math.Abs(clickY - Cursor.Y) < 4)
                    {
                        ActiveElement = null;
                        foreach (var window in Windows)
                        {
                            if (clickX > window.X && clickX < window.X + window.Width && clickY > window.Y && clickY < window.Y + window.Height)
                            {
                                window.Click(clickX - window.X, clickY - window.Y - (screenY / 25), 1);
                            }
                        }
                    }
                    clickX = -100;
                    clickY = -100;
                }
                if (ActiveElement != null)
                {
                    
                    if (Cosmos.System.KeyboardManager.TryReadKey(out Cosmos.System.KeyEvent key))
                    {
                        ActiveElement.Key(key);
                    }
                }
                cursor.Update();
            }
        }
    }
}