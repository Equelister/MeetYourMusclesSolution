﻿using Oracle.ManagedDataAccess.Client;
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
                        meet.getDateAndHour(), meet.getDuration(), meet.getAccepted(), meet.getNew(), meet.getIDUser(), meet.getIDTrainer(), meet.getIDPlace());

                    cmd = new OracleCommand(sql, connection);
                    cmd.CommandType = CommandType.Text;

                    cmd.ExecuteNonQuery();
                }
                catch
                {
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
                //testLabel.Content = "Connected to Oracle" + connection.ServerVersion + connection.DatabaseName;

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
