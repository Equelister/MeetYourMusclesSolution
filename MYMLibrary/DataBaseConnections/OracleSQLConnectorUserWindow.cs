using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MYMLibrary
{
    public class OracleSQLConnectorUserWindow
    {

        public bool insertMeetingToDBReturnItsID(MeetModel meet)
        {
            using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    OracleCommand cmd = new OracleCommand();
                    String sql = String.Format("INSERT INTO meet_table (date_and_time, duration, accepted, isnew, user_table_id, trainer_table_id, place_table_id) VALUES (TO_DATE('{0}', 'DD/MM/YYYY HH24:MI:SS'), {1}, {2}, {3}, {4}, {5}, {6})",
                        meet.DateAndHour, meet.Duration, meet.Accepted, meet.New, meet.IDUser, meet.IDTrainer, meet.IDPlace);

                    cmd = new OracleCommand(sql, connection);
                    cmd.CommandType = CommandType.Text;

                    cmd.ExecuteNonQuery();
                    //Console.WriteLine("1213231");


                }
                catch (Exception e)
                {
                    // Extract some information from this exception, and then
                    // throw it to the parent method.
                    if (e.Source != null)
                        Console.WriteLine("IOException source: {0}", e.Source);
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
            return true;
        }



        public List<TrainerModel> loadAllTrainersFromDataBase()
        {
            List<TrainerModel> trainerList = new List<TrainerModel>();
            using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
            {
                connection.Open();
                OracleCommand cmd;
                string sql = String.Format("select * from trainer_table");

                cmd = new OracleCommand(sql, connection);
                cmd.CommandType = CommandType.Text;

                OracleDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        TrainerModel trainer = new TrainerModel();
                        trainer.setID(reader.GetInt32(0));
                        trainer.setFirstName(reader.GetString(1));
                        trainer.setLastName(reader.GetString(2));
                        trainer.setEmailAddress(reader.GetString(3));
                        trainer.setPhoneNumber(reader.GetString(4));
                        try
                        {
                            trainer.setPricePerhour(reader.GetInt32(5));
                        }
                        catch
                        {
                            trainer.setPricePerhour(0);
                        }
                        try
                        {
                            trainer.setImageBlob((byte[])reader["image"]);
                        }
                        catch
                        {
                            trainer.setImageBlob(null);
                        }
                        trainerList.Add(trainer);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return trainerList;
        }
    }
}
