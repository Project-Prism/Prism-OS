using System;
using System.Drawing;
using System.Collections.Generic;

namespace PrismProject
{
    class Desktop
    {
        #region Themes and GUI vars
        //Default theme colors
        public static Color Window = Color.FromArgb(23, 23, 37);
        public static Color Background = Color.FromArgb(23,23,27);
        public static Color Text = Color.White;
        public static Color Accent_Text = Color.Black;
        public static Color Accent = Color.FromArgb(0,120,212);
        public static Color Accent_Dark = Color.FromArgb(23, 23, 37);
        public static Color Accent_Medium = Color.FromArgb(33, 33, 47);

        //Define graphics variables
        private static int screenX = Driver.screenX, screenY = Driver.screenY;
        private static G_lib draw = new G_lib();
        private static Cursor cursor = new Cursor();
        public static List<GuiWindow> Windows = new List<GuiWindow>();
        public static BaseGuiElement ActiveElement = null;
        #endregion
        public static void Start()
        {
            var testWindow = new GuiWindow("App menu", screenX / 4, screenY / 4, screenX / 2, screenX / 2, 2);
            testWindow.AddChild(new GuiButton("random accent color", (self) => { 
                Accent = Color.FromArgb(Driver.randomcolor); }, true, 16, 32, 170, 20));
            testWindow.AddChild(new GuiButton("Click to reboot", (self) => { 
                Cosmos.System.Power.Reboot(); }, true, 16, 64, 170, 20));
            
            testWindow.AddChild(new GuiText("Wi-Fi", 32, 162));
            testWindow.AddChild(new GuiSwitch((self) => { 
                if (self.status) { self.status = false; } if (!self.status) { self.status = true; } }, Accent_Medium, Accent, false, 200, 160));
            testWindow.AddChild(new GuiDiv(0, 150, 40, testWindow.Width, Accent));

            Windows.Add(testWindow);


            int clickX = -100, clickY = -100;
            bool clickDown = false;
            Driver.clear(Background);

            while (Kernel.canvasRunning)
            {
                draw.Box(Accent, 0, screenY - 30, screenX, 30);
                draw.Circle(Color.White, 16, screenY-15, 10);
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
                    Cosmos.System.KeyEvent key;
                    if (Cosmos.System.KeyboardManager.TryReadKey(out key))
                    { ActiveElement.Key(key); }
                }
                cursor.Update();
                #endregion
            }
        }
    }
}
