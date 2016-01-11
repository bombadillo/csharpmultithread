namespace MultiThread.Classes
{
    using System;
    using Interfaces;

    public class WriteLoadsOfStringsToFile : IWriteLoadsOfStringsToFile
    {
        private readonly ILog Logger;
        private readonly IWriteFile FileWriter;
        private readonly IHandleDatabase DatabaseHandler;

        private const int NumLinesToWrite = 1000000;
        private int FileLimit = 500;
        private int SqlLimit = 100;

        public WriteLoadsOfStringsToFile(IWriteFile fileWriter, ILog logger, IHandleDatabase databaseHandler)
        {
            FileWriter = fileWriter;
            Logger = logger;
            DatabaseHandler = databaseHandler;
        }

        public void WriteDemStrings(string thingy, int threadNumber)
        {
            var stringToWrite = "";
            var stringToInsert = "INSERT ALL ";

            for (int i = 0; i < NumLinesToWrite; i++)
            {
                stringToWrite += string.Format("{0} {1}{2}", thingy, i, Environment.NewLine);

                stringToInsert = AddToInsertString(i, thingy, stringToInsert);

                if (i % FileLimit == 0 && i != 0)
                {                    
                    FileWriter.WriteStringToFileWaitIfLocked(stringToWrite, @"C:\temp\test.txt");                    
                    stringToWrite = "";
                }

                if (i % SqlLimit == 0 && i != 0)
                {                    
                    stringToInsert += " SELECT * FROM DUAL";
                    DatabaseHandler.NonQuery(stringToInsert);
                    stringToInsert = "INSERT ALL ";
                }

                if (i % 10000 == 0) Logger.Info(i + " inserts");
                
            }

            DatabaseHandler.Close();

            if (!string.IsNullOrEmpty(stringToWrite)) FileWriter.WriteStringToFileWaitIfLocked(stringToWrite, @"C:\temp\test.txt");

            Logger.Info(string.Format("Thread {0} done", threadNumber));
        }

        private string AddToInsertString(int i, string thingy, string stringToInsert)
        {
            stringToInsert += string.Format("INTO monkey VALUES(MONKEY_SEQ.NEXTVAL, '{0}', '{1}')", i, thingy);

            return stringToInsert;
        }
    }
}

