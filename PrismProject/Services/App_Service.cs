using Cosmos.System.Graphics;
using System.Collections.Generic;

namespace PrismProject.Functions.Services
{
    class App_Service
    {
        public static Dictionary<string, App> AppList = new Dictionary<string, App>();
        public struct App
        {
            public string Title;
            public Bitmap Icon;
            public string Script; // placeholder for when hexi is implemented
            public bool Maximized;
            public bool Visible;

            public App(string Name, Bitmap Ico, string scr)
            {
                Title = Name;
                Icon = Ico;
                Script = scr;
                Maximized = true;
                Visible = true;
            }
        }

        public static void AddApp(string name, Bitmap Icon, string MainScript)
        {

        }

        public static void RemoveApp(string name)
        {

        }
    }
}
