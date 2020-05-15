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

namespace MYMUI
{
    /// <summary>
    /// Interaction logic for CreateMeetingPage.xaml
    /// </summary>
    public partial class CreateMeetingPage : Page
    {
        List<Int32> placesIDList = new List<Int32>();
        Int32 currentPlaceID;
        public CreateMeetingPage()
        {
            InitializeComponent();
            placesIDList = loadPlacesFromDataBase();
            
        }

        private void addPlaceButton_Click(object sender, RoutedEventArgs e)
        {
            PlaceModel place = new PlaceModel(1, cityTextBox.Text, postcodeTextBox.Text, streetTextBox.Text, descriptionTextBox.Text);
            //
            addNewPlaceToDataBase(place);
            //placesListBox.Items.Add(place.getCity()+place.getCity());   //send to database TO DO
            //
            
            //update listbox from database
            placesIDList = loadPlacesFromDataBase();
            //putIntoPlacesListBoxPlacesList(placesIDList);
        }

        public List<Int32> loadPlacesFromDataBase()
        {
            List<Int32> placesIDList = new List<Int32>();
            placesListBox.Items.Clear();
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
                    while (reader.Read())
                    {
                        PlaceModel place = new PlaceModel();
                        place.setID(reader.GetInt32(0));
                        place.setCity(reader.GetString(1));
                        place.setPostCode(reader.GetString(2));
                        place.setStreet(reader.GetString(3));
                        place.setDescription(reader.GetString(4));
                        placesIDList.Add(place.getID());
                        placesListBox.Items.Add(place.getCity() + place.getDescription()); ;
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return placesIDList;
        }

        public void addNewPlaceToDataBase(PlaceModel place)
        {
            using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
            {
                connection.Open();
                testLabel.Content = "Connected to Oracle" + connection.ServerVersion + connection.DatabaseName;

                OracleCommand cmd;

                string sql = String.Format("INSERT INTO place_table(city, postcode, street, description) VALUES('{0}', '{1}', '{2}', '{3}')", place.getCity(), place.getPostCode(), place.getStreet(), place.getDescription());

                cmd = new OracleCommand(sql, connection);
                cmd.CommandType = CommandType.Text;

                int rowsUpdated = cmd.ExecuteNonQuery();
                if (rowsUpdated == 0)
                {
                    label.Content = "Record not inserted";
                }
                else
                {
                    label.Content = "Success! User has been created";
                }


            }
        }

        /// <summary>
        /// TO DO 
        /// get id from database and show content for this id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void placesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (placesListBox.Items.Count > 0)
            {
                int index = placesListBox.Items.IndexOf(placesListBox.SelectedItem);
                PlaceModel place = new PlaceModel();

                using (OracleConnection connection = new OracleConnection(OracleSQLConnector.GetConnectionString()))
                {
                    connection.Open();
                    testLabel.Content = "Connected to Oracle" + connection.ServerVersion + connection.DatabaseName;
                    OracleCommand cmd;

                    string sql = String.Format("select * from place_table WHERE ID = {0}", placesIDList.ElementAt(index));

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
                try
                {
                    currentPlaceID = place.getID();
                    cityTextBox.Text = place.getCity();
                    postcodeTextBox.Text = place.getPostCode();
                    streetTextBox.Text = place.getStreet();
                    descriptionTextBox.Text = place.getDescription();
                }
                catch (Exception errorek)
                {
                    label.Content = "ERROR placesListBox_SelectionChanged";
                }
            }
        }

    }
}
