using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Game;

namespace INSAWars.Units.EII
{
    class EIITeacher : Teacher
    {
        public override double DefensePoints { get { return 2.0; } }
        public override double MovementPoints { get { return 2.0; } }
        public override double HitPoints { get { return 1.0; } }

        public EIITeacher(Case location)
            : base(location)
        {
        }
    }
}
