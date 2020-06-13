using MYMLibrary.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MYMLibrary.DataBaseConnections
{
    public class OracleSQLConnectorTrainerWindow : OracleSQLConnector
    {

        public bool updateMeetingStatus(MeetModel meeting)
        {
            bool result = true;
            try
            {
                using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
                {
                    try
                    {
                        connection.Open();
                        OracleCommand cmd;
                        string sql = String.Format("update meet_table set accepted = {1}, isnew = {2} where id = {0}", meeting.ID, meeting.Accepted, meeting.New);
                        cmd = new OracleCommand(sql, connection);
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        result = false;
                    }
                }
            }catch
            {
                result = false;
            }
            return result;
        }


        /// <summary>
        /// Sends object to Oracle DataBase for an update.
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        public bool updatedPlaceToDataBase(PlaceModel place, int currentlySelectedItemID)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
                {
                    connection.Open();
                    OracleCommand cmd;
                    string sql = String.Format(
                            "UPDATE place_table SET city = '{0}', postcode = '{1}', street = '{2}', description = '{3}' WHERE id = {4}",
                            place.getCity(), place.getPostCode(), place.getStreet(), place.getDescription(), currentlySelectedItemID);
                    cmd = new OracleCommand(sql, connection);
                    cmd.CommandType = CommandType.Text;

                    int rowsUpdated = cmd.ExecuteNonQuery();
                    if (rowsUpdated == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }catch
            {
                return false;
            }
        }


        /// <summary>
        /// Inserts object into Oracle DataBase and returns its ID
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        public int insertPlaceToDBReturnItsID(PlaceModel place)
        {
            int output = -1;
            try
            {
                using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
                {
                    connection.Open();
                    OracleCommand cmd = new OracleCommand();
                    cmd.CommandText = String.Format(
                                    "INSERT INTO place_table(city, postcode, street, description, trainer_table_id) VALUES('{0}', '{1}', '{2}', '{3}', {4}) RETURNING id INTO :id",
                                    place.getCity(), place.getPostCode(), place.getStreet(), place.getDescription(), GlobalClass.getTrainerID());
                    cmd.Connection = connection;

                    cmd.Parameters.Add(new OracleParameter
                    {
                        ParameterName = ":id",
                        OracleDbType = OracleDbType.Int32,
                        Direction = ParameterDirection.Output
                    });
                    cmd.ExecuteNonQuery();
                    String outputStr = cmd.Parameters[":id"].Value.ToString();
                    output = Convert.ToInt32(outputStr);
                }
            }catch
            {

            }
            return output;
        }

    }
}
