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

        public byte[] getImageBytes(String tableName, int id)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
                {
                    connection.Open();
                    OracleCommand cmd;
                    string sql = String.Format("Select image from {1} where id = {0}", id, tableName);

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
            }catch
            {

            }
            return null;
        }


        public bool sendImageToDB(String tableName, int id, string filename)
        {
            bool success = true;
            try
            {
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

                            string sql = "Update " + tableName + " set image = :BLOBFILE WHERE id = " + id;
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
            }
            catch
            {
                success = false;
            }
            return success;
        }





    }
}
