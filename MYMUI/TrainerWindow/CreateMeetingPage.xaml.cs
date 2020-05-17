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

namespace MYMUI
{

    /// <summary>
    /// CreatingPlacePage - Page to create new places by trainer
    /// placesList - List of all places from Oracle DataBase
    /// currentlySelectedItemID - ID of selected object
    /// currentlySelectedListItemindex - List index of an selected object
    /// </summary>
    public partial class CreateMeetingPage : Page
    {
        List<PlaceModel> placesList = new List<PlaceModel>();
        private int currentlySelectedItemID = -1;
        private int currentlySelectedListItemindex = -1;
        public CreateMeetingPage()
        {
            InitializeComponent();
            placesList = loadPlacesFromDataBase();
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
                    //
                    int placeID = insertPlaceToDBReturnItsID(place);
                    if (placeID < 0)
                    {
                        label.Content = "Record not inserted";
                    }
                    else
                    {
                        place.setID(placeID);
                        placesList.Add(place);
                        placesListBox.Items.Add(place.getCity() + ", " + place.getDescription());
                        label.Content = "Success! Place has been created";
                    }
                }
            }
        }


        /// <summary>
        /// Selects all rows from Oracle DataBase and inserts it to a List of Places
        /// </summary>
        /// <returns></returns>
        public List<PlaceModel> loadPlacesFromDataBase()
        {
            using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
            {
                connection.Open();
                testLabel.Content = "Connected to Oracle" + connection.ServerVersion + connection.DatabaseName;

                OracleCommand cmd;

                string sql = "select * from place_table";

                cmd = new OracleCommand(sql, connection);
                cmd.CommandType = CommandType.Text;

                OracleDataReader reader = cmd.ExecuteReader();
                try
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        PlaceModel place = new PlaceModel();
                        place.setID(reader.GetInt32(0));
                        place.setCity(reader.GetString(1));
                        place.setPostCode(reader.GetString(2));
                        place.setStreet(reader.GetString(3));
                        place.setDescription(reader.GetString(4));
                        placesList.Add(place);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return placesList;
        }


        /// <summary>
        /// Updates row in Oracle DataBase and changes List and ListBox values for this specific object.
        /// </summary>
        private void updatePlace()
        {
            if(isPlaceTextBoxesGood())
            {
                PlaceModel place = new PlaceModel(cityTextBox.Text, postcodeTextBox.Text, streetTextBox.Text, descriptionTextBox.Text);
                if(updatedPlaceToDataBase(place))
                {
                    placesList.ElementAt(currentlySelectedListItemindex).setCity(place.getCity());
                    placesList.ElementAt(currentlySelectedListItemindex).setPostCode(place.getPostCode());
                    placesList.ElementAt(currentlySelectedListItemindex).setStreet(place.getStreet());
                    placesList.ElementAt(currentlySelectedListItemindex).setDescription(place.getDescription());

                    placesListBox.Items[currentlySelectedListItemindex] = place.getCity() + ", " + place.getDescription();

                    label.Content = "Place updated!";
                }
                else
                {
                    label.Content = "Failed to update palce.";
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
                label.Content = "Fill empty fields.";
                return false;
            }
            if (cityTextBox.Text.Length >= 50
                || postcodeTextBox.Text.Length >= 10
                || streetTextBox.Text.Length >= 50
                || descriptionTextBox.Text.Length >= 100)
            {
                label.Content = "Too many characters";
                return false;
            }
            return true;
        }


        /// <summary>
        /// Sends object to Oracle DataBase for an update.
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        public bool updatedPlaceToDataBase(PlaceModel place)
        {
            using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
            {
                connection.Open();
                testLabel.Content = "Connected to Oracle" + connection.ServerVersion + connection.DatabaseName;

                OracleCommand cmd;

                string sql = String.Format("UPDATE place_table SET city = '{0}', postcode = '{1}', street = '{2}', description = '{3}' WHERE id = {4}", place.getCity(), place.getPostCode(), place.getStreet(), place.getDescription(), currentlySelectedItemID);

                cmd = new OracleCommand(sql, connection);
                cmd.CommandType = CommandType.Text;

                int rowsUpdated = cmd.ExecuteNonQuery();
                if (rowsUpdated == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
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
            if(placesListBox.Items.IndexOf(placesListBox.SelectedItem) >= 0)
            {
                currentlySelectedListItemindex = placesListBox.Items.IndexOf(placesListBox.SelectedItem);
            }
            currentlySelectedItemID = placesList[currentlySelectedListItemindex].getID();
            cityTextBox.Text = placesList[currentlySelectedListItemindex].getCity();
            postcodeTextBox.Text = placesList[currentlySelectedListItemindex].getPostCode();
            streetTextBox.Text = placesList[currentlySelectedListItemindex].getStreet();
            descriptionTextBox.Text = placesList[currentlySelectedListItemindex].getDescription();
        }


        /// <summary>
        /// Inserts object into Oracle DataBase
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        public int insertPlaceToDBReturnItsID(PlaceModel place)
        {
            int output = -1;
            using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
            {
                // Open a database connection
                connection.Open();
                OracleCommand cmd = new OracleCommand();
                           // INSERT statement with RETURNING clause to get the generated ID 
                cmd.CommandText = String.Format(
                "INSERT INTO place_table(city, postcode, street, description, trainer_table_id) VALUES('{0}', '{1}', '{2}', '{3}', {4}) RETURNING id INTO :id",
                place.getCity(), place.getPostCode(), place.getStreet(), place.getDescription(), GlobalClass.getTrainerID());
                cmd.Connection = connection;
            
                cmd.Parameters.Add(new OracleParameter
                {
                    ParameterName = ":id",
                    OracleDbType = OracleDbType.Int32,
                    Direction = ParameterDirection.Output
                });

                            // Execute INSERT statement
                cmd.ExecuteNonQuery();
                String outputStr = cmd.Parameters[":id"].Value.ToString();
                output = Convert.ToInt32(outputStr);
            }
            return output;
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
                label.Content = "First select an item.";
            }
        }
    }
}
