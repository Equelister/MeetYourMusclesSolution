using MYMLibrary;
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
        OracleSQLConnector oracleSQLConnector = new OracleSQLConnector();
        int currentlySelectedMeetingListItemindex = -1;
        int currentlySelectedMeetingItemID = -1;

        public MainTrainerPage()
        {
            InitializeComponent();
            loadUserData();
            loadData();

        }

        private void loadUserData()
        {
            TrainerModel trainer = oracleSQLConnector.loadTrainerFromDataBase();
            trainerNameTextBlock.Text = trainer.getFullName();
            trainerEmailTextBlock.Text = trainer.getEmailAddress();
            trainerPhoneNumberTextBlock.Text = trainer.getPhoneNumberStr();
        }

        private void loadData()
        {
            loadAllMeetingsFromDataBase();
            separeteMeetingLists();
            putIntoAcceptedMeetingsListBoxTrainerList();
            putIntoPendingMeetingsListBoxTrainerList();
            putIntoDeclinedMeetingsListBoxTrainerList();


        }


        private void loadAllMeetingsFromDataBase()
        {
            using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
            {
                connection.Open();
                //testLabel.Content = "Connected to Oracle" + connection.ServerVersion + connection.DatabaseName;

                OracleCommand cmd;

                string sql = String.Format("select * from meet_table WHERE trainer_table_id = {0}", GlobalClass.getTrainerID());

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
                        pendingMeetingsList.Add(meeting);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
        }


        private void separeteMeetingLists()
        {
            int size = pendingMeetingsList.Count;
            for (int i = 0; i < size; i++)
            {
                if (pendingMeetingsList.ElementAt(i).getAccepted() == 1)
                {
                    acceptedMeetingsList.Add(pendingMeetingsList[i]);
                    pendingMeetingsList.RemoveAt(i);
                    i--;
                    size--;
                }else if (pendingMeetingsList.ElementAt(i).getAccepted() == 0 && pendingMeetingsList.ElementAt(i).getNew() == 0)
                {
                    declinedMeetingsList.Add(pendingMeetingsList[i]);
                    pendingMeetingsList.RemoveAt(i);
                    i--;
                    size--;
                }
            }
        }


        private void putIntoAcceptedMeetingsListBoxTrainerList()
        {
            UserModel user = new UserModel();
            for (int i = 0; i < acceptedMeetingsList.Count; i++)
            {
                user = loadUserNameAndPhoneFromDataBase(acceptedMeetingsList[i].getIDUser());
                acceptedMeetingsListBox.Items.Add(i + 1 + ". " + user.getFirstName() + " " + user.getLastName());
            }
        }

        private void putIntoPendingMeetingsListBoxTrainerList()
        {
            UserModel user = new UserModel();
            for (int i = 0; i < pendingMeetingsList.Count; i++)
            {
                user = loadUserNameAndPhoneFromDataBase(pendingMeetingsList[i].getIDUser());
                pendingMeetingsListBox.Items.Add(i + 1 + ". " + user.getFirstName() + " " + user.getLastName()); ;
            }
        }

        private void putIntoDeclinedMeetingsListBoxTrainerList()
        {
            UserModel user = new UserModel();
            for (int i = 0; i < declinedMeetingsList.Count; i++)
            {
                user = loadUserNameAndPhoneFromDataBase(declinedMeetingsList[i].getIDUser());
                declinedMeetingsListBox.Items.Add(i + 1 + ". " + user.getFirstName() + " " + user.getLastName()); ;
            }
        }

        private UserModel loadUserNameAndPhoneFromDataBase(int userID)
        {
            UserModel user = new UserModel();
            using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
            {
                connection.Open();
                //testLabel.Content = "Connected to Oracle" + connection.ServerVersion + connection.DatabaseName;

                OracleCommand cmd;

                string sql = String.Format("select first_name, last_name, phone_number from user_table WHERE id = {0}", userID);

                cmd = new OracleCommand(sql, connection);
                cmd.CommandType = CommandType.Text;

                OracleDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        user.setFirstName(reader.GetString(0));
                        user.setLastName(reader.GetString(1));
                        user.setPhoneNumber(reader.GetString(2));
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return user;
        }








    }
}
