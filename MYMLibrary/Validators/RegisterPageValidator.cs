using System;

namespace MYMLibrary.Validators
{
    public class RegisterPageValidator
    {
        public bool IsInputedDataValid(String firstNameValue, String lastNameValue, String emailValue, String phoneNumberStringValue, String passwordValue, String retypedPasswordValue)
        {
            if (firstNameValue.Equals(null) || firstNameValue.Length > 50)
                return false;
            if (lastNameValue.Equals(null) || lastNameValue.Length > 50)
                return false;
            if (emailValue.Equals(null) || emailValue.Length > 50)
                return false;
            if (!emailValue.Contains("@"))
                return false;
            if (phoneNumberStringValue.Length > 20)
                return false;
            if (!passwordValue.Equals(retypedPasswordValue))
                return false;
            return true;
        }

    }
}
