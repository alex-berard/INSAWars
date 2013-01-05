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
    /// Interaction logic for InGameMenuWindow.xaml
    /// </summary>
    public partial class InGameMenuWindow : Window
    {
        public InGameMenuWindow()
        {
            InitializeComponent();
        }

        private void SaveGameButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
            Close();
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

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}
