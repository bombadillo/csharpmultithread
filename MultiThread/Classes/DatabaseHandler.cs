namespace MultiThread.Classes
{
    using Oracle.ManagedDataAccess.Client;
    using Interfaces;
    using System.Data;
    using System;
    using Ninject;
    using System.Reflection;
    public class DatabaseHandler : IHandleDatabase
    {
        private readonly ILog Logger;
        private readonly IHandleDatabaseConnection DatabaseConnectionHandler;

        private OracleConnection Con;

        private IKernel Kernel;

        public DatabaseHandler(ILog logger, IHandleDatabaseConnection databaseConnectionHandler)
        {
            Logger = logger;
            DatabaseConnectionHandler = databaseConnectionHandler;
        }

        public void NonQuery(string sql)
        {

            GetConnection();            
            //DatabaseConnectionHandler.GetConnection();
            

            var cmd = new OracleCommand();
            cmd.Connection = Con;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;

            try
            {
                var dr = cmd.ExecuteNonQuery();
            }
            catch (OracleException e)
            {
                Logger.Error(e.Message);
                throw;
            }             
        }

        private void GetConnection()
        {
            Kernel = new StandardKernel();
            Kernel.Load(Assembly.GetExecutingAssembly());

            if (Con == null)
            {
                var con = Kernel.Get<IHandleDatabaseConnection>();
                con.GetConnection();
                Con = con.Con;
            }


        }

        public void NonQuery(string sql, string[] arguments)
        {
            DatabaseConnectionHandler.GetConnection();



            var cmd = new OracleCommand();
            cmd.Connection = DatabaseConnectionHandler.Con;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;



            //foreach (var argument in arguments)
            //{
            //    var split = argument.Split(',');

            //    for (var i = 0; i < split.Length; i++)
            //    {
            //        var parameterValue = split[i];
            //        var parameter = new OracleParameter("Argument" + i, parameterValue);
            //        cmd.Parameters.Add(parameter);
            //    }                
            //}

            try
            {
                var dr = cmd.ExecuteNonQuery();
            }
            catch (OracleException e)
            {
                Logger.Error(e.Message);
                throw;
            }
        }

        public void Query(string sql)
        {
            DatabaseConnectionHandler.GetConnection();

            var cmd = new OracleCommand();
            cmd.Connection = DatabaseConnectionHandler.Con;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;

            var dr = cmd.ExecuteReader();
            dr.Read();            
        }

        public void Close()
        {
            DatabaseConnectionHandler.Con.Dispose();
        }
    }
}
