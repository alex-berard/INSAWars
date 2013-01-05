#region usings
using INSAWars.Units;
using INSAWars.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace INSAWars.Units
{
    [Serializable]
    public abstract class Unit
    {
        #region fields
        protected Player player;
        protected Case location;
        protected int remainingHitPoints;
        protected int remainingMovementPoints;
        #endregion

        #region properties
        public virtual int AttackPoints { get { return 0; } }
        public virtual int DefensePoints { get { return 0; } }
        public virtual int HitPoints { get { return 0; } }
        public virtual int MovementPoints { get { return 0; } }

        public virtual int AttackBonus { get { return 1; } }
        public virtual int DefenseBonus { get { return 1; } }

        public Player Player { get { return player; } }
        public Case Location { get { return location; } }
        public int RemainingHitPoints { get { return remainingHitPoints; } }
        public int RemainingMovementPoints { get { return remainingMovementPoints;  } }
        public bool HasAttacked { get; set; }
        public string Texture { get; set; }
        #endregion

        #region constructors
        public Unit(Case location, Player player)
        {
            this.location = location;
            this.player = player;
            Reset();
        }
        #endregion

        #region methods
        /// <summary>
        /// Makes this unit attack an other one.
        /// </summary>
        /// <param name="opponent">The unit to attack.</param>
        public void Attack(Unit opponent)
        {
            if (opponent.DefensePoints == 0)
            {
                opponent.Kill();
            }

            int hits = Math.Max(RemainingHitPoints, opponent.RemainingHitPoints) + 2;
            int rounds = Game.Game.random.Next(3, hits);

            for (int i = 0; i < rounds; i++)
            {
                double ratio = AttackPoints / opponent.DefensePoints;
                double proba = 0.5 * ratio;

                if (Game.Game.random.NextDouble() < proba)
                {
                    if (--opponent.remainingHitPoints == 0)
                    {
                        opponent.Kill();
                        return;
                    }
                }
                else
                {
                    if (--remainingHitPoints == 0)
                    {
                        Kill();
                        return;
                    }
                }
            }
        }

        public void Kill()
        {
            player.RemoveUnit(this);
            location.RemoveUnit(this);
        }

        public void MoveTo(Case destination)
        {
            location.RemoveUnit(this);
            location = destination;
            destination.AddUnit(this);

            remainingMovementPoints = Math.Max(0, remainingMovementPoints - location.DistanceTo(destination));
        }

        /// <summary>
        /// Resets the hit points and movement points (to call at the beginning of a new turn).
        /// </summary>
        public void Reset()
        {
            remainingHitPoints = HitPoints;
            remainingMovementPoints = MovementPoints;
            HasAttacked = false;
        }
        #endregion
    }
}
