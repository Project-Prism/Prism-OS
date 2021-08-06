namespace PrismProject.System2
{
    internal class Power
    {
        public static void Shutdown()
        {
            Cosmos.System.Power.Shutdown();
        }

        public static void Reboot()
        {
            Cosmos.System.Power.Reboot();
        }
    }
}