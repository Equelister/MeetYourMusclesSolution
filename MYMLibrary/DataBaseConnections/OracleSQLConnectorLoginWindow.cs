using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace MYMLibrary.DataBaseConnections
{
    public class OracleSQLConnectorLoginWindow : OracleSQLConnector
    {

        public int getIDFromDataBase(String tableName, String searchedValue)
        {
            try
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
                        if (reader.Read())
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
            }
            catch
            {
                return -1;
            }
            return -1;
        }

        public PersonModel getAllFromDataBaseForEmail(String tableName, String email, out int personTableID)
        {
            PersonModel person = new PersonModel();
            personTableID = -1;
            try
            {
                using (OracleConnection connection = new OracleConnection(OracleSQLConnectorLoginWindow.GetConnectionString()))
                {
                    connection.Open();
                    OracleCommand cmd;

                    string sql = String.Format("select * from {0} WHERE email LIKE '{1}'", tableName, email);

                    cmd = new OracleCommand(sql, connection);
                    cmd.CommandType = CommandType.Text;

                    OracleDataReader reader = cmd.ExecuteReader();
                    try
                    {
                        if (reader.Read())
                        {
                            person.setID(reader.GetInt32(0));
                            person.setPassword(reader.GetString(1));
                            personTableID = reader.GetInt32(2);
                            person.setEmailAddress(reader.GetString(3));

                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
            }catch
            {

            }
            return person;
        }
    }
}
