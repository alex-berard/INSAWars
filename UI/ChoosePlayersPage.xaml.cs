using System;
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
    /// Interaction logic for ChoosePlayersPage.xaml
    /// </summary>
    public partial class ChoosePlayersPage : Page
    {
        private int _playerCount;

        public ChoosePlayersPage(int playerCount)
        {
            _playerCount = playerCount;
            InitializeComponent();
            HideUnusedPlayers();
        }

        private void HideUnusedPlayers()
        {
            if (_playerCount < 3)
            {
                _playerThree.Visibility = System.Windows.Visibility.Hidden;
            }
            if (_playerCount < 4)
            {
                _playerFour.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void NextButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
