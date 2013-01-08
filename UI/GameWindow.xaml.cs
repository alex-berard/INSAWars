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
using UI.Commands;
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
        private CommandState _state;

        private enum CommandState
        {
            Attacking,
            Moving,
            Selecting           
        };

        public GameWindow(GameBuilder builder)
        {
            _builder = builder;
            _builder.UseDefaultFrequencies();
            _game = _builder.Build();
            _gameView = new GameView(_game);
            _state = CommandState.Selecting;

            _game.GameIsOver += _game_GameIsOver;

            InitializeComponent();
            InitializeGameControl();
            InitializeDataContexts();
            InitializeClock();
        }

        void _game_GameIsOver(object sender, GameOverEventArgs e)
        {
            var menu = new GameOverWindow(e.Winner.Name);
            Opacity = 0.5;
            menu.ShowDialog();
        }

        private void InitializeGameControl()
        {
            _gameControl.Game = _game;
            _gameControl.CaseClicked += CaseClicked;
            _gameControl.CaseSelected += CaseSelected;
        }

        private void CaseClicked(object sender, CaseClickedEventArgs e)
        {
            
            switch (_state)
            {
                case CommandState.Selecting:
                    var select = new SelectCaseCommand(_game, _gameControl);
                    if (select.CanExectute(e.ClickedCase))
                    {
                        select.Execute(e.ClickedCase);
                    }
                    else
                    {
                        _gameControl.DisplayInvalidCommandOn(e.ClickedCase);
                    }

                    break;
                case CommandState.Moving:
                    var unitToMove = ((UnitView)_units.SelectedItem).Unit;
                    var move = new MoveUnitCommand(_game, unitToMove);

                    if (move.CanExectute(e.ClickedCase))
                    {
                        move.Execute(e.ClickedCase);
                        _state = CommandState.Selecting;
                        Cursor = Cursors.Arrow;
                        _gameControl.SelectCase(e.ClickedCase);
                    }
                    else
                    {
                        _gameControl.DisplayInvalidCommandOn(e.ClickedCase);
                    }

                    break;
                case CommandState.Attacking:
                    var attackWithUnit = ((UnitView)_units.SelectedItem).Unit;
                    var attack = new AttackCommand(attackWithUnit);

                    if (attack.CanExectute(e.ClickedCase))
                    {
                        attack.Execute(e.ClickedCase);
                        _state = CommandState.Selecting;
                        Cursor = Cursors.Arrow;
                        _gameControl.SelectCase(e.ClickedCase);
                    }
                    else
                    {
                        _gameControl.DisplayInvalidCommandOn(e.ClickedCase);
                    }
                    break;
                default:
                    _state = CommandState.Selecting;
                    CaseClicked(sender, e);
                    break;
            }
        }

        private void CaseSelected(object sender, CaseSelectedEventArgs e)
        {
            _caseInformation.DataContext = _unitActions.DataContext = (e.IsDeselection ? null : new CaseView(_game, e.SelectedCase));
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
            _gameControl.ClearCaseSelection();
        }

        private void MoveClicked(object sender, RoutedEventArgs e)
        {
            _state = CommandState.Moving;
            Cursor = ((FrameworkElement)this.Resources["MoveCursor"]).Cursor;
        }

        private void AttackClicked(object sender, RoutedEventArgs e)
        {
            _state = CommandState.Attacking;
            Cursor = ((FrameworkElement)this.Resources["AttackCursor"]).Cursor;
        }

        private void BuildCityClicked(object sender, RoutedEventArgs e)
        {
            var builder = ((UnitView)_units.SelectedItem).Unit;
            _game.BuildCity(builder);
        }
    }
}
