using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Units
{
    class Head : Unit
    {
        public virtual double DefensePoints { get { return 2.0; } }
        public virtual double MovementPoints { get { return 3.0; } }
        public virtual double HitPoints { get { return 5.0; } }
        public virtual double IronCost { get { return 200.0; } }
    }
}
