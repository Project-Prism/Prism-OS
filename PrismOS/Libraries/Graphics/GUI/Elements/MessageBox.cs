using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismOS.Libraries.Graphics.GUI.Elements
{
    public static class MessageBox
    {
        public static void ShowMessage(string Title, string Contents, string Button, WindowManager Instance)
        {
            Window W = new()
            {
                X = (Kernel.Canvas.Width / 2) - 200 + (10 * (Instance.Count - 1)),
                Y = (Kernel.Canvas.Height / 2) - 75 + (10 * (Instance.Count - 1)),
                Width = 400,
                Height = 150,
                Radius = Kernel.WM.GlobalRadius,
                Text = Title,
                Elements = new()
                {
                    new Button()
                    {
                        X = 400 - 15,
                        Y = -15,
                        Width = 15,
                        Height = 15,
                        Radius = Kernel.WM.GlobalRadius,
                        Text = "X",
                        OnClick = (ref Element E, ref Window Parent) => { Instance.Remove(Parent); },
                    },
                    new Label()
                    {
                        X = 200,
                        Y = 75,
                        Center = true,
                        Text = Contents,
                        Color = Color.White,
                    },
                    new Button()
                    {
                        X = 360,
                        Y = 138,
                        Width = 40,
                        Height = 12,
                        Radius = Kernel.WM.GlobalRadius,
                        Text = Button,
                        OnClick = (ref Element E, ref Window Parent) => { Instance.Remove(Parent); },
                    },
                },
            };
            Instance.Add(W);
        }
    }
}