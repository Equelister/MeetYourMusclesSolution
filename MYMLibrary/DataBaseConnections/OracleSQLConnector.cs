using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MYMLibrary.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace MYMLibrary
{
    public class OracleSQLConnector
    {
        public static String GetConnectionString()
        {
            return "User Id=projektcsoracle; Password=haslo2020; Data Source=localhost:1521/xe";
        }

        /// <summary>
        /// Loads all places for currently logged in traienr
        /// </summary>
        /// <returns></returns>
        public List<PlaceModel> loadPlacesFromDataBase()
        {
            List<PlaceModel> placesList = new List<PlaceModel>();
            using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
            {
                connection.Open();
                OracleCommand cmd;
                string sql = String.Format("select * from place_table WHERE trainer_table_id = {0}", GlobalClass.getTrainerID());

                cmd = new OracleCommand(sql, connection);
                cmd.CommandType = CommandType.Text;

                OracleDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        PlaceModel place = new PlaceModel();
                        place.setID(reader.GetInt32(0));
                        place.setCity(reader.GetString(1));
                        place.setPostCode(reader.GetString(2));
                        place.setStreet(reader.GetString(3));
                        place.setDescription(reader.GetString(4));
                        placesList.Add(place);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return placesList;
        }

        /// <summary>
        /// Loads current logged in User Data
        /// </summary>
        /// <returns></returns>
        public UserModel loadUserFromDataBase()
        {
            UserModel user = new UserModel();
            using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
            {
                connection.Open();
                OracleCommand cmd;
                string sql = String.Format("select * from user_table WHERE id = {0}", GlobalClass.getUserID());

                cmd = new OracleCommand(sql, connection);
                cmd.CommandType = CommandType.Text;

                OracleDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        user = new UserModel(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return user;
        }

        public PlaceModel loadPlaceFromDataBase(int placeID)
        {
            PlaceModel place = new PlaceModel();
            using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
            {
                connection.Open();
                OracleCommand cmd;
                string sql = String.Format("select * from place_table WHERE id = {0}", placeID);

                cmd = new OracleCommand(sql, connection);
                cmd.CommandType = CommandType.Text;

                OracleDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        place.setID(reader.GetInt32(0));
                        place.setCity(reader.GetString(1));
                        place.setPostCode(reader.GetString(2));
                        place.setStreet(reader.GetString(3));
                        place.setDescription(reader.GetString(4));
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return place;
        }

        /// <summary>
        /// Load currently logged in Trainer Data
        /// </summary>
        /// <returns></returns>
        public TrainerModel loadTrainerFromDataBase()
        {
            TrainerModel trainer = new TrainerModel();
            using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
            {
                connection.Open();
                OracleCommand cmd;
                string sql = String.Format("select * from trainer_table WHERE id = {0}", GlobalClass.getTrainerID());

                cmd = new OracleCommand(sql, connection);
                cmd.CommandType = CommandType.Text;

                OracleDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        trainer = new TrainerModel(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                        try
                        {
                            trainer.setPricePerhour(reader.GetInt32(5));
                        }catch
                        {
                            trainer.setPricePerhour(0);
                        }
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return trainer;
        }

        public TrainerModel loadTrainerFromDataBase(int trainerID)
        {
            TrainerModel trainer = new TrainerModel();
            using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
            {
                connection.Open();
                OracleCommand cmd;
                string sql = String.Format("select * from trainer_table WHERE id = {0}", trainerID);

                cmd = new OracleCommand(sql, connection);
                cmd.CommandType = CommandType.Text;

                OracleDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        trainer = new TrainerModel(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                        try
                        {
                            trainer.setPricePerhour(reader.GetInt32(5));
                        }
                        catch
                        {
                            trainer.setPricePerhour(0);
                        }
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return trainer;
        }


        public List<MeetModel> loadAllMeetingsFromDataBase(int ID, String tableName)
        {
            List<MeetModel> meetingList = new List<MeetModel>();
            using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
            {
                connection.Open();
                OracleCommand cmd;
                string sql = String.Format("select * from meet_table WHERE {1}_table_id = {0}", ID, tableName);

                cmd = new OracleCommand(sql, connection);
                cmd.CommandType = CommandType.Text;

                OracleDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        MeetModel meeting = new MeetModel(
                            reader.GetInt32(0),
                            reader.GetInt32(5),
                            reader.GetInt32(6),
                            reader.GetInt32(7),
                            reader.GetDateTime(1),
                            reader.GetInt32(2),
                            reader.GetInt32(3),
                            reader.GetInt32(4));
                        if (meeting.DateAndHour > DateTime.Now)
                            meetingList.Add(meeting);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return meetingList;
        }
    }
}
