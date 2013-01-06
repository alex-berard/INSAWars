using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INSAWars.Game;
using INSAWars.MVVM;
using System.ComponentModel;

namespace UI.Views
{
    public class GameView : ObservableObject
    {
        private Game _game;
        private string _turns;

        public GameView(Game game)
        {
            _game = game;
            _game.PropertyChanged += new PropertyChangedEventHandler(delegate(object sender, PropertyChangedEventArgs args)
            {
                Turns = TurnLabel(_game.NbTurns);
            });
           Turns = TurnLabel(_game.NbTurns);
        }

        public string Turns
        {
            get { return _turns; }
            set
            {
                SetProperty(ref _turns, value);
            }
        }

        protected string TurnLabel(int turns)
        {
            return "Turn " + (turns + 1).ToString();
        }
    }
}
