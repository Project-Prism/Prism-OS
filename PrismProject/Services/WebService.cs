namespace PrismProject.Services
{
    class WebService
    {
        bool StopTask = false;
        public WebService(string RootDir, int Port)
        {
            StartServer(RootDir, Port);
            StopServer();
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
