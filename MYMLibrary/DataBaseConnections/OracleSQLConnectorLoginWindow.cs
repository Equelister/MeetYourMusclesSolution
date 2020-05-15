using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MYMLibrary.DataBaseConnections
{
    public class OracleSQLConnectorLoginWindow : OracleSQLConnector
    {




        public int getIDFromDataBase(String tableName, String searchedValue)
        {
            using (OracleConnection connection = new OracleConnection(OracleSQLConnectorLoginWindow.GetConnectionString()))
            {
                connection.Open();
                OracleCommand cmd;

                string sql = string.Format("select id from {0} WHERE email LIKE '{1}'", tableName, searchedValue);

                cmd = new OracleCommand(sql, connection);
                cmd.CommandType = CommandType.Text;

                OracleDataReader reader = cmd.ExecuteReader();
                try
                {
                    if(reader.Read())
                    {
                        int result = (reader.GetInt32(0));
                        reader.Close();
                        return result;
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return -1;
        }

        public static String getColumnFromDataBaseForID(String columnName, String tableName, int personID)
        {
            String password;
            using (OracleConnection connection = new OracleConnection(OracleSQLConnectorLoginWindow.GetConnectionString()))
            {
                connection.Open();
                OracleCommand cmd;

                string sql = String.Format("select {0} from {1} WHERE id = {2}", columnName, tableName, personID);

                cmd = new OracleCommand(sql, connection);
                cmd.CommandType = CommandType.Text;

                OracleDataReader reader = cmd.ExecuteReader();
                try
                {
                    if (reader.Read())
                    {
                        password = reader.GetString(0);
                    }
                    else
                    {
                        password = null;
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return password;
        }
    }
}
