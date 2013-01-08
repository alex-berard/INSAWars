using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INSAWars.Game;
using INSAWars.Units;

namespace UI.Commands
{
    public class MoveUnitCommand : CaseCommand
    {
        private Unit _unit;

        public MoveUnitCommand(Unit unit)
        {
            _unit = unit;
        }

        public override void Execute(Case selectedCase)
        {
            _unit.MoveTo(selectedCase);
        }

        public override bool CanExectute(Case selectedCase)
        {
            return _unit.CanMoveTo(selectedCase);
        }
    }
}
