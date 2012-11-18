using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Game;

namespace INSAWars.Units.EII
{
    class EIIStudent : Student
    {
        public override double AttackPoints { get { return 3.0; } }
        public override double DefensePoints { get { return 3.0; } }
        public override double MovementPoints { get { return 2.0; } }
        public override double HitPoints { get { return 10.0; } }

        public EIIStudent(Case location)
            : base(location)
        {
        }
    }
}
