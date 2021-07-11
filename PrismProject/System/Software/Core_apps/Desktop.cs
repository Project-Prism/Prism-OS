using System;
using System.Drawing;
using System.Collections.Generic;

namespace PrismProject
{
    class Desktop
    {
        //Default theme colors
        public static Color Appbar = Color.FromArgb(0,120,212);
        public static Color Window = Color.White;
        public static Color Windowbar = Color.FromArgb(0,120,212);
        public static Color Button = Color.LightGray;
        public static Color Background = Color.FromArgb(40,40,40);
        public static Color Text = Color.Black;
        public static Random rnd = new Random();

        //Define the graphics method

        private static int screenX = Driver.screenX, screenY = Driver.screenY;
        private static drawable draw = new drawable();
        private static Cursor cursor = new Cursor();

        // GUI Elements
        public static List<GuiWindow> Windows = new List<GuiWindow>();
        public static BaseGuiElement ActiveElement = null;

        public static void Start()
        {
            draw.Clear(Background);

            var testWindow = new GuiWindow("App menu", screenX / 4, screenY / 4, screenX / 2, screenX / 2);
            testWindow.AddChild(new GuiText("List of demo applications", 8, 8));
            testWindow.AddChild(new GuiButton("Color me blue!", (self) => { var newcolor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)); self.Background = newcolor; self.Value = "Yay im a color!"; self.TextColour = Color.FromArgb(newcolor.ToArgb() ^ 0xffffff); }, 8, 32, 150));
            testWindow.AddChild(new GuiButton("Click to reboot", (self) => { Cosmos.System.Power.Reboot(); }, 8, 64, 150));
            testWindow.AddChild(new GuiButton("Change window bar", (self) => { Windowbar = Color.Black; }, 8, 96, 150));
            testWindow.AddChild(new GuiButton("Click for an extra mouse!", (self) => { testWindow.AddChild(new GuiImage(Images.mouse, 158, 160, 16, 16, true, 16, 16)); }, 8, 160, 32));
            Windows.Add(testWindow);


            int clickX = -100, clickY = -100;
            bool clickDown = false;

            while (Kernel.canvasRunning)
            {
                draw.Box(Appbar, 0, screenY - 30, screenX, 30);
                draw.Circle(Button, 20, screenY - 15, 10);

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
                    Cosmos.System.KeyEvent key;
                    if (Cosmos.System.KeyboardManager.TryReadKey(out key))
                    {
                        ActiveElement.Key(key);
                    }
                }

                cursor.Update();
            }
        }

        public static void Start_rec()
        {
            Driver.Init();
            draw.Clear(Color.Red);
            int clickX = -100, clickY = -100;
            bool clickDown = false;
            while (Kernel.canvasRunning)
            {
                var testWindow = new GuiWindow("System crash", screenX / 4, screenY / 4, screenX / 2, screenX / 2);
                testWindow.AddChild(new GuiText("Unfortunatley, the system has crashed!", 8, 8));
                Windows.Add(testWindow);
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
            }
            
        }
    }
}
