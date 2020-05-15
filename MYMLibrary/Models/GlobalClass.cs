using System;
using System.Collections.Generic;
using System.Text;

namespace MYMLibrary.Models
{
    public static class GlobalClass
    {
        private static int trainerID;
        private static int userID;
        private static int placeID;

        public static int getTrainerID()
        {
            return trainerID;
        }

        public static int getUserID()
        {
            return userID;
        }

        public static int getPlaceID()
        {
            return placeID;
        }

        public static void setTrainerID(int newTrainerID)
        {
            trainerID = newTrainerID;
        }

        public static void setUserID(int newUserID)
        {
            userID = newUserID;
        }

        public static void setPlaceID(int newPlaceID)
        {
            placeID = newPlaceID;
        }






    }
}
