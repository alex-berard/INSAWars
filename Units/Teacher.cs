using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Game;

namespace INSAWars.Units
{
    public class Teacher : Unit
    {
        public override double IronCost { get { return 60.0; } }

        public Teacher(Case location)
            : base(location)
        {
        }
    }
}
