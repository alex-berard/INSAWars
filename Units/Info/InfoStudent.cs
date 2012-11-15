using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Units.EII
{
    class InfoStudent : Student
    {
        public virtual double AttackPoints { get { return 4.0; } }
        public virtual double DefensePoints { get { return 2.0; } }
        public virtual double MovementPoints { get { return 2.0; } }
        public virtual double HitPoints { get { return 10.0; } }
    }
}
