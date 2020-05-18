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
    /// Interaction logic for CreateMeetingPage.xaml
    /// </summary>
    public partial class CreateMeetingPage : Page
    {
        OracleSQLConnector oracleSQLConnector = new OracleSQLConnector();
        List<TrainerModel> trainerList = new List<TrainerModel>();
        List<PlaceModel> placeList = new List<PlaceModel>();

        int currentlySelectedTrainerListItemindex = -1;
        int currentlySelectedTrainerItemID = -1;
        int currentlySelectedPlaceListItemindex = -1;
        int currentlySelectedPlaceItemID = -1;
        public CreateMeetingPage()
        {
            InitializeComponent();
            trainerList = loadAllTrainersFromDataBase();
            putIntoTrainersListBoxTrainerList(trainerList);
        }



        private void trainersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (trainersListBox.Items.IndexOf(trainersListBox.SelectedItem) >= 0)
            {
                currentlySelectedTrainerListItemindex = trainersListBox.Items.IndexOf(trainersListBox.SelectedItem);
            }
            currentlySelectedTrainerItemID = trainerList[currentlySelectedTrainerListItemindex].getID();
            trainerTextBox.Text = trainerList.ElementAt(currentlySelectedTrainerListItemindex).getFirstName() + " " 
                + trainerList.ElementAt(currentlySelectedTrainerListItemindex).getLastName() + ", "
                + trainerList.ElementAt(currentlySelectedTrainerListItemindex).getPhoneNumberStr();



            GlobalClass.setTrainerID(currentlySelectedTrainerItemID);
            currentlySelectedPlaceItemID = -1;
            currentlySelectedPlaceListItemindex = -1;


            placeList.Clear();
            placeList = oracleSQLConnector.loadPlacesFromDataBase();
            placesListBox.Items.Clear();
            putIntoPlacesListBoxPlacesList(placeList);
        }

        private void placesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (placesListBox.Items.IndexOf(placesListBox.SelectedItem) >= 0)
            {
                currentlySelectedPlaceListItemindex = placesListBox.Items.IndexOf(placesListBox.SelectedItem);
                placeTextBox.Text = placeList.ElementAt(currentlySelectedPlaceListItemindex).getCity() + ", " 
                    + placeList.ElementAt(currentlySelectedPlaceListItemindex).getPostCode() + ", " 
                    + placeList.ElementAt(currentlySelectedPlaceListItemindex).getStreet();
                currentlySelectedPlaceItemID = placeList[currentlySelectedPlaceListItemindex].getID();
            }
            else
            {
                placeTextBox.Text = null;
            }
            if (placeList == null)
            {
                currentlySelectedPlaceItemID = -1;
                placeTextBox.Text = null;
            }
            GlobalClass.setPlaceID(currentlySelectedPlaceItemID);
        }

        private void putIntoPlacesListBoxPlacesList(List<PlaceModel> placesList)
        {
            foreach (PlaceModel place in placesList)
            {
                placesListBox.Items.Add(place.getCity() + ", " + place.getDescription()); ;
            }
        }

        private void putIntoTrainersListBoxTrainerList(List<TrainerModel> trainerList)
        {
            foreach (TrainerModel trainer in trainerList)
            {
                trainersListBox.Items.Add(trainer.getFirstName() + ", " + trainer.getLastName()); ;
            }
        }



        private List<TrainerModel> loadAllTrainersFromDataBase()
        {
            using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
            {
                connection.Open();
                //testLabel.Content = "Connected to Oracle" + connection.ServerVersion + connection.DatabaseName;

                OracleCommand cmd;

                string sql = String.Format("select * from trainer_table");

                cmd = new OracleCommand(sql, connection);
                cmd.CommandType = CommandType.Text;

                OracleDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        TrainerModel trainer = new TrainerModel();
                        trainer.setID(reader.GetInt32(0));
                        trainer.setFirstName(reader.GetString(1));
                        trainer.setLastName(reader.GetString(2));
                        trainer.setEmailAddress(reader.GetString(3));
                        trainer.setPhoneNumber(reader.GetString(4));
                        try
                        {
                            trainer.setPricePerhour(reader.GetInt32(5));
                        }
                        catch
                        {
                            trainer.setPricePerhour(0);
                        }
                        trainerList.Add(trainer);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return trainerList;
        }




        private void hourUp_Click(object sender, RoutedEventArgs e)
        {
            hourPicker.Text = ((Int32.Parse(hourPicker.Text)) + 1).ToString();
        }

        private void hourDown_Click(object sender, RoutedEventArgs e)
        {
            hourPicker.Text = ((Int32.Parse(hourPicker.Text)) - 1).ToString();
        }

        private void hourPicker_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(hourPicker.Text))
                {
                    if (Int32.Parse(hourPicker.Text) > 23)
                    {
                        hourPicker.Text = "0";
                    }
                    else if (Int32.Parse(hourPicker.Text) < 0)                                                  // If 0-9 -> String = '0' + xxx.Text; to DB
                    {
                        hourPicker.Text = "23";
                    }
                }
            } catch
            {
                hourPicker.Text = "12";
            }
        }

        private void minuteUp_Click(object sender, RoutedEventArgs e)
        {
            minutePicker.Text = ((Int32.Parse(minutePicker.Text)) + 1).ToString();
        }

        private void minuteDown_Click(object sender, RoutedEventArgs e)
        { 
            minutePicker.Text = ((Int32.Parse(minutePicker.Text)) - 1).ToString();
        }

        private void minutePicker_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(minutePicker.Text))
                    {
                    if (Int32.Parse(minutePicker.Text) > 59)
                    {
                        minutePicker.Text = "0";
                    }
                    else if (Int32.Parse(minutePicker.Text) < 0)
                    {
                        minutePicker.Text = "59";
                    }
                } 
            }
            catch
            {
                minutePicker.Text = "30";
            }
        }

        private void addMeetingButton_Click(object sender, RoutedEventArgs e)
        {
            if(isDataValidForInsert())
            {
                DateTime dateTime = new DateTime();
                String dateStr = datePicker.SelectedDate.ToString();
                dateStr = dateStr.Substring(0, 11);
                String timeStr = hourPicker.Text + ":" + minutePicker.Text + ":00";
                String dateAndTimeStr = dateStr + " " + timeStr;
                dateTime = Convert.ToDateTime(dateAndTimeStr);
                 durationComboBox.SelectedValue.ToString().Trim();
                MeetModel meet = new MeetModel(GlobalClass.getUserID(), GlobalClass.getTrainerID(), GlobalClass.getPlaceID(), dateTime, Int32.Parse(durationComboBox.SelectedValue.ToString().Trim())) ;

                if(insertMeetingToDBReturnItsID(meet))
                {
                    addLabel.Content = "Added successfully!";
                }else
                {
                    addLabel.Content = "Error.";
                }
                    
                // date + timepicker = DateTime
                // to model
                //send

            }
            else
            {
                addLabel.Content = "Data is not valid!";
                //Data invalid error
            }

            //check for empty
            //insert
        }

        private bool isDataValidForInsert()
        {

            if (String.IsNullOrEmpty(trainerTextBox.Text.Trim()))
                return false;
            if (String.IsNullOrEmpty(placeTextBox.Text.Trim()))
                return false;
            if (durationComboBox.SelectedIndex >= 0)
            {
                if (String.IsNullOrEmpty(durationComboBox.SelectedItem.ToString()))
                    return false;
            }
            else
                return false;
            if (datePicker.SelectedDate < DateTime.Now)
                return false;

            if (Int32.Parse(minutePicker.Text) < 10 && Int32.Parse(minutePicker.Text) >= 0)
            {
                minutePicker.Text = '0' + Int32.Parse(minutePicker.Text).ToString();
            }
            if (Int32.Parse(hourPicker.Text) < 10 && Int32.Parse(hourPicker.Text) >= 0)
            {
                hourPicker.Text = '0' + Int32.Parse(hourPicker.Text).ToString();
            }

            return true;
        }


        public bool insertMeetingToDBReturnItsID(MeetModel meet)
        {
            using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
            {
                try {
                    connection.Open();
                    OracleCommand cmd = new OracleCommand();
                    String sql = String.Format("INSERT INTO meet_table (date_and_time, duration, accepted, isnew, user_table_id, trainer_table_id, place_table_id) VALUES (TO_DATE('{0}', 'DD/MM/YYYY HH24:MI:SS'), {1}, {2}, {3}, {4}, {5}, {6})",
                        meet.getDateAndHour(), meet.getDuration(), meet.getAccepted(), meet.getNew(), meet.getIDUser(), meet.getIDTrainer(), meet.getIDPlace());

                    cmd = new OracleCommand(sql, connection);
                    cmd.CommandType = CommandType.Text;

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    return false;
                }
                finally
                {
                    connection.Close();
                }
                }
            return true;
        }

    }
}
