﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MYMLibrary
{
    public class UserModel : PersonModel
    {
        public UserModel() : base()
        {

        }

        public UserModel(int IDValue, String FirstNameValue, String LastNameValue, String EmailAddressValue, String PhoneNumberValue) : base(IDValue, FirstNameValue, LastNameValue, EmailAddressValue, PhoneNumberValue)
        {
            
        }

        public UserModel(String FirstNameValue, String LastNameValue, String EmailAddressValue, String PhoneNumberValue) : base(FirstNameValue, LastNameValue, EmailAddressValue, PhoneNumberValue)
        {

        }

        public UserModel(String FirstNameValue, String LastNameValue, String EmailAddressValue, String PhoneNumberValue, byte[] ImageBlobValue) : base(FirstNameValue, LastNameValue, EmailAddressValue, PhoneNumberValue, ImageBlobValue)
        {
            
        }



    }
}
