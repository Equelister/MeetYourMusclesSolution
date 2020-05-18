using System;
using System.Collections.Generic;
using System.Text;

namespace MYMLibrary
{
    public abstract class PersonModel
    {
        protected int ID { get; set; }
        protected String FirstName { get; set; }
        protected String LastName { get; set; }
        protected String EmailAddress { get; set; }
        protected String PhoneNumber { get; set; }


        protected PersonModel()
        {

        }

        protected PersonModel(int IDValue, String FirstNameValue, String LastNameValue, String EmailAddressValue, String PhoneNumberValue)
        {
            ID = IDValue;
            FirstName = FirstNameValue;
            LastName = LastNameValue;
            EmailAddress = EmailAddressValue;
            PhoneNumber = PhoneNumberValue;
        }

        protected PersonModel(String FirstNameValue, String LastNameValue, String EmailAddressValue, String PhoneNumberValue)
        {
            FirstName = FirstNameValue;
            LastName = LastNameValue;
            EmailAddress = EmailAddressValue;
            PhoneNumber = PhoneNumberValue;
        }

        public int getID()
        {
            return this.ID;
        }
        public String getFirstName()
        {
            return this.FirstName;
        }

        public String getLastName()
        {
            return this.LastName;
        }

        public String getEmailAddress()
        {
            return this.EmailAddress;
        }

        public String getPhoneNumberStr()
        {
            return this.PhoneNumber;
        }


    }
}
