using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Game;

namespace INSAWars.Units
{
    [Serializable]
    public abstract class Head : Unit
    {
        public override int AttackPoints { get { return 0; } }
        public override int DefensePoints { get { return 2; } }
        public override int HitPoints { get { return 5; } }
        public override int MovementPoints { get { return 3; } }

        public Head(Case position, Player player)
            : base(position, player)
        {
        }
    }
}
