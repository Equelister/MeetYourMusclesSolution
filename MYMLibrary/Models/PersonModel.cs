using System;
using System.Collections.Generic;
using System.Text;

namespace MYMLibrary
{
    public class PersonModel
    {
        protected int ID;
        protected String FirstName;
        protected String LastName;
        protected String EmailAddress;
        protected String PhoneNumber;
        protected String Password;


        public PersonModel()
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

        public String getPassword()
        {
            return this.Password;
        }

        public void setID(int newID)
        {
            this.ID = newID;
        }

        public void setFirstName(String newFirstName)
        {
            this.FirstName = newFirstName;
        }

        public void setLastName(String newLastName)
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

        public void setPassword(String newPassword)
        {
            this.Password = newPassword;
        }

        public String getFullName()
        {
            return getFirstName() + " " + getLastName();
        }
    }
}
