namespace MultiThread.Classes
{
    using System.IO;
    using Interfaces;
    using System;

    public class FileWriter : IWriteFile
    {
        private readonly ILog Logger;

        public FileWriter(ILog logger)
        {
            Logger = logger;
        }

        public void WriteStringToFile(string data, string fileName)
        {
            using (var sw = new StreamWriter(fileName, true))
            {
                sw.Write(data);
            }
        }

        public void WriteStringToFileWaitIfLocked(string data, string fileName)
        {
            while (true)
            {
                try
                {
                    using (var sw = new StreamWriter(fileName, true))
                    {
                        sw.Write(data);
                        break;
                    }
                }
                catch (Exception)
                {
                    System.Threading.Thread.Sleep(50);
                }
            }
        }
    }
}
