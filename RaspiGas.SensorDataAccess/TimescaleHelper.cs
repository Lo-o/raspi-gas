using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspiGas.SensorDataAccess
{
    /// <summary>
    /// Timescale-provided example Helper class
    /// This class contains all of the methods needed to complete the
    /// quick-start, providing a sample of each database operation in total
    /// to refer to later.
    /// </summary>
    public class TimescaleHelper
    {
        private static string Host = "";
        private static string User = "";
        private static string DBname = "";
        private static string Password = "";
        private static string Port = "";
        private static string conn_str = "";

        public TimescaleHelper(string host = "localhost", string user = "postgres",
            string dbname = "postgres", string password = "password", string port = "5432")
        {
            Host = host;
            User = user;
            DBname = dbname;
            Password = password;
            Port = port;

            // Build connection string using the parameters above
            conn_str = $"Server={Host};Username={User};Database={DBname};Port={Port};Password={Password};SSLMode=Prefer";
        }

        /// <summary>
        /// Procedure - Connecting .NET to TimescaleDB:
        /// Check the connection TimescaleDB and verify that the extension
        /// is installed in this database
        /// </summary>
        public void CheckDatabaseConnection()
        {
            // get one connection for all SQL commands below
            var sql = "SELECT default_version, comment FROM pg_available_extensions WHERE name = 'timescaledb';";
            
            using var timescaleConnection = getConnection();
            using var cmd = new NpgsqlCommand(sql, timescaleConnection);
            using var rdr = cmd.ExecuteReader();

            if (!rdr.HasRows)
            {
                Console.WriteLine("Missing TimescaleDB extension!");
                timescaleConnection.Close();
                return;
            }

            while (rdr.Read())
            {
                Console.WriteLine("TimescaleDB Default Version: {0}\n{1}", rdr.GetString(0), rdr.GetString(1));
            }
            timescaleConnection.Close();

        }

        private NpgsqlConnection getConnection()
        {
            var Connection = new NpgsqlConnection(conn_str);
            Connection.Open();
            return Connection;
        }
    }
}
