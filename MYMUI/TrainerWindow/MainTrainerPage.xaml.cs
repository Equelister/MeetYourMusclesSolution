using MYMLibrary;
using MYMLibrary.DataBaseConnections;
using MYMLibrary.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    /// Interaction logic for MainTrainerPage.xaml
    /// </summary>
    public partial class MainTrainerPage : Page
    {
        List<MeetModel> acceptedMeetingsList = new List<MeetModel>();
        List<MeetModel> pendingMeetingsList = new List<MeetModel>();
        List<MeetModel> declinedMeetingsList = new List<MeetModel>();

        public MainTrainerPage()
        {
            InitializeComponent();
            loadUserData();
            loadData();
        }

        private void loadUserData()
        {
            OracleSQLConnector oracleSQLConnector = new OracleSQLConnector();
            TrainerModel trainer = oracleSQLConnector.loadTrainerFromDataBase();
            trainerNameTextBlock.Text = trainer.getFullName();
            trainerEmailTextBlock.Text = trainer.getEmailAddress();
            trainerPhoneNumberTextBlock.Text = trainer.getPhoneNumberStr();

            if (!String.IsNullOrWhiteSpace(trainer.getImageUrl()))
            {
                String userImageUrl = trainer.getImageUrl();
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(trainer.getImageUrl(), UriKind.RelativeOrAbsolute);
                bi.EndInit();
                userImage.Source = bi;
            }
        }

        private void loadData()
        {
            OracleSQLConnector oraclesql = new OracleSQLConnector();
            pendingMeetingsList = oraclesql.loadAllMeetingsFromDataBase(GlobalClass.getTrainerID(), "trainer");
            separeteMeetingLists();
            Sorts sort = new Sorts();
            sort.sortListsByDateASC(acceptedMeetingsList);
            sort.sortListsByDateASC(pendingMeetingsList);
            sort.sortListsByDateASC(declinedMeetingsList);
            acceptedMeetingsListBox.ItemsSource = acceptedMeetingsList;
            pendingMeetingsListBox.ItemsSource = pendingMeetingsList;
            declinedMeetingsListBox.ItemsSource = declinedMeetingsList;
        }


        /// <summary>
        /// Loads all meetings from DB for trainer with date greater than current date
        /// </summary>


        private void separeteMeetingLists()
        {
            int size = pendingMeetingsList.Count;
            for (int i = 0; i < size; i++)
            {
                if (pendingMeetingsList.ElementAt(i).Accepted == 1)
                {
                    acceptedMeetingsList.Add(pendingMeetingsList[i]);
                    pendingMeetingsList.RemoveAt(i);
                    i--;
                    size--;
                }else if (pendingMeetingsList.ElementAt(i).Accepted == 0 && pendingMeetingsList.ElementAt(i).New == 0)
                {
                    declinedMeetingsList.Add(pendingMeetingsList[i]);
                    pendingMeetingsList.RemoveAt(i);
                    i--;
                    size--;
                }
            }
        }

        private void toDeclinedListButton_Click(object sender, RoutedEventArgs e)
        {
            if (pendingMeetingsListBox.SelectedIndex >= 0)
            {
                MeetModel meetingToMove = pendingMeetingsList.ElementAt(pendingMeetingsListBox.SelectedIndex);
                meetingToMove.Accepted = 0;
                meetingToMove.New = 0;
                OracleSQLConnectorTrainerWindow oraclesql = new OracleSQLConnectorTrainerWindow();
                if (oraclesql.updateMeetingStatus(meetingToMove))
                {
                    pendingMeetingsList.Remove(meetingToMove);
                    declinedMeetingsList.Add(meetingToMove);
                    pendingMeetingsListBox.Items.Refresh();
                    declinedMeetingsListBox.Items.Refresh();
                }
            }
        }

        private void toAcceptedListButton_Click(object sender, RoutedEventArgs e)
        {
            if (pendingMeetingsListBox.SelectedIndex >= 0)
            {
                MeetModel meetingToMove = pendingMeetingsList.ElementAt(pendingMeetingsListBox.SelectedIndex);
                meetingToMove.Accepted = 1;
                meetingToMove.New = 0;

                OracleSQLConnectorTrainerWindow oraclesql = new OracleSQLConnectorTrainerWindow();
                if (oraclesql.updateMeetingStatus(meetingToMove))
                {
                    pendingMeetingsList.Remove(meetingToMove);
                    acceptedMeetingsList.Add(meetingToMove);
                    pendingMeetingsListBox.Items.Refresh();
                    acceptedMeetingsListBox.Items.Refresh();
                }
            }
        }


        private void pendingMeetingsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(pendingMeetingsListBox.SelectedIndex >= 0)
                setTextBoxesWithData(pendingMeetingsList[pendingMeetingsListBox.SelectedIndex]);
        }
        private void acceptedMeetingsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (acceptedMeetingsListBox.SelectedIndex >= 0)
                setTextBoxesWithData(acceptedMeetingsList[acceptedMeetingsListBox.SelectedIndex]);
        }

        private void declinedMeetingsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (declinedMeetingsListBox.SelectedIndex >= 0)
                setTextBoxesWithData(declinedMeetingsList[declinedMeetingsListBox.SelectedIndex]);
        }

        /// <summary>
        /// Filling textBoxes
        /// </summary>
        /// <param name="meeting"></param>
        private void setTextBoxesWithData(MeetModel meeting)
        {
            OracleSQLConnector oracleSQLConnector = new OracleSQLConnector();
            int globalUserID = GlobalClass.getUserID();
            GlobalClass.setUserID(meeting.IDUser);
            UserModel user = oracleSQLConnector.loadUserFromDataBase();
            GlobalClass.setUserID(globalUserID);

            PlaceModel place = oracleSQLConnector.loadPlaceFromDataBase(meeting.IDPlace);

            userTextBox.Text = user.getFullName();
            userPhoneNumberTextBox.Text = user.getPhoneNumberStr();
            placeTextBox.Text = place.getFullDetails();
            dateAndHourTextBox.Text = meeting.getDateAndHourStr();
        }
    }
}
