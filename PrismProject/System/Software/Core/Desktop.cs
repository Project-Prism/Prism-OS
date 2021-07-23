using System;
using System.Collections.Generic;
using System.Drawing;

namespace PrismProject
{
    internal class Desktop
    {
        public struct UIcolors
        {
            public static Color Window_main = Color.FromArgb(25, 25, 45);
            public static Color Desktop_main = Color.FromArgb(30, 30, 50);
            public static Color Accent_color = Color.FromArgb(0, 202, 78);
            public static Color Element_dark = Color.FromArgb(60, 60, 60);
            public static Color Text = Color.White;
        }

        private static readonly int screenX = Driver.screenX, screenY = Driver.screenY;
        private static readonly G_lib draw = new G_lib();
        private static readonly Cursor cursor = new Cursor();
        public static List<GuiWindow> Windows = new List<GuiWindow>();
        public static BaseGuiElement ActiveElement = null;

        public static void Start()
        {
            var testWindow = new GuiWindow(screenX / 5, screenY / 5, screenX / 2, screenY / 2, "App menu");

            //test power commands
            testWindow.AddChild(new GuiDiv(30, 54, 70, 144, UIcolors.Accent_color));
            testWindow.AddChild(new GuiButton(35, 64, 95, 30, "Reboot", UIcolors.Text, UIcolors.Accent_color, (self) =>
            {
                Cosmos.System.Power.Reboot();
            }));
            testWindow.AddChild(new GuiButton(135, 64, 95, 30, "Shut down", UIcolors.Text, UIcolors.Accent_color, (self => { Cosmos.System.Power.Shutdown(); })));

            //Text box
            testWindow.AddChild(new GuiText(32, 118, "Enter into textbox"));
            testWindow.AddChild(new GuiTextBox(32, 138, 150));

            //GuiToggle switches
            testWindow.AddChild(new GuiText(30, 162, "Wi-Fi"));
            testWindow.AddChild(new GuiSwitch(90, 160, UIcolors.Element_dark, UIcolors.Accent_color, true, (self) =>
            {
                if (self.status) { self.status = false; }
                if (!self.status) { self.status = true; }
            }));
            testWindow.AddChild(new GuiText(30, 190, "radio button!"));
            testWindow.AddChild(new GuiToggle(150, 190, UIcolors.Accent_color, UIcolors.Element_dark, true, (self) =>
            {
                if (self.enable)
                {
                    self.enable = false;
                }
                else if (!self.enable)
                {
                    self.enable = true;
                }
            }));

            Windows.Add(testWindow);

            int clickX = -100, clickY = -100;
            bool clickDown = false;

            Driver.Clear(UIcolors.Desktop_main);
            while (Kernel.canvasRunning)
            {
                draw.Box(UIcolors.Element_dark, 0, screenY - 30, screenX, screenY / 24);
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