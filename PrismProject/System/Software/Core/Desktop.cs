using System;
using System.Collections.Generic;
using System.Drawing;

namespace PrismProject
{
    internal class Desktop
    {
        #region Themes and GUI vars

        //Default theme colors
        public static Color Window = Color.FromArgb(25, 25, 45);
        public static Color Background = Color.FromArgb(15, 52, 96);
        public static Color Text = Color.White;
        public static Color Text_invert = Color.Black;
        public static Color Accent = Color.FromArgb(255, 82, 0);
        public static Color Accent_unfocus = Color.FromArgb(68, 68, 68);

        //Define graphics variables
        private static readonly int screenX = Driver.screenX, screenY = Driver.screenY;

        private static readonly G_lib draw = new G_lib();
        private static readonly Cursor cursor = new Cursor();
        public static List<GuiWindow> Windows = new List<GuiWindow>();
        public static BaseGuiElement ActiveElement = null;

        #endregion Themes and GUI vars

        public static void Start()
        {
            var testWindow = new GuiWindow(screenX / 5, screenY / 5, screenX / 2, screenX / 2, "App menu", 5);

            testWindow.AddChild(new GuiButton(16, 64, 180, 30, "Click to reboot now", true, Text, Accent, (self) =>
            {
                Cosmos.System.Power.Reboot();
            }));
            testWindow.AddChild(new GuiText(30, 162, "Wi-Fi"));
            testWindow.AddChild(new GuiSwitch(90, 160, Accent_unfocus, Accent, true, (self) =>
            {
                if (self.status) { self.status = false; }
                if (!self.status) { self.status = true; }
            }));
            testWindow.AddChild(new GuiDiv(20, 150, 40, 150, Accent));
            
            Windows.Add(testWindow);

            int clickX = -100, clickY = -100;
            bool clickDown = false;
            
            Driver.Clear(Background);
            while (Kernel.canvasRunning)
            {
                draw.Box(Accent_unfocus, 0, screenY - 30, screenX, 30);
                draw.Circle(Color.White, 16, screenY - 15, 10);
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

                #region Mouse stuff

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
                    { ActiveElement.Key(key); }
                }
                cursor.Update();

                #endregion Mouse stuff
            }
        }
    }
}