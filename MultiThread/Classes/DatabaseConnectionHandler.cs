namespace MultiThread.Classes
{
    using System;
    using Interfaces;
    using Oracle.ManagedDataAccess.Client;
    using System.Configuration;

    public class DatabaseConnectionHandler : IHandleDatabaseConnection
    {
        private readonly ILog Logger;

        public OracleConnection Con { get; set; }

        private readonly string OracleConnectionString = ConfigurationManager.AppSettings["OracleConnection"];

        public DatabaseConnectionHandler(ILog logger)
        {
            Logger = logger;
        }

        public void GetConnection()
        {

            if (Con != null) return;

            Con = new OracleConnection
            {
                ConnectionString = OracleConnectionString
            };

            try
            {
                Con.Open();
            }
            catch (Exception)
            {
                Logger.Error("Unable to open Oracle connection.");
            }
        }


        public void CloseConnection()
        {
            Con.Dispose();
        }
    }
}
