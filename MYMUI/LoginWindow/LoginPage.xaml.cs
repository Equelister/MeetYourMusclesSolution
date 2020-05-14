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

namespace MYMUI
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            //valudate with sql
            //login to user/trainer window

            if(emailTextBox.Text.Equals("admin") && passwordTextBox.Text.Equals("admin1"))
            {
                int indexOfUserIDTableFromUserPasswordTable = 0; // to do get from table after checking if login/pass are good


                App.Current.MainWindow.Hide();
                TrainerWindow trainerWindow = new TrainerWindow(indexOfUserIDTableFromUserPasswordTable);
                trainerWindow.Show();
                App.Current.MainWindow.Close();
            }

        }
    }
}
