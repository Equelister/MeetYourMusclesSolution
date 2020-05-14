﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MYMUI
{
    /// <summary>
    /// Interaction logic for TrainerWindow.xaml
    /// </summary>
    public partial class TrainerWindow : Window
    {

        MYMLibrary.TrainerModel trainer = new MYMLibrary.TrainerModel(/* load from database */);

        CreateMeetingPage createMeetingPage = new CreateMeetingPage();
        public TrainerWindow(int trainerID)
        {
            InitializeComponent();


            //
            //trainer.setID(trainerID);
            //
            // trainer = Select trainer from trainer where id = trainerid
            //
        }

        private void Page0_Click(object sender, RoutedEventArgs e)
        {
            TrainerWindowFrame.Content = createMeetingPage;
        }
    }
}
