using System;
using System.Collections.Generic;
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
using MYMLibrary.Models;
namespace MYMUI
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        OracleSQLConnectorLoginWindow oracleSQLConnectorLoginWindow = new OracleSQLConnectorLoginWindow();

        public LoginPage()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            //valudate with sql
            //login to user/trainer window
            bool emailFound = false;
            int personID = oracleSQLConnectorLoginWindow.getIDFromDataBase("user_password_table", emailTextBox.Text.Trim());
            if(personID>0)
            {
                emailFound = true;
                if(passwordPasswordBox.Password.Equals(OracleSQLConnectorLoginWindow.getColumnFromDataBaseForID("password", "user_password_table", personID)))
                {
                    GlobalClass.setUserID(personID);
                    App.Current.MainWindow.Hide();
                    UserWindow userWindow = new UserWindow();
                    userWindow.Show();
                    App.Current.MainWindow.Close();
                }else
                {
                    errorLabel.Content = "Wrong Password!";
                    //label incorrect password
                }
            }
            if (!emailFound)
            {
                personID = oracleSQLConnectorLoginWindow.getIDFromDataBase("trainer_password_table", emailTextBox.Text.Trim());
                if (personID > 0)
                {
                    emailFound = true;
                    if (passwordPasswordBox.Password.Equals(OracleSQLConnectorLoginWindow.getColumnFromDataBaseForID("password", "trainer_password_table", personID)))
                    {
                        GlobalClass.setTrainerID(personID);
                        App.Current.MainWindow.Hide();
                        TrainerWindow trainerWindow = new TrainerWindow();
                        trainerWindow.Show();
                        App.Current.MainWindow.Close();
                    }
                    else
                    {
                        errorLabel.Content = "Wrong Password!";
                        // label incorrect password
                    }
                }
            }
            if(!emailFound)
            {
                errorLabel.Content = "Wrong Email!";
                // label incorrect email
            }

        }
    }
}
