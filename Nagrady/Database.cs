using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nagrady
{
    class Database
    {
        private static OleDbConnection connection;
        private static String connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= rewards.mdb";
        public static bool connect()
        {
            if (connection == null)
            {
                connection = new OleDbConnection(connectionString);
                try
                {
                    connection.Open();
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
        public static bool execute(String query)
        {
            if (Database.connect())
            {
                OleDbCommand command = new OleDbCommand(query, Database.connection);
                command.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }
        public static DataSet getList(String query)
        {
            DataSet result = new DataSet();
            if (Database.connect())
            {
                OleDbCommand command = new OleDbCommand(query, Database.connection);
                OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                adapter.Fill(result);
                return result;
            }
            else
            {
                return result;
            }
        }
        public static OleDbDataReader getReader(String query)
        {
            OleDbDataReader result;
            Database.connect();

            var comanda = new OleDbCommand(query, Database.connection);
            comanda.CommandType = CommandType.Text;
            result = comanda.ExecuteReader();
            return result;
           
        }
        public static object getScalar(String query)
        {
            object result;
            Database.connect();
            var comanda = new OleDbCommand(query, Database.connection);
            comanda.CommandType = CommandType.Text;
            result = comanda.ExecuteScalar();
            return result;
        }
    }
}
