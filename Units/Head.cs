using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Game;

namespace INSAWars.Units
{
    public class Head : Unit
    {
        public override double DefensePoints { get { return 2.0; } }
        public override double MovementPoints { get { return 3.0; } }
        public override double HitPoints { get { return 5.0; } }
        public override double IronCost { get { return 200.0; } }

        public Head(Case position)
            : base(position)
        {
        }
    }
}
