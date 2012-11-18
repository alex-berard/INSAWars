using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Game;
using INSAWars.Units;

namespace INSAWars.Units.Info
{
    class InfoCivilization : ICivilization
    {
        public AbstractUnitFactory UnitFactory
        {
            get { return new InfoUnitFactory(); }
        }
    }
}
