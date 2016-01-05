namespace MultiThread.Interfaces
{
    public interface IWriteFile
    {
        void WriteStringToFile(string data, string fileName);
        void WriteStringToFileWaitIfLocked(string data, string fileName);
    }
}
