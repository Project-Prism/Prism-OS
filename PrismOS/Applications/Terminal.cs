using PrismOS.Libraries.Graphics.GUI;
using PrismOS.Libraries.Graphics;
using System.Collections.Generic;
using PrismOS.Libraries;

namespace PrismOS.Applications
{
    public class Terminal : Runtime.Application
    {
        public class Command
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Usage { get; set; }
            public string Permission { get; set; }
            public System.Action<object> Target { get; set; }
           }
        public List<Command> Commands;
        public Window Window = new();
        public Panel Panel1 = new();
        public Label Label1 = new();
        public string Input = "";

        public override void OnCreate()
        {
            Commands = new()
            {
                new()
                {
                    Name = "clear",
                    Description = "Clear the console",
                    Permission = "User",
                    Usage = "[ Empty ]",
                    Target = (object Arguments) => { Label1.Text += "> "; },
                },
                new()
                {
                    Name = "time",
                    Description = "Get the current time",
                    Permission = "User",
                    Usage = "[ Empty ]",
                    Target = (object Arguments) => { Label1.Text += System.DateTime.Now.ToString("M/d/yyyy, h:mm t UTC+z") + "\n"; }
                },
                new()
                {
                  Name = "echo",
                  Description = "Echo some text back",
                  Permission = "User",
                  Usage = "[ Empty ]",
                  Target = (object Args) => { Label1.Text += Args + "\n"; },
                },
            };

            // Main window
            Window.X = 256;
            Window.Y = 256;
            Window.Width = 400;
            Window.Height = 175;
            Window.Radius = 0;
            Window.Text = "Terminal (.cs)";

            // Main panel
            Panel1.X = 0;
            Panel1.Y = 0;
            Panel1.Width = Window.Width;
            Panel1.Height = Window.Height;
            Panel1.Radius = 0;
            Panel1.Color = new(255, 30, 30, 30);
            Window.Elements.Add(Panel1);

            // Main text
            Label1.X = 0;
            Label1.Y = 0;
            Label1.Text = "> ";
            Label1.Color = Color.White;
            Window.Elements.Add(Label1);

            Runtime.Windows.Add(Window);
        }

        public override void OnDestroy()
        {
        }

        public override void OnUpdate()
        {
            string[] Temp = Label1.Text.Split("\n");
            if ((Temp.Length * Canvas.Font.Default.Height) >= Window.Height)
            {
                for (int I = 1; I < Temp.Length; I++)
                {
                    Label1.Text = Temp[I] + "\n";
                }
                Label1.Text = Label1.Text[0..(Label1.Text.Length - 1)];
            }
            for (int I = 0; I < Temp.Length; I++)
            {
                if (Temp[I].Length * Canvas.Font.Default.Width >= Window.Width)
                {
                    Temp[I] = Temp[I][0..(Temp[I].Length * Canvas.Font.Default.Width)];
                }
            }
            if (Cosmos.System.KeyboardManager.TryReadKey(out var Key))
            {
                switch (Key.Key)
                {
                    case Cosmos.System.ConsoleKeyEx.Enter:
                        Label1.Text += "\n";
                        if (Input.Length == 0)
                        {
                            Label1.Text += "> ";
                            return;
                        }
                        foreach (Command C in Commands)
                        {
                            if (C.Name == Input)
                            {
                                C.Target.Invoke(Input[(C.Name.Length + 1)..Input.Length]);
                                Label1.Text += "> ";
                                Input = "";
                                return;
                            }
                        }
                        Input = "";
                        Label1.Text += "Command not found!\n> ";
                        break;
                    case Cosmos.System.ConsoleKeyEx.Backspace:
                        if (Input.Length != 0)
                        {
                            Label1.Text = Label1.Text[0..(Label1.Text.Length - 1)];
                            Input = Input[0..(Input.Length - 1)];
                        }
                        break;
                    default:
                        Label1.Text += Key.KeyChar;
                        Input += Key.KeyChar;
                        break;
                }
            }
        }
    }
}