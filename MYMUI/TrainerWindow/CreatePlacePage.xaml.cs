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
using MYMUI;
using MYMLibrary;
using Oracle.ManagedDataAccess.Client;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using MYMLibrary.Models;
using MYMLibrary.DataBaseConnections;

namespace MYMUI
{

    /// <summary>
    /// CreatingPlacePage - Page to create new places by trainer
    /// placesList - List of all places from Oracle DataBase
    /// currentlySelectedItemID - ID of selected object
    /// currentlySelectedListItemindex - List index of an selected object
    /// </summary>
    public partial class CreatePlacePage : Page
    {
        OracleSQLConnector oracleSQLConnector = new OracleSQLConnector();
        List<PlaceModel> placesList = new List<PlaceModel>();
        private int currentlySelectedItemID = -1;

        public CreatePlacePage()
        {
            InitializeComponent();
            placesList = oracleSQLConnector.loadPlacesFromDataBase();
            putIntoPlacesListBoxPlacesList(placesList);
        }





        /// <summary>
        /// Adds new PlaceModel object to Oracle DataBase
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addPlaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (isPlaceTextBoxesGood())
            {
                PlaceModel place = new PlaceModel(cityTextBox.Text, postcodeTextBox.Text, streetTextBox.Text, descriptionTextBox.Text);
                {
                    OracleSQLConnectorTrainerWindow oraclesql = new OracleSQLConnectorTrainerWindow();
                    int placeID = oraclesql.insertPlaceToDBReturnItsID(place);
                    if (placeID < 0)
                    {
                        communicationLabel.Content = "Record not inserted";
                    }
                    else
                    {
                        place.setID(placeID);
                        placesList.Add(place);
                        placesListBox.Items.Add(place.getCity() + ", " + place.getDescription());
                        placesListBox.UnselectAll();
                        currentlySelectedItemID = -1;
                        communicationLabel.Content = "Success! Place has been created";
                    }
                }
            }
        }


        /// <summary>
        /// Updates row in Oracle DataBase and changes List and ListBox values for this specific object.
        /// </summary>
        private void updatePlace()
        {
            if(isPlaceTextBoxesGood())
            {
                PlaceModel place = new PlaceModel(cityTextBox.Text, postcodeTextBox.Text, streetTextBox.Text, descriptionTextBox.Text);
                OracleSQLConnectorTrainerWindow oraclesql = new OracleSQLConnectorTrainerWindow();
                if (oraclesql.updatedPlaceToDataBase(place, currentlySelectedItemID))
                {
                    placesList.ElementAt(placesListBox.SelectedIndex).setCity(place.getCity());
                    placesList.ElementAt(placesListBox.SelectedIndex).setPostCode(place.getPostCode());
                    placesList.ElementAt(placesListBox.SelectedIndex).setStreet(place.getStreet());
                    placesList.ElementAt(placesListBox.SelectedIndex).setDescription(place.getDescription());

                    placesListBox.Items[placesListBox.SelectedIndex] = place.getCity() + ", " + place.getDescription();

                    communicationLabel.Content = "Place updated!";
                }
                else
                {
                    communicationLabel.Content = "Failed to update palce.";
                }
            }
        }


        /// <summary>
        /// Checks if inputed data is valid 
        /// </summary>
        /// <returns></returns>
        private bool isPlaceTextBoxesGood()
        {
            if (String.IsNullOrEmpty(cityTextBox.Text.Trim())
                || String.IsNullOrEmpty(postcodeTextBox.Text.Trim())
                || String.IsNullOrEmpty(streetTextBox.Text.Trim())
                || String.IsNullOrEmpty(descriptionTextBox.Text.Trim()))
            {
                communicationLabel.Content = "Fill empty fields.";
                return false;
            }
            if (cityTextBox.Text.Length >= 50
                || postcodeTextBox.Text.Length >= 10
                || streetTextBox.Text.Length >= 50
                || descriptionTextBox.Text.Length >= 100)
            {
                communicationLabel.Content = "Too many characters";
                return false;
            }
            return true;
        }





        /// <summary>
        /// Places contents of List into ListBox
        /// </summary>
        /// <param name="placesList"></param>
        private void putIntoPlacesListBoxPlacesList(List<PlaceModel> placesList)
        {
            foreach (PlaceModel place in placesList)
            {
                placesListBox.Items.Add(place.getCity() + ", " + place.getDescription()); ;
            }
        }


        /// <summary>
        /// Inputs data of an selected object into TextBoxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void placesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (placesListBox.SelectedIndex >= 0)
            {
                //currentlySelectedListItemindex = placesListBox.Items.IndexOf(placesListBox.SelectedItem);

                currentlySelectedItemID = placesList[placesListBox.SelectedIndex].getID();
                cityTextBox.Text = placesList[placesListBox.SelectedIndex].getCity();
                postcodeTextBox.Text = placesList[placesListBox.SelectedIndex].getPostCode();
                streetTextBox.Text = placesList[placesListBox.SelectedIndex].getStreet();
                descriptionTextBox.Text = placesList[placesListBox.SelectedIndex].getDescription();
            }
        }




        /// <summary>
        /// Checks for an selected item and allows to update object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updatePlaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentlySelectedItemID >= 0)
            {
                updatePlace();
            }else
            {
                communicationLabel.Content = "First select an item.";
            }
        }
    }
}
