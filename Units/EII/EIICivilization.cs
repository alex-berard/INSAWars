using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Units.EII
{
    class EIICivilization : ICivilization
    {
        public AbstractUnitFactory UnitFactory
        {
            get { return new EIIUnitFactory(); }
        }
    }
}
