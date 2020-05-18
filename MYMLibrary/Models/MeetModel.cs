using System;
using System.Collections.Generic;
using System.Text;

namespace MYMLibrary
{
    public class MeetModel
    {
        private int ID { get; set; }
        private int IDUser { get; set; }
        private int IDTrainer { get; set; }
        private int IDPlace { get; set; }
        private DateTime DateAndHour { get; set; }
        private int Duration { get; set; }
        private int Accepted { get; set; }
        private int New { get; set; }


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

        public String getDateAndHourStrOracleDateFormat()
        {
            return "..."; //TO_DATE('2015/05/15 8:30:25', 'YYYY/MM/DD HH:MI:SS')
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
