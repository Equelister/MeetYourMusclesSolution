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
        private Boolean Accepted { get; set; }
        private Boolean New { get; set; }


        public MeetModel()
        {

        }

        public MeetModel(int IDValue, int IDUserValue, int IDTrainerValue, int IDPlaceValue, DateTime DateAndHourValue, int DurationValue, Boolean AcceptedValue, Boolean NewValue)
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

    }
}
