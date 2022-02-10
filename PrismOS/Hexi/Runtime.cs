using System.Collections.Generic;

namespace PrismOS.Hexi
{
    public static class Runtime
    {
        public static List<Core.Process> Executables { get; set; } = new();

        public static void RunProgram(string PathToFile)
        {
            Executables.Add(new(PathToFile));
        }

        public static void StopProgram(Core.Process Process)
        {
            Executables[Executables.IndexOf(Process)].Dispose();
            Executables.Remove(Process);
        }

        public static void Tick()
        {
            foreach (Core.Process exe in Executables)
                exe.Tick();
        }
    }
}