using Cosmos.System.Graphics;

namespace PrismProject
{
    class PGUI
    {
        public static Canvas canvas;
        public static int screenX = canvas.Mode.Rows;
        public static int screenY = canvas.Mode.Columns;

        public static void boot_screen()
        {
            canvas = FullScreenCanvas.GetFullScreenCanvas();
            canvas.Display();
            Utils.Playjingle();
            canvas.Clear(System.Drawing.Color.White);
            canvas.DrawFilledRectangle(new Pen(System.Drawing.Color.DarkGray), 0, 0, screenX, screenY / 8);
            while (true)
            {
            }
        }

        public static void message()
        {
            //coming soon.
        }
    }
}
