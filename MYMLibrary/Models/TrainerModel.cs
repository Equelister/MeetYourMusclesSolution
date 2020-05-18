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

        public void setID(int newID)
        {
            this.ID = newID;
        }

        public void setFirstName(String newFirstName)
        {
            this.FirstName = newFirstName;
        }

        public void setLastName (String newLastName)
        {
            this.LastName = newLastName;
        }

        public void setEmailAddress(String newEmailAddress)
        {
            this.EmailAddress = newEmailAddress;
        }

        public void setPhoneNumber(String newPhoneNumber)
        {
            this.PhoneNumber = newPhoneNumber;
        }


        public void setPricePerhour(int newPricePerHour)
        {
            this.PricePerHour = newPricePerHour;
        }






    }
}
