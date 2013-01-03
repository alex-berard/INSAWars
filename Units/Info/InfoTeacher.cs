using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Game;

namespace INSAWars.Units.Info
{
    class InfoTeacher : Teacher
    {
        public override uint AttackPoints { get { return 0; } }
        public override uint DefensePoints { get { return 1; } }
        public override uint HitPoints { get { return 1; } }
        public override uint MovementPoints { get { return 3; } }

        public InfoTeacher(Case location, Player player)
            : base(location, player)
        {
        }
    }
}
