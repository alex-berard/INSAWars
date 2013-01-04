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
using INSAWars.Game;

namespace UI
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private GameBuilder _builder;
        private Game _game;

        public GameWindow(GameBuilder builder)
        {
            _builder = builder;
            _builder.UseDefaultFrequencies();
            _game = _builder.Build();

            InitializeComponent();
            DrawMap();
        }

        public void DrawMap()
        {
            _gameView.Map = _game.Map;
        }
    }
}
