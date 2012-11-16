using INSAWars.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Units
{
    abstract class Unit
    {

        public virtual double AttackPoints { get { return 0.0; } }
        public virtual double DefensePoints { get { return 0.0; } }
        public virtual double FoodCost { get { return 0.0; } }        
        public virtual double HitPoints { get { return 0.0; } }
        public virtual double IronCost { get { return 0.0; } }
        public virtual double MovementPoints { get { return 0.0; } }
        
        public Case CurrentCase { get; set; }
        public uint RemainingHitPoints { get; set; }
        public uint RemainingMovementPoints { get; set; }
        public Texture Texture { get; set; }

        public abstract void Attack(Unit opponent);

        public void Kill() {
            
        }

        public abstract Boolean MoveTo(Case to);

        public Unit(Case location);
    }
}
