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

            Thread tid1 = new Thread(() => WriteLoadsOfStringsToFile.WriteDemStrings("blah", 1));
            Thread tid2 = new Thread(() => WriteLoadsOfStringsToFile.WriteDemStrings("munchkin", 2));

            tid1.Start();
            tid2.Start();
        }

    }
}

