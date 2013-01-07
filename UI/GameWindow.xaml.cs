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
using UI.Views;
using System.Windows.Threading;

namespace UI
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private GameBuilder _builder;
        private Game _game;
        private GameView _gameView;

        public GameWindow(GameBuilder builder)
        {
            _builder = builder;
            _builder.UseDefaultFrequencies();
            _game = _builder.Build();
            _gameView = new GameView(_game);

            InitializeComponent();
            InitializeDataContexts();
            InitializeClock();

            _gameControl.CaseSelected += _gameControl_CaseSelected;

            DrawMap();
        }

        void _gameControl_CaseSelected(object sender, CaseSelectionEventArgs e)
        {
            _caseInformation.DataContext = (e.IsDeselection ? null : new CaseView(e.SelectedCase));
        }

        private void InitializeDataContexts()
        {
            _turnLabel.DataContext = _gameView;
            _playerInformation.DataContext = new PlayerView(_game.CurrentPlayer);
        }

        private void InitializeClock()
        {
            var initial = DateTime.Now;
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                _clock.Content = DateTime.Now.Subtract(initial).ToString(@"hh\:mm\:ss");
            }, Dispatcher);
        }

        public void DrawMap()
        {
            _gameControl.Map = _game.Map;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                var menu = new InGameMenuWindow();
                Opacity = 0.5;
                menu.ShowDialog();
                Opacity = 1;
            }
        }

        private void NextTurnClick(object sender, RoutedEventArgs e)
        {
            _game.NextTurn();
            _playerInformation.DataContext = new PlayerView(_game.CurrentPlayer);
            _gameControl.InvalidateVisual();
        }
    }
}
