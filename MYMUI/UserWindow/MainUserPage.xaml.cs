using MYMLibrary;
using MYMLibrary.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
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
using System.Windows.Threading;

namespace MYMUI
{
    /// <summary>
    /// Interaction logic for MainUserPage.xaml
    /// </summary>
    public partial class MainUserPage : Page
    {
        List<MeetModel> acceptedMeetingsList = new List<MeetModel>();
        List<MeetModel> pendingMeetingsList = new List<MeetModel>();
        OracleSQLConnector oracleSQLConnector = new OracleSQLConnector();

        public MainUserPage()
        {
            InitializeComponent();
            loadUserData();
            loadData();
        }

        private void loadUserData()
        {
            UserModel user = oracleSQLConnector.loadUserFromDataBase();
            userNameTextBlock.Text = user.getFullName();
            userEmailTextBlock.Text = user.getEmailAddress();
            userPhoneNumberTextBlock.Text = user.getPhoneNumberStr();
        }

        private void loadData()
        {
            OracleSQLConnector oraclesql = new OracleSQLConnector();
            acceptedMeetingsList = oraclesql.loadAllMeetingsFromDataBase(GlobalClass.getUserID(), "user");
            separeteAcceptedFromPendingLists();
            Sorts sort = new Sorts();
            sort.sortListsByDateASC(acceptedMeetingsList);
            sort.sortListsByDateASC(pendingMeetingsList);
            acceptedMeetingsListBox.ItemsSource = acceptedMeetingsList;
            pendingMeetingsListBox.ItemsSource = pendingMeetingsList;
        }

        private void acceptedMeetingsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (acceptedMeetingsListBox.SelectedIndex >= 0)
            {
                setTextBoxesWithData(acceptedMeetingsList[acceptedMeetingsListBox.SelectedIndex]);
            }

        }

        private void pendingMeetingsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pendingMeetingsListBox.SelectedIndex >= 0)
            {
                setTextBoxesWithData(pendingMeetingsList[pendingMeetingsListBox.SelectedIndex]);
            }
        }

        private void setTextBoxesWithData(MeetModel meeting)
        {
            OracleSQLConnector oracleSQLConnector = new OracleSQLConnector();
            int globalUserID = GlobalClass.getTrainerID();
            GlobalClass.setUserID(meeting.getIDTrainer());
            TrainerModel trainer = oracleSQLConnector.loadTrainerFromDataBase(meeting.getIDTrainer());
            GlobalClass.setTrainerID(globalUserID);

            PlaceModel place = oracleSQLConnector.loadPlaceFromDataBase(meeting.getIDPlace());

            trainerTextBox.Text = trainer.getFullName();
            trainerPhoneNumberTextBox.Text = trainer.getPhoneNumberStr();
            placeTextBox.Text = place.getFullDetails();
            dateAndHourTextBox.Text = meeting.getDateAndHourStr();
        }


        private void separeteAcceptedFromPendingLists()
        {
            int size = acceptedMeetingsList.Count;
            for (int i=0; i< size; i++)
            {
                if (acceptedMeetingsList.ElementAt(i).getAccepted() == 0)
                {
                    pendingMeetingsList.Add(acceptedMeetingsList[i]);
                    acceptedMeetingsList.RemoveAt(i);
                    i--;
                    size--;
                }
            }
        }



        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            acceptedMeetingsList.Clear();
            pendingMeetingsList.Clear();
            acceptedMeetingsListBox.Items.Clear();
            pendingMeetingsListBox.Items.Clear();
            loadData();
        }
    }
}
