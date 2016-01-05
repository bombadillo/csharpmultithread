namespace MultiThread.Interfaces
{
    using Oracle.ManagedDataAccess.Client;

    public interface IHandleDatabaseConnection
    {
        void GetConnection();
        OracleConnection Con { get; }
    }
}
