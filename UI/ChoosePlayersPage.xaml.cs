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
using INSAWars.Game;
using INSAWars.Units;

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
                _playerThree.Visibility = Visibility.Hidden;
            }
            if (_playerCount < 4)
            {
                _playerFour.Visibility = Visibility.Hidden;
            }
        }

        private void AddPlayerToGameBuilder(GameBuilder builder, StackPanel player, TextBox name, ComboBox civilization)
        {
            if (player.Visibility == Visibility.Visible)
            {
                System.Diagnostics.Debug.WriteLine("This player is looovisible!");
                builder.AddPlayer(name.Text, CivilizationFactory.GetCivilizationByName(civilization.Text));
            }
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }        

        private void NextButtonClick(object sender, RoutedEventArgs e)
        {
            GameBuilder builder = new GameBuilder();
            AddPlayerToGameBuilder(builder, _playerOne, _playerOneName, _playerOneCivilization);
            AddPlayerToGameBuilder(builder, _playerTwo, _playerTwoName, _playerTwoCivilization);
            AddPlayerToGameBuilder(builder, _playerThree, _playerThreeName, _playerThreeCivilization);
            AddPlayerToGameBuilder(builder, _playerFour, _playerFourName, _playerFourCivilization);
            
            NavigationService.Navigate(new ChooseMapSizePage(builder));
        }
    }
}
