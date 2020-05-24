using System;
using System.Collections.Generic;
using System.Text;

namespace MYMLibrary
{
    public class TrainerModel : PersonModel
    {
        private int PricePerHour { get; set; }

        public TrainerModel() : base()
        {

        }

        public TrainerModel(int IDValue, String FirstNameValue, String LastNameValue, String EmailAddressValue, String PhoneNumberValue, int PricePerHourValue) : base(IDValue, FirstNameValue, LastNameValue, EmailAddressValue, PhoneNumberValue)
        {
            PricePerHour = PricePerHourValue;
        }

        public TrainerModel(String FirstNameValue, String LastNameValue, String EmailAddressValue, String PhoneNumberValue, int PricePerHourValue) : base(FirstNameValue, LastNameValue, EmailAddressValue, PhoneNumberValue)
        {
            PricePerHour = PricePerHourValue;
        }

        public TrainerModel(String FirstNameValue, String LastNameValue, String EmailAddressValue, String PhoneNumberValue) : base(FirstNameValue, LastNameValue, EmailAddressValue, PhoneNumberValue)
        {

        }



        public void setPricePerhour(int newPricePerHour)
        {
            this.PricePerHour = newPricePerHour;
        }

        public int getPricePerHour()
        {
            return this.PricePerHour;
        }





    }
}
