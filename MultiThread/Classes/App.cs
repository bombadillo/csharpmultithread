namespace MultiThread.Classes
{
    using Interfaces;
    using System.Threading;

    public class App : IApp
    {
        private readonly ILog Logger;        
        private readonly IWriteLoadsOfStringsToFile WriteLoadsOfStringsToFile;

        public App(ILog logger,
            IWriteLoadsOfStringsToFile writeLoadsOfStringsToFile)
        {
            Logger = logger;                        
            WriteLoadsOfStringsToFile = writeLoadsOfStringsToFile;
        }

        public void Run()
        {
            Logger.Info("App started");                       

            var threadOne = new Thread(() => WriteLoadsOfStringsToFile.WriteDemStrings("blah", 1));
            var threadTwo = new Thread(() => WriteLoadsOfStringsToFile.WriteDemStrings("munchkin", 2));

            threadOne.Start();
            threadTwo.Start();

            Logger.Info("App ended");
        }

    }
}

