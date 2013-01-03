using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Game;

namespace INSAWars.Units.Info
{
    class InfoStudent : Student
    {
        public override uint AttackPoints { get { return 4; } }
        public override uint DefensePoints { get { return 2; } }
        public override uint HitPoints { get { return 10; } }
        public override uint MovementPoints { get { return 2; } }

        public InfoStudent(Case location, Player player)
            : base(location, player)
        {
        }
    }
}
