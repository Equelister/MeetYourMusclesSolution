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
        int currentlySelectedMeetingListItemindex = -1;
        int currentlySelectedMeetingItemID = -1;

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
            loadAllMeetingsFromDataBase();
            separeteAcceptedFromPendingLists();
            putIntoacceptedMeetingsListBoxTrainerList(acceptedMeetingsList);
            putIntopendingMeetingsListBoxTrainerList(pendingMeetingsList);
        }

        private void acceptedMeetingsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (acceptedMeetingsListBox.Items.IndexOf(acceptedMeetingsListBox.SelectedItem) >= 0)
            {
                currentlySelectedMeetingListItemindex = acceptedMeetingsListBox.Items.IndexOf(acceptedMeetingsListBox.SelectedItem);
            }
            if (acceptedMeetingsListBox.SelectedIndex >= 0)
            {
                currentlySelectedMeetingItemID = acceptedMeetingsList[currentlySelectedMeetingListItemindex].getID();
                int placeID = -1;
                int trainerID = loadTrainerIDFromMeetingTable(currentlySelectedMeetingItemID, out placeID);

                TrainerModel trainer = new TrainerModel();
                trainer = loadTrainerNameAndPhoneFromDataBase(trainerID);

                trainerTextBox.Text = trainer.getFirstName() + " " + trainer.getLastName();
                trainerPhoneNumberTextBox.Text = trainer.getPhoneNumberStr();

                // add place and date


                PlaceModel place = new PlaceModel();
                place = loadPlaceFromDataBase(placeID);

                placeTextBox.Text = place.getFullDetails();

                dateAndHourTextBox.Text = acceptedMeetingsList[currentlySelectedMeetingListItemindex].getDateAndHourStr();

            }
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                acceptedMeetingsListBox.SelectedIndex = -1;
            }));

            currentlySelectedMeetingItemID = -1;
            currentlySelectedMeetingListItemindex = -1;

        }

        private void pendingMeetingsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pendingMeetingsListBox.Items.IndexOf(pendingMeetingsListBox.SelectedItem) >= 0)
            {
                currentlySelectedMeetingListItemindex = pendingMeetingsListBox.Items.IndexOf(pendingMeetingsListBox.SelectedItem);
            }
            if(pendingMeetingsListBox.SelectedIndex >= 0)
            {
                currentlySelectedMeetingItemID = pendingMeetingsList[currentlySelectedMeetingListItemindex].getID();
                int placeID = -1;
                int trainerID = loadTrainerIDFromMeetingTable(currentlySelectedMeetingItemID, out placeID);

                TrainerModel trainer = new TrainerModel();
                trainer = loadTrainerNameAndPhoneFromDataBase(trainerID);

                trainerTextBox.Text = trainer.getFirstName() + " " + trainer.getLastName();
                trainerPhoneNumberTextBox.Text = trainer.getPhoneNumberStr();

                // add place and date
               // int placeID = loadplaceIDFromMeetingTable(currentlySelectedMeetingItemID);

                PlaceModel place = new PlaceModel();
                place = loadPlaceFromDataBase(placeID);

                placeTextBox.Text = place.getFullDetails();

                dateAndHourTextBox.Text = pendingMeetingsList[currentlySelectedMeetingListItemindex].getDateAndHourStr();
            }

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                pendingMeetingsListBox.SelectedIndex = -1;
            }));


            currentlySelectedMeetingItemID = -1;
            currentlySelectedMeetingListItemindex = -1;
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




        private void putIntoacceptedMeetingsListBoxTrainerList(List<MeetModel> meetingList)
        {
            TrainerModel trainer = new TrainerModel();
            for(int i=0; i < acceptedMeetingsList.Count; i++)
            {
                trainer = loadTrainerNameAndPhoneFromDataBase(acceptedMeetingsList[i].getIDTrainer());
                acceptedMeetingsListBox.Items.Add(i+1 +". " + trainer.getFirstName() +" "+trainer.getLastName()); 
            }
        }

        private void putIntopendingMeetingsListBoxTrainerList(List<MeetModel> meetingList)
        {
            TrainerModel trainer = new TrainerModel();
            for (int i = 0; i < pendingMeetingsList.Count; i++)
            {
                trainer = loadTrainerNameAndPhoneFromDataBase(pendingMeetingsList[i].getIDTrainer());
                pendingMeetingsListBox.Items.Add(i+1 + ". " + trainer.getFirstName() + " " + trainer.getLastName()); ;
            }
        }


        private void loadAllMeetingsFromDataBase()
        {
            using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
            {
                connection.Open();
                //testLabel.Content = "Connected to Oracle" + connection.ServerVersion + connection.DatabaseName;

                OracleCommand cmd;

                string sql = String.Format("select * from meet_table WHERE user_table_id = {0}", GlobalClass.getUserID());

                cmd = new OracleCommand(sql, connection);
                cmd.CommandType = CommandType.Text;

                OracleDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        MeetModel meeting = new MeetModel(
                            reader.GetInt32(0),
                            reader.GetInt32(5), 
                            reader.GetInt32(6), 
                            reader.GetInt32(7), 
                            reader.GetDateTime(1), 
                            reader.GetInt32(2), 
                            reader.GetInt32(3), 
                            reader.GetInt32(4));
                        acceptedMeetingsList.Add(meeting);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
        }

        private TrainerModel loadTrainerNameAndPhoneFromDataBase(int trainerID)
        {
            TrainerModel trainer = new TrainerModel();
            using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
            {
                connection.Open();
                //testLabel.Content = "Connected to Oracle" + connection.ServerVersion + connection.DatabaseName;

                OracleCommand cmd;

                string sql = String.Format("select first_name, last_name, phone_number from trainer_table WHERE id = {0}", trainerID);

                cmd = new OracleCommand(sql, connection);
                cmd.CommandType = CommandType.Text;

                OracleDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        trainer.setFirstName(reader.GetString(0));
                        trainer.setLastName(reader.GetString(1));
                        trainer.setPhoneNumber(reader.GetString(2));
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return trainer;
        }

        private int loadTrainerIDFromMeetingTable(int currentlySelectedMeetingItemID, out int placeID)
        {
            placeID = -1;
            int id = -1;
            using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
            {
                connection.Open();
                //testLabel.Content = "Connected to Oracle" + connection.ServerVersion + connection.DatabaseName;

                OracleCommand cmd;

                string sql = string.Format("select trainer_table_id, place_table_id from meet_table WHERE id = {0}", currentlySelectedMeetingItemID);

                cmd = new OracleCommand(sql, connection);
                cmd.CommandType = CommandType.Text;

                OracleDataReader reader = cmd.ExecuteReader();
                try
                {
                    if(reader.Read())
                    {
                        id = reader.GetInt32(0);
                        placeID = reader.GetInt32(1);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return id;
        }


        private PlaceModel loadPlaceFromDataBase(int placeID)
        {
            PlaceModel place = new PlaceModel();
            using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
            {
                connection.Open();
                //testLabel.Content = "Connected to Oracle" + connection.ServerVersion + connection.DatabaseName;

                OracleCommand cmd;

                string sql = String.Format("select * from place_table WHERE id = {0}", placeID);

                cmd = new OracleCommand(sql, connection);
                cmd.CommandType = CommandType.Text;

                OracleDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        place.setID(reader.GetInt32(0));
                        place.setCity(reader.GetString(1));
                        place.setPostCode(reader.GetString(2));
                        place.setStreet(reader.GetString(3));
                        place.setDescription(reader.GetString(4));
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return place;
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
