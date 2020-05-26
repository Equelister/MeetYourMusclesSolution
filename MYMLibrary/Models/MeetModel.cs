using System;
using System.Collections.Generic;
using System.Text;

namespace MYMLibrary
{
    public class MeetModel
    {
        public int ID { get; set; }
        public int IDUser { get; set; }
        public int IDTrainer { get; set; }
        public int IDPlace { get; set; }
        public DateTime DateAndHour { get; set; }
        public int Duration { get; set; }
        public int Accepted { get; set; }
        public int New { get; set; }


        public MeetModel()
        {

        }
        public MeetModel(int IDUserValue, int IDTrainerValue, int IDPlaceValue, DateTime DateAndHourValue, int DurationValue)
        {
            IDUser = IDUserValue;
            IDTrainer = IDTrainerValue;
            IDPlace = IDPlaceValue;
            DateAndHour = DateAndHourValue;
            Duration = DurationValue;
            Accepted = 0;
            New = 1;
        }
        public MeetModel(int IDValue, int IDUserValue, int IDTrainerValue, int IDPlaceValue, DateTime DateAndHourValue, int DurationValue, int AcceptedValue, int NewValue)
        {
            ID = IDValue;
            IDUser = IDUserValue;
            IDTrainer = IDTrainerValue;
            IDPlace = IDPlaceValue;
            DateAndHour = DateAndHourValue;
            Duration = DurationValue;
            Accepted = AcceptedValue;
            New = NewValue;
        }


        public int getID()
        {
            return this.ID;
        }
        public int getIDUser()
        {
            return this.IDUser;
        }

        public int getIDTrainer()
        {
            return this.IDTrainer;
        }

        public int getIDPlace()
        {
            return this.IDPlace;
        }

        public DateTime getDateAndHour()
        {
            return this.DateAndHour;
        }
        public String getDateAndHourStr()
        {
            return this.DateAndHour.ToString();
        }

        public int getDuration()
        {
            return this.Duration;
        }

        public int getAccepted()
        {
            return this.Accepted;
        }

        public int getNew()
        {
            return this.New;
        }

           

    }
}
