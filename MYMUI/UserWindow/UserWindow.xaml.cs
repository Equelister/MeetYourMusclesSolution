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
using System.Windows.Shapes;

namespace MYMUI
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        MainUserPage mainUserPage = new MainUserPage();
        CreateMeetingPage createMeetingPage = new CreateMeetingPage();

        public UserWindow()
        {
            InitializeComponent();
        }

        private void Page1_Click(object sender, RoutedEventArgs e)
        {
            UserWindowFrame.Content = createMeetingPage;

        }

        private void Page0_Click(object sender, RoutedEventArgs e)
        {
            UserWindowFrame.Content = mainUserPage;
        }
    }
}
