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
    /// Interaction logic for ChooseMapSizePage.xaml
    /// </summary>
    public partial class ChooseMapSizePage : Page
    {
        private GameBuilder _builder;

        public ChooseMapSizePage(GameBuilder builder)
        {
            _builder = builder;

            InitializeComponent();
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void NextButtonClick(object sender, RoutedEventArgs e)
        {
            _builder.SetSize(_mapSize.Text);

            var main = Application.Current.MainWindow;
            var game = new GameWindow(_builder);            
            Application.Current.MainWindow = game;
            main.Close();
            game.Show();
        }
    }
}
