﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for NumberOfPlayerPage.xaml
    /// </summary>
    public partial class ChoosePlayerCountPage : Page
    {
        public ChoosePlayerCountPage()
        {
            InitializeComponent();
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void NextButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChoosePlayersPage(int.Parse(_numberOfPlayers.Text)));
        }
    }
}
