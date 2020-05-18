﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MYMLibrary.Models;
using Oracle.ManagedDataAccess.Client;

namespace MYMLibrary
{
    public class OracleSQLConnector
    {
        public static String GetConnectionString()
        {
            return "User Id=projektcsoracle; Password=haslo2020; Data Source=localhost:1521/xe";
        }


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
    }
}
