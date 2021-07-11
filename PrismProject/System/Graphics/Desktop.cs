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
        public static Color Text = Color.White;

        //Define the graphics method

        private static int screenX = Driver.screenX, screenY = Driver.screenY;
        private static Elements draw = new Elements();
        private static Cursor cursor = new Cursor();

        // GUI Elements
        public static List<GuiWindow> Windows = new List<GuiWindow>();
        public static BaseGuiElement ActiveElement = null;

        public static void Start()
        {
            Driver.Init();
            draw.Clear(Background);

            var testWindow = new GuiWindow("Test Window", screenX / 4, screenY / 4, screenX / 2, screenX / 2);
            testWindow.AddChild(new GuiText("Hello, world!", 8, 8));
            testWindow.AddChild(new GuiButton("Click me!", (self) => { self.Value = "Clicked!"; }, 8, 32, 96));
            testWindow.AddChild(new GuiText("Textbox:", 8, 128));
            testWindow.AddChild(new GuiTextBox(96, 128));
            Windows.Add(testWindow);


            int clickX = -100, clickY = -100;
            bool clickDown = false;

            while (Kernel.canvasRunning)
            {
                if (Memory.Free < 100)
                {
                    Memory.OutOfMemoryWarning();
                    Cosmos.Core.Bootstrap.CPU.Halt();
                }
                draw.Box(Appbar, 0, screenY - 30, screenX, 30);
                draw.Circle(Button, 20, screenY - 15, 10);
                draw.Text(Text, Driver.font, "Used memory: " + Memory.Used + " MB (" + Memory.Used_percent + "%)", 0, 0);
                draw.Text(Text, Driver.font, "Total memory: " + Memory.Total + " MB", 0, 15);
                draw.Text(Text, Driver.font, "Free memory: " + Memory.Free + " MB (" + Memory.Free_percent + "%)", 0, 30);

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
            while (Kernel.canvasRunning)
            {
                draw.Window(Driver.font, screenX / 4, screenY / 4, screenX / 2, screenX / 2, "SYSTEM FAILURE");
                draw.Text(Text, Driver.font, "System crashed! Unrecoverable error occured.", screenX/4+75, screenY/4+75);
                cursor.Update();
            }
        }
    }
}
