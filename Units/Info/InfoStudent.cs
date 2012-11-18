using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Game;

namespace INSAWars.Units.Info
{
    class InfoStudent : Student
    {
        public override double AttackPoints { get { return 4.0; } }
        public override double DefensePoints { get { return 2.0; } }
        public override double MovementPoints { get { return 2.0; } }
        public override double HitPoints { get { return 10.0; } }

        public InfoStudent(Case location)
            : base(location)
        {
        }
    }
}
