using System;
using System.Collections.Generic;
using System.Text;

namespace MYMLibrary
{
    public class PersonModel
    {
        protected int ID;
        public String FirstName { get; set; }
        public String LastName { get; set; }
        protected String EmailAddress;
        protected String PhoneNumber;
        protected String Password;
        public byte[] ImageBlob { get; set; }

        
        
        //protected String ImageUrl = "";


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

        protected PersonModel(int IDValue, String FirstNameValue, String LastNameValue, String EmailAddressValue, String PhoneNumberValue, String ImageUrlValue)
        {
            ID = IDValue;
            FirstName = FirstNameValue;
            LastName = LastNameValue;
            EmailAddress = EmailAddressValue;
            PhoneNumber = PhoneNumberValue;
            //ImageUrl = ImageUrlValue;
        }

        protected PersonModel(String FirstNameValue, String LastNameValue, String EmailAddressValue, String PhoneNumberValue, byte[] ImageBlobValue)
        {
            FirstName = FirstNameValue;
            LastName = LastNameValue;
            EmailAddress = EmailAddressValue;
            PhoneNumber = PhoneNumberValue;
            ImageBlob = ImageBlobValue;
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

        public byte[] getImageBlob()
        {
            return this.ImageBlob;
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

        public void setImageBlob(byte[] newImageBlob)
        {
            this.ImageBlob = newImageBlob;
       }

        public String getFullName()
        {
            return getFirstName() + " " + getLastName();
        }
    }
}
