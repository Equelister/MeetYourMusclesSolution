using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace MYMLibrary.DataBaseConnections
{
    public class OracleSQLConnectorImages : OracleSQLConnector
    {

        public byte[] getImageBytes(int id)
        {
            using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
            {
                connection.Open();
                OracleCommand cmd;
                string sql = String.Format("Select image from user_table where id = {0}", id);

                cmd = new OracleCommand(sql, connection);
                cmd.CommandType = CommandType.Text;

                OracleDataReader reader = cmd.ExecuteReader();
                try
                {
                    if (reader.Read())
                    {
                        if (reader["image"] != DBNull.Value)
                            return (byte[])reader["image"];
                        else
                            return null;
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return null;
        }


        public bool sendImageToDB(int id, string filename)
        {
            bool success = true;
            using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
            {
                try
                {
                    if (filename != "")
                    {
                        FileStream fls;
                        fls = new FileStream(@filename, FileMode.Open, FileAccess.Read);
                        byte[] blob = new byte[fls.Length];
                        fls.Read(blob, 0, System.Convert.ToInt32(fls.Length));
                        fls.Close();

                        string sql = "Update user_table set image = :BLOBFILE WHERE id = " + id; 
                        connection.Open();

                        using (OracleCommand cmd = new OracleCommand(sql, connection))
                        {
                            cmd.Parameters.Add("BLOBFILE", OracleDbType.Blob, blob, ParameterDirection.Input);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch
                {
                    success = false;
                }

            }
            return success;
        }





    }
}
