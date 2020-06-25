using System;
using Xunit;
using MYMLibrary.Validators;

namespace MYMXUnitTest
{
    public class RegisterTests
    {
        /// <summary>
        /// For Null data 
        /// </summary>
        [Fact]
        public static void ShouldReturnFalseForNullInputs()
        {
            RegisterPageValidator validator = new RegisterPageValidator();
            String firstName = "";
            String lastName = "";
            String email = "";
            String phoneNumberString = "";
            String password = "";
            String retypedPassword = "";

            var result = validator.IsInputedDataValid(firstName, lastName, email, phoneNumberString, password, retypedPassword);

            Assert.False(result);
        }

        /// <summary>
        /// For Empty data 
        /// </summary>
        [Fact]
        public static void ShouldReturnFalseForEmptyInputs()
        {
            RegisterPageValidator validator = new RegisterPageValidator();
            String firstName = "             ";
            String lastName = "    ";
            String email = "      ";
            String phoneNumberString = "            ";
            String password = "  ";
            String retypedPassword = "  ";

            var result = validator.IsInputedDataValid(firstName, lastName, email, phoneNumberString, password, retypedPassword);

            Assert.False(result);
        }

        /// <summary>
        /// For Incorrect email 
        /// </summary>
        [Fact]
        public static void ShouldReturnFalseForIncorrectEmail()
        {
            RegisterPageValidator validator = new RegisterPageValidator();
            String firstName = "ABC";
            String lastName = "ABC";
            String email = "ABC";
            String phoneNumberString = "ABC";
            String password = "ABC";
            String retypedPassword = "ABC";

            var result = validator.IsInputedDataValid(firstName, lastName, email, phoneNumberString, password, retypedPassword);

            Assert.False(result);
        }

        /// <summary>
        /// For Mismatched passwords data 
        /// </summary>
        [Fact]
        public static void ShouldReturnFalseForMismatchedPasswords()
        {
            RegisterPageValidator validator = new RegisterPageValidator();
            String firstName = "ABC";
            String lastName = "ABC";
            String email = "ABC";
            String phoneNumberString = "ABC";
            String password = "ABC";
            String retypedPassword = "DEF";

            var result = validator.IsInputedDataValid(firstName, lastName, email, phoneNumberString, password, retypedPassword);

            Assert.False(result);
        }

        /// <summary>
        /// For Too Long data 
        /// </summary>
        [Fact]
        public static void ShouldReturnFalseForTooLongInputs()
        {
            RegisterPageValidator validator = new RegisterPageValidator();
            String firstName = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
            String lastName = "ABC";
            String email = "ABC";
            String phoneNumberString = "ABC";
            String password = "ABC";
            String retypedPassword = "ABC";

            var result = validator.IsInputedDataValid(firstName, lastName, email, phoneNumberString, password, retypedPassword);

            Assert.False(result);
        }

        /// <summary>
        /// For Valid data 
        /// </summary>
        [Fact]
        public static void ShouldReturnTrueForValidData()
        {
            RegisterPageValidator validator = new RegisterPageValidator();
            String firstName = "ABC";
            String lastName = "ABC";
            String email = "ABC@abc";
            String phoneNumberString = "ABC";
            String password = "ABC";
            String retypedPassword = "ABC";

            var result = validator.IsInputedDataValid(firstName, lastName, email, phoneNumberString, password, retypedPassword);

            Assert.True(result);
        }
    }
}
