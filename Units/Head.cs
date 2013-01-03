using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Game;

namespace INSAWars.Units
{
    public class Head : Unit
    {
        public override uint AttackPoints { get { return 0; } }
        public override uint DefensePoints { get { return 2; } }
        public override uint FoodCost { get { return 0; } }
        public override uint IronCost { get { return 200; } }
        public override uint HitPoints { get { return 5; } }
        public override uint MovementPoints { get { return 3; } }

        public Head(Case position, Player player)
            : base(position, player)
        {
        }
    }
}
