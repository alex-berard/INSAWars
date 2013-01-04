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
    public abstract class Unit
    {
        #region fields
        protected Player player;
        protected Case location;
        protected uint remainingHitPoints;
        protected uint remainingMovementPoints;
        #endregion

        #region properties
        public virtual uint AttackPoints { get { return 0; } }
        public virtual uint DefensePoints { get { return 0; } }
        public virtual uint HitPoints { get { return 0; } }
        public virtual uint MovementPoints { get { return 0; } }

        public Player Player { get { return player; } }
        public Case Location { get { return location; } }
        public uint RemainingHitPoints { get { return remainingHitPoints; } }
        public uint RemainingMovementPoints { get { return remainingMovementPoints;  } }
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

            int hits = Math.Max((int) RemainingHitPoints, (int) opponent.RemainingHitPoints) + 2;
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
                        this.Kill();
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
