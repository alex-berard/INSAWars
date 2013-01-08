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
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for GameOverWindow.xaml
    /// </summary>
    public partial class GameOverWindow : Window
    {
        public GameOverWindow(string winnerName)
        {
            InitializeComponent();
            _winnerAcclaim.Text = winnerName + _winnerAcclaim.Text;
        }

        private void NewGameButtonClick(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            var gameWindow = Application.Current.MainWindow;
            Application.Current.MainWindow = window;
            window.Show();
            gameWindow.Close();
            Close();
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
            Close();
        }
    }
}
