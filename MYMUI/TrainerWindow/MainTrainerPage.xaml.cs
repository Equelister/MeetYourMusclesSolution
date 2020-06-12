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

            OracleSQLConnectorImages connector = new OracleSQLConnectorImages();
            byte[] blob = trainer.getImageBlob();//connector.getImageBytes(GlobalClass.getTrainerID());
            if (blob != null)
            {
                BitmapImage newImg = ToImage(blob);
                userImage.Source = newImg;
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

        public BitmapImage ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        private void userImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.DefaultExt = ".jpg";
                dlg.Filter = "Image File (.jpg; .png)|*.jpg; *.png";
                Nullable<bool> result = dlg.ShowDialog();

                if (result == true)
                {
                    string filename = dlg.FileName;
                    BitmapImage newImg = new BitmapImage(new Uri(filename));
                    var newImgSize = new System.IO.FileInfo(filename).Length;

                    if (newImgSize > 204800 || newImg.PixelWidth > 256 || newImg.PixelHeight > 256)
                    {
                        MessageBox.Show("Selected image is too large. (Max 200kB | 256x256px)");
                    }
                    else
                    {
                        OracleSQLConnectorImages connector = new OracleSQLConnectorImages();
                        if (connector.sendImageToDB("trainer_table", GlobalClass.getTrainerID(), filename))
                            userImage.Source = newImg;
                        else
                            MessageBox.Show("Error, while sending image to DataBase.");
                    }

                }
            }
            catch
            {
            }
        }
    }
}
