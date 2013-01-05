using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Game;

namespace INSAWars.Units.EII
{
    [Serializable]
    class EIITeacher : Teacher
    {
        public override int AttackPoints { get { return 0; } }
        public override int DefensePoints { get { return 2; } }
        public override int HitPoints { get { return 1; } }
        public override int MovementPoints { get { return 2; } }

        public EIITeacher(Case location, Player player)
            : base(location, player)
        {
        }
    }
}
