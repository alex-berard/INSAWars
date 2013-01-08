using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INSAWars.Game;

namespace UI.Commands
{
    public abstract class CaseCommand
    {
        public abstract void Execute(Case selectedCase);
        public abstract bool CanExectute(Case selectedCase);
    }
}
