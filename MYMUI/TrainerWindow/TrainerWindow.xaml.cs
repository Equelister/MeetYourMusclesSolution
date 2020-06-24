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
using System.Windows.Threading;

namespace MYMUI
{
    /// <summary>
    /// Interaction logic for TrainerWindow.xaml
    /// </summary>
    public partial class TrainerWindow : Window
    {
        CreatePlacePage CreatePlacePage = new CreatePlacePage();
        MainTrainerPage mainTrainerPage = new MainTrainerPage();
        bool maximized = false;
        public TrainerWindow()
        {
            InitializeComponent();
            setUpClock();
            TrainerWindowFrame.Content = mainTrainerPage;
        }

        private void Page0_Click(object sender, RoutedEventArgs e)
        {
            TrainerWindowFrame.Content = mainTrainerPage;
        }

        private void Page1_Click(object sender, RoutedEventArgs e)
        {
            TrainerWindowFrame.Content = CreatePlacePage;
        }
        private void setUpClock()
        {
            DispatcherTimer clock = new DispatcherTimer();
            clock.Interval = TimeSpan.FromMilliseconds(100);
            clock.Tick += timerTickEvent;
            clock.Start();
        }

        void timerTickEvent(object sender, EventArgs e)
        {
            clockTextBlock.Text = DateTime.Now.ToString(@"HH\:mm\:ss");
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            TrainerWindowFrame.Content = mainTrainerPage;
        }

        private void ListViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            TrainerWindowFrame.Content = CreatePlacePage;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void toTrayButton_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void maxminButton_Click(object sender, RoutedEventArgs e)
        {
            if (!maximized)
                SystemCommands.MaximizeWindow(this);
            else
                SystemCommands.RestoreWindow(this);
            maximized = !maximized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && Mouse.GetPosition(this).Y < 30)
                this.DragMove();
        }

        private void ListViewItem_Selected_2(object sender, RoutedEventArgs e)
        {
            if(BlueColorListItem.IsVisible)
            {
                BlueColorListItem.Visibility = Visibility.Collapsed;
                RedColorListItem.Visibility = Visibility.Collapsed;
            }else
            {
                BlueColorListItem.Visibility = Visibility.Visible;
                RedColorListItem.Visibility = Visibility.Visible;
            }

        }

        private void ListViewItem_Selected_3(object sender, RoutedEventArgs e)
        {

        }

        private void ListViewItem_Selected_4(object sender, RoutedEventArgs e)
        {

        }
    }
}
