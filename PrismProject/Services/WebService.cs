namespace PrismProject.Services
{
    class WebService
    {
        bool StopTask = false;
        public WebService(string RootDir, int Port)
        {
            StartServer(RootDir, Port);
        }

        void StartServer(string aRootDir, int aPort)
        {

        }
        void StopServer()
        {
            StopTask = true;
        }
    }
}
