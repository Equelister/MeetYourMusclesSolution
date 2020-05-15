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
        protected int PhoneNumber { get; set; }


        protected PersonModel()
        {

        }

        protected PersonModel(int IDValue, String FirstNameValue, String LastNameValue, String EmailAddressValue, int PhoneNumberValue)
        {
            ID = IDValue;
            FirstName = FirstNameValue;
            LastName = LastNameValue;
            EmailAddress = EmailAddressValue;
            PhoneNumber = PhoneNumberValue;
        }

        protected PersonModel(String FirstNameValue, String LastNameValue, String EmailAddressValue, int PhoneNumberValue)
        {
            FirstName = FirstNameValue;
            LastName = LastNameValue;
            EmailAddress = EmailAddressValue;
            PhoneNumber = PhoneNumberValue;
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
            return this.PhoneNumber.ToString();
        }

        public int getPhoneNumber()
        {
            return this.PhoneNumber;
        }

    }
}
