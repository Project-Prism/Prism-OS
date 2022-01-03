namespace MkUI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }

        public enum Elements : byte
        {
            Button,
        }
        public enum Vars : byte
        {
            Width,
            Height,
            Colors,
        }
        public enum Colors : byte
        {
            Foreground,
            Background,
            Text,
        }

        public static void Convert(string[] Lines)
        {
            List<byte> Bytes = new();

            foreach (string Line in Lines)
            {
                string Type = Line.Split("<=>")[0];
                string[] Args = Line.Split("<=>")[1].Split(", ");
                switch (Type)
                {
                    case "button":
                        Bytes.Add((byte)Elements.Button);
                        Bytes.Add((byte)Line.Split("<=>")[1].Replace(", ", "").Length);
                        Bytes.Add((byte)Vars.Width);
                        Bytes.Add((byte)int.Parse(Args[0]));
                        Bytes.Add((byte)Vars.Height);
                        Bytes.Add((byte)int.Parse(Args[1]));
                        Bytes.Add((byte)Vars.Colors);
                        Bytes.Add((byte)Args[2].Split(">").Length);
                        Bytes.Add((byte)int.Parse(Args[2]));
                        break;
                }
            }
        }
    }
}