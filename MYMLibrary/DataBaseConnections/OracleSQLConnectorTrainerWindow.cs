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
            using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    OracleCommand cmd;
                    string sql = String.Format("update meet_table set accepted = {1}, isnew = {2} where id = {0}", meeting.getID(), meeting.getAccepted(), meeting.getNew());
                    cmd = new OracleCommand(sql, connection);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    result = false;
                }
            }
            return result;
        }





    }
}
