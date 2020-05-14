using System;
using System.Collections.Generic;
using System.Text;

namespace MYMLibrary
{
    public class UserModel : PersonModel
    {
        public UserModel() : base()
        {

        }

        public UserModel(int IDValue, String FirstNameValue, String LastNameValue, String EmailAddressValue, int PhoneNumberValue) : base(IDValue, FirstNameValue, LastNameValue, EmailAddressValue, PhoneNumberValue)
        {
            
        }
    }
}
