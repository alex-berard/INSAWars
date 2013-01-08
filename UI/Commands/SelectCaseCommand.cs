using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INSAWars.Game;

namespace UI.Commands
{
    public class SelectCaseCommand : CaseCommand
    {
        private Game _game;
        private GameControl _control;
        
        public SelectCaseCommand(Game g, GameControl control)
        {
            _game = g;
            _control = control;
        }

        public override void Execute(Case selectedCase)
        {
            _control.SelectCase(selectedCase);
        }

        public override bool CanExectute(Case selectedCase)
        {
            return _game.IsVisible(selectedCase);
        }
    }
}
