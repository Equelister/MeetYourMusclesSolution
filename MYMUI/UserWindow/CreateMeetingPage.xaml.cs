using MYMLibrary;
using MYMLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MYMUI
{

    /// <summary>
    /// Interaction logic for CreateMeetingPage.xaml
    /// </summary>
    public partial class CreateMeetingPage : Page
    {
        List<TrainerModel> trainerList = new List<TrainerModel>();
        List<PlaceModel> placeList = new List<PlaceModel>();
         
        int currentlySelectedTrainerItemID = -1;
        int currentlySelectedPlaceItemID = -1;

        public CreateMeetingPage()
        {
            InitializeComponent();
            OracleSQLConnectorUserWindow oraclesql = new OracleSQLConnectorUserWindow();
            trainerList = oraclesql.loadAllTrainersFromDataBase();
            //putIntoTrainersListBoxTrainerList(trainerList);
            //trainersListBox.ItemsSource = trainerList;
            setTable();
        }

        private void setTable()
        {
            trainersListBox.ItemsSource = trainerList;
        }



        private void trainersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OracleSQLConnector oracleSQLConnector = new OracleSQLConnector();

            if (trainersListBox.SelectedIndex >= 0)
            {
                currentlySelectedTrainerItemID = trainerList[trainersListBox.SelectedIndex].getID();
                trainerTextBox.Text = trainerList.ElementAt(trainersListBox.SelectedIndex).getFirstName() + " "
                    + trainerList.ElementAt(trainersListBox.SelectedIndex).getLastName() + ", "
                    + trainerList.ElementAt(trainersListBox.SelectedIndex).getPhoneNumberStr();
            }


            GlobalClass.setTrainerID(currentlySelectedTrainerItemID);
            currentlySelectedPlaceItemID = -1;
            placesListBox.UnselectAll();
            placesListBox.SelectedIndex = -1;


            placeList.Clear();
            placeList = oracleSQLConnector.loadPlacesFromDataBase();
            placesListBox.Items.Clear();
            putIntoPlacesListBoxPlacesList(placeList);
        }

        private void placesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (placesListBox.SelectedIndex >= 0)
            {
                placeTextBox.Text = placeList.ElementAt(placesListBox.SelectedIndex).getCity() + ", " 
                    + placeList.ElementAt(placesListBox.SelectedIndex).getPostCode() + ", " 
                    + placeList.ElementAt(placesListBox.SelectedIndex).getStreet();
                currentlySelectedPlaceItemID = placeList[placesListBox.SelectedIndex].getID();
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
                OracleSQLConnectorUserWindow oraclesql = new OracleSQLConnectorUserWindow();
                if (oraclesql.insertMeetingToDBReturnItsID(meet))
                {
                    addLabelFailed.Visibility = Visibility.Hidden;
                    addLabelSuccess.Visibility = Visibility.Visible;
                }else
                {
                    addLabelFailed.Visibility = Visibility.Visible;
                    addLabelSuccess.Visibility = Visibility.Hidden;
                }
                    
                // date + timepicker = DateTime
                // to model
                //send

            }
            else
            {
                addLabelSuccess.Visibility = Visibility.Hidden;
                addLabelFailed.Visibility = Visibility.Visible;
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


    }
}
