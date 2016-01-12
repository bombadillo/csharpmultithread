namespace MultiThread.Classes
{
    using Oracle.ManagedDataAccess.Client;
    using Interfaces;
    using System.Data;

    public class DatabaseHandler : IHandleDatabase
    {
        private readonly ILog Logger;
        private readonly IFactoryGeneric<IHandleDatabaseConnection> FactoryGeneric;

        private OracleConnection Con;

        public DatabaseHandler(ILog logger, IFactoryGeneric<IHandleDatabaseConnection> factoryGeneric)
        {
            Logger = logger;
            FactoryGeneric = factoryGeneric;
        }

        public void NonQuery(string sql)
        {

            GetConnection();                        

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
            if (Con == null)
            {
                var con = FactoryGeneric.Create();
                con.GetConnection();
                Con = con.Con;
            }


        }

        public void NonQuery(string sql, string[] arguments)
        {
            GetConnection();

            var cmd = new OracleCommand();
            cmd.Connection = Con;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            
            foreach (var argument in arguments)
            {
                var split = argument.Split(',');

                for (var i = 0; i < split.Length; i++)
                {
                    var parameterValue = split[i];
                    var parameter = new OracleParameter("Argument" + i, parameterValue);
                    cmd.Parameters.Add(parameter);
                }
            }

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
            GetConnection();

            var cmd = new OracleCommand();
            cmd.Connection = Con;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;

            var dr = cmd.ExecuteReader();
            dr.Read();            
        }

        public void Close()
        {
            Con.Dispose();
        }
    }
}
