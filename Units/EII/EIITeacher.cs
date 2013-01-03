using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Game;

namespace INSAWars.Units.EII
{
    class EIITeacher : Teacher
    {
        public override uint AttackPoints { get { return 0; } }
        public override uint DefensePoints { get { return 2; } }
        public override uint HitPoints { get { return 1; } }
        public override uint MovementPoints { get { return 2; } }

        public EIITeacher(Case location, Player player)
            : base(location, player)
        {
        }
    }
}
