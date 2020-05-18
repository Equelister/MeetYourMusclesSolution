using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MYMLibrary;
using MYMLibrary.DataBaseConnections;
using Oracle.ManagedDataAccess.Client;

namespace MYMUI
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        OracleSQLConnectorLoginWindow oracleSQLConnectorLoginWindow = new OracleSQLConnectorLoginWindow();

        public RegisterPage()
        {
            InitializeComponent();
        }



        /// <summary>
        /// TO DO REGISTER
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            bool success = true;
            String firstNameValue = firstNameTextBox.Text.Trim();
            String lastNameValue = lastNameTextBox.Text.Trim();
            String emailValue = emailTextBox.Text.Trim();
            String phoneNumberStringValue = phoneNumberTextBox.Text.Trim();

      


            if (firstNameValue.Equals(null))
                success = false;
            if (lastNameValue.Equals(null))
                success = false;
            if (emailValue.Equals(null))
                success = false;
            if (!passwordPasswordBox.Password.Equals(retypePasswordPasswordBox.Password))
                success = false;



            if (success)
            {
                if (userRadioButton.IsChecked == true)
                {
                    UserModel u = new UserModel(firstNameValue, lastNameValue, emailValue, phoneNumberStringValue);
                    bool isEmailUnique = isUniqueValue("email", "user_password_table", emailValue);

                    if (isEmailUnique)
                    {
                        insertUserToDataBase(u, passwordPasswordBox.Password, "user_table");
                    }
                }
                else
                {
                    TrainerModel t = new TrainerModel(firstNameValue, lastNameValue, emailValue, phoneNumberStringValue);
                    bool isEmailUnique = isUniqueValue("email", "trainer_password_table", emailValue);

                    if (isEmailUnique)
                    {
                        insertUserToDataBase(t, passwordPasswordBox.Password, "trainer_table");
                    }
                }
            }else
            {
                // label error, return
            }
        }



        private bool isUniqueValue(String columnName, String tableName, String searchedValue)
        {
            bool success = false;
            using (OracleConnection connection = new OracleConnection(OracleSQLConnectorLoginWindow.GetConnectionString()))
            {
                connection.Open();
                OracleCommand cmd;

                string sql = String.Format("select {0} from {1} WHERE {0} LIKE '{2}'", columnName, tableName, searchedValue);

                cmd = new OracleCommand(sql, connection);
                cmd.CommandType = CommandType.Text;

                OracleDataReader reader = cmd.ExecuteReader();
                try
                {
                    if(reader.Read())
                    {
                        success = false;
                    }
                    else
                    {
                        success = true;
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return success;
        }


        private void insertUserToDataBase(PersonModel p, String password, String mainTableName)
        {
            using (OracleConnection connection = new OracleConnection(OracleSQLConnectorLoginWindow.GetConnectionString()))
            {
                connection.Open();
                OracleCommand cmd;

                string sql = String.Format("INSERT INTO {4}(first_name, last_name, email, phone_number) VALUES('{0}', '{1}', '{2}', '{3}')", p.getFirstName(), p.getLastName(), p.getEmailAddress(), p.getPhoneNumberStr(), mainTableName);

                cmd = new OracleCommand(sql, connection);
                cmd.CommandType = CommandType.Text;

                int rowsUpdated = cmd.ExecuteNonQuery();
                if (rowsUpdated == 0)
                {
                    //label.Content = "Record not inserted";
                }
                else
                {
                    String secondaryTableName;
                    if (mainTableName.Equals("user_table"))
                    {
                        secondaryTableName = "user_password_table";
                    }else
                    {
                        secondaryTableName = "trainer_password_table";
                    }
                    sql = String.Format("INSERT INTO {3}(password, {4}_id, email) VALUES('{0}', {1}, '{2}')", password, oracleSQLConnectorLoginWindow.getIDFromDataBase(mainTableName, p.getEmailAddress()), p.getEmailAddress(), secondaryTableName, mainTableName);

                    cmd = new OracleCommand(sql, connection);
                    cmd.CommandType = CommandType.Text;
                    rowsUpdated = cmd.ExecuteNonQuery();

                    if(rowsUpdated == 0)
                    {
                        // error drop previous insert
                    }
                }
            }
        }
    }
}
