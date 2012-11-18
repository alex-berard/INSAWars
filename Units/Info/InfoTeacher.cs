using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Game;

namespace INSAWars.Units.Info
{
    class InfoTeacher : Teacher
    {
        public override double DefensePoints { get { return 1.0; } }
        public override double MovementPoints { get { return 3.0; } }
        public override double HitPoints { get { return 1.0; } }

        public InfoTeacher(Case location)
            : base(location)
        {
        }
    }
}
