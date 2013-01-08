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
    /// The in-game menu provides actions to create a new game, save the curent game or exit the application.
    /// </summary>
    public partial class InGameMenuWindow : Window
    {
        public InGameMenuWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Shows the user a new page to save the current game to a file.
        /// Not implemented yet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveGameClicked(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
            Close();
        }

        /// <summary>
        /// Creates a new Main Window to start a new game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackToMainMenuClicked(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            var gameWindow = Application.Current.MainWindow;
            Application.Current.MainWindow = window;
            window.Show();
            gameWindow.Close();
            Close();
        }

        /// <summary>
        /// Hides the menu if the player pushes Escape.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}
