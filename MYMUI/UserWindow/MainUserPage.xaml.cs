using MYMLibrary;
using MYMLibrary.Models;
using MYMLibrary.DataBaseConnections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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

            OracleSQLConnectorImages connector = new OracleSQLConnectorImages();
            byte[] blob = user.getImageBlob();//connector.getImageBytes(GlobalClass.getUserID());
            if(blob != null)
            {
                BitmapImage newImg = ToImage(blob);
                userImage.Source = newImg;
            }
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
            int globalTrainerID = GlobalClass.getTrainerID();
            GlobalClass.setTrainerID(meeting.IDTrainer);
            TrainerModel trainer = oracleSQLConnector.loadTrainerFromDataBase(meeting.IDTrainer);
            GlobalClass.setTrainerID(globalTrainerID);

            PlaceModel place = oracleSQLConnector.loadPlaceFromDataBase(meeting.IDPlace);

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
                if (acceptedMeetingsList.ElementAt(i).Accepted == 0)
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
            acceptedMeetingsListBox.ItemsSource = null;
            pendingMeetingsListBox.ItemsSource = null;

            //acceptedMeetingsListBox.Items.Clear();
            //pendingMeetingsListBox.Items.Clear();
            loadData();
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
                        if (connector.sendImageToDB("user_table", GlobalClass.getUserID(), filename))
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


    }
}
