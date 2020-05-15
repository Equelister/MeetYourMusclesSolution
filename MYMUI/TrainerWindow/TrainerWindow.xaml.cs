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
    /// Interaction logic for TrainerWindow.xaml
    /// </summary>
    public partial class TrainerWindow : Window
    {
        CreateMeetingPage createMeetingPage = new CreateMeetingPage();

        public TrainerWindow()
        {
            InitializeComponent();
        }

        private void Page0_Click(object sender, RoutedEventArgs e)
        {
            TrainerWindowFrame.Content = createMeetingPage;
        }
    }
}
