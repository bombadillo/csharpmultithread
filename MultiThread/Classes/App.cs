namespace MultiThread.Classes
{
    using System;
    using Interfaces;
    using System.Threading;
    public class App : IApp
    {
        private readonly ILog Logger;
        private readonly IWriteFile FileWriter;
        private readonly IHandleDatabaseConnection DatabaseConnectionHandler;

        public App(ILog logger, IWriteFile fileWriter, IHandleDatabaseConnection databaseConnectionHandler)
        {
            Logger = logger;
            FileWriter = fileWriter;
            DatabaseConnectionHandler = databaseConnectionHandler;
        }

        public void Run()
        {
            Logger.Info("App started");

            //DatabaseConnectionHandler.GetConnection();

            Thread tid1 = new Thread(new ThreadStart(WriteToFile));
            Thread tid2 = new Thread(new ThreadStart(WriteToFile2));

            tid1.Start();
            tid2.Start();
        }

        public void WriteToFile()
        {
            var stringToWrite = "";

            for (int i = 0; i < 1000000; i++)
            {
                stringToWrite += "blah " + i + Environment.NewLine;

                if (i % 500 == 0)
                {
                    FileWriter.WriteStringToFileWaitIfLocked(stringToWrite, @"C:\temp\test.txt");
                    stringToWrite = "";
                }
            }

            if (!string.IsNullOrEmpty(stringToWrite)) FileWriter.WriteStringToFileWaitIfLocked(stringToWrite, @"C:\temp\test.txt");

            Logger.Info("Thread 1 done");
        }

        public void WriteToFile2()
        {
            var stringToWrite = "";

            for (int i = 0; i < 1200000; i++)
            {
                stringToWrite += "munchkin " + i + Environment.NewLine;

                if (i % 500 == 0)
                {
                    FileWriter.WriteStringToFileWaitIfLocked(stringToWrite, @"C:\temp\test.txt");
                    stringToWrite = "";
                }                
            }

            if (!string.IsNullOrEmpty(stringToWrite)) FileWriter.WriteStringToFileWaitIfLocked(stringToWrite, @"C:\temp\test.txt");

            Logger.Info("Thread 2 done");
        }
    }
}

