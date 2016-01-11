namespace MultiThread.Interfaces
{
    using Oracle.ManagedDataAccess.Client;

    public interface IHandleDatabase
    {
        void Query(string sql);
        void NonQuery(string sql);
        void NonQuery(string sql, string[] arguments);
        void Close();
    }
}
