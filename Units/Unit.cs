using INSAWars.Units;
using INSAWars.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Units
{
    public abstract class Unit
    {
        protected Case currentCase;
        protected double remainingLife;
        protected double remainingMovementPoints;

        public virtual double AttackPoints { get { return 0.0; } }
        public virtual double DefensePoints { get { return 0.0; } }
        public virtual double FoodCost { get { return 0.0; } }        
        public virtual double HitPoints { get { return 0.0; } }
        public virtual double IronCost { get { return 0.0; } }
        public virtual double MovementPoints { get { return 0.0; } }

        public Player Player { get; set; }
        public Case CurrentCase { get; set; }
        public uint RemainingHitPoints { get; set; }
        public uint RemainingMovementPoints { get; set; }
        public Texture Texture { get; set; }

        public void Attack(Unit opponent)
        {

        }

        public void Kill()
        {
            
        }

        public bool MoveTo(Case to)
        {
            return false;
        }

        /// <summary>
        /// Resets the hit points and movement points.
        /// </summary>
        public void Reset()
        {

        }

        public Unit(Case location)
        {

        }
    }
}
