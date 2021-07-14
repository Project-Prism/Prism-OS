using System;
using System.Drawing;
using System.Collections.Generic;

namespace PrismProject
{
    class Desktop
    {
        //Default theme colors
        public static Color Title_bar = Color.FromArgb(0,120,212);
        public static Color Title_text = Color.White;
        public static Color Window_main = Color.White;
        public static Color Task_bar = Color.FromArgb(0,120,212);
        public static Color Button = Color.DarkCyan;
        public static Color Background = Color.FromArgb(100, 30, 35, 40);
        public static Color Text = Color.White;

        //Define graphics variables
        private static int screenX = Driver.screenX, screenY = Driver.screenY;
        private static drawable draw = new drawable();
        private static Cursor cursor = new Cursor();
        public static List<GuiWindow> Windows = new List<GuiWindow>();
        public static BaseGuiElement ActiveElement = null;

        public static void Start()
        {
            var testWindow = new GuiWindow("App menu", screenX / 4, screenY / 4, screenX / 2, screenX / 2);
            testWindow.AddChild(new GuiButton("Get random color", (self) => { 
                self.Background = Color.FromArgb(Driver.randomcolor); self.Value = "Yay im a color!"; self.TextColour = Color.FromArgb(Driver.randomcolor); }, true, 8, 32, 170, 20));
            testWindow.AddChild(new GuiButton("Click to reboot", (self) => { 
                Cosmos.System.Power.Reboot(); }, true, 8, 64, 170, 20));
            testWindow.AddChild(new GuiButton("Change window bar", (self) => { 
                Title_bar = Color.Black; }, true, 8, 96, 170, 20));
            testWindow.AddChild(new GuiButton("Open system info", (self) => {
            var Infoscreen = new GuiWindow("System infomation", 8, 8, screenX / 2, screenY / 3);
                Infoscreen.AddChild(new GuiText("CPU name: " + Cosmos.Core.CPU.GetCPUBrandString(), 8, 32));
                Infoscreen.AddChild(new GuiText("CPU vendor: " + Cosmos.Core.CPU.GetCPUVendorName(), 8, 64));
                Infoscreen.AddChild(new GuiText("Build/codename: Prism OS " + Kernel.Codename + " (" + Kernel.Kernel_build + ")", 8, 96));
                Windows.Add(Infoscreen);
            }, true, 8, 128, 170, 20));
            testWindow.AddChild(new GuiButton("Install to drive", (self)=>{Installer.Start();}, true, 8, 160, 170, 20));
            Windows.Add(testWindow);


            int clickX = -100, clickY = -100;
            bool clickDown = false;
            draw.Clear(Background);

            while (Kernel.canvasRunning)
            {
                draw.Box(Task_bar, 0, screenY - 30, screenX, 30);
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
