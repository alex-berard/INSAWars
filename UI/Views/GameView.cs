using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INSAWars.Game;

namespace UI.Views
{
    public class GameView
    {
        private Game _game;

        public GameView(Game game)
        {
            _game = game;
        }

        public string Turns
        {
            get { return "Turn " + (_game.NbTurns + 1).ToString(); }
        }
    }
}
