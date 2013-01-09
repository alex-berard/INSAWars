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

namespace UI
{
    /// <summary>
    /// Interaction logic for InGameMenuSavePage.xaml
    /// </summary>
    public partial class InGameMenuSavePage : Page
    {
        private Game _game;
        public InGameMenuSavePage(Game currentGame)
        {
            _game = currentGame;
            InitializeComponent();
        }

        private void BackClicked(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SaveClicked(object sender, RoutedEventArgs e)
        {
            
                GameBuilder.SaveGame(_gameName.Text, _game);
                Window.GetWindow(this).Close();
           
        }
    }
}
