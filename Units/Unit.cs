using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Units
{
    abstract class Unit
    {
        protected static uint attackPoints;

        public static uint AttackPoints
        {
            get { return Unit.attackPoints; }
            set { Unit.attackPoints = value; }
        }

        private static uint defensePoints;

        protected static uint DefensePoints
        {
            get { return Unit.defensePoints; }
            set { Unit.defensePoints = value; }
        }
        private static uint hitPoints;

        protected static uint HitPoints
        {
            get { return Unit.hitPoints; }
            set { Unit.hitPoints = value; }
        }
        private static uint movementPoints;

        protected static uint MovementPoints
        {
            get { return Unit.movementPoints; }
            set { Unit.movementPoints = value; }
        }

        private uint remainingMovement;

        protected uint RemainingMovement
        {
            get { return remainingMovement; }
            set { remainingMovement = value; }
        }

        protected uint remainingHitPoints;

        public abstract void Attack(Unit opponent);
        public abstract void Die();
        public abstract Boolean MoveTo(Case to);

        public Unit(Case location);
    }
}
