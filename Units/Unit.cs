#region usings
using INSAWars.Units;
using INSAWars.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.MVVM;
using System.ComponentModel;
#endregion

namespace INSAWars.Units
{
    [Serializable]
    public abstract class Unit : ObservableObject
    {
        #region fields
        protected Player _player;
        protected Case _location;
        protected int _remainingHitPoints;
        protected int _remainingMovementPoints;
        #endregion

        #region properties
        public virtual int AttackBase { get { return 0; } }
        public virtual int DefenseBase { get { return 0; } }
        public virtual int HitPoints { get { return 0; } }
        public virtual int MovementPoints { get { return 0; } }

        public virtual int AttackBonus {
            get { return (_player.Head != null && _location.Contains(_player.Head)) ? AttackBase / 2 : 0; }
        }

        public virtual int DefenseBonus {
            get { return (_player.Head != null && _location.Contains(_player.Head)) ? DefenseBase / 2 : 0; }
        }

        public virtual int AttackTotal { get { return AttackBase + AttackBonus; } }
        public virtual int DefenseTotal { get { return DefenseBase + DefenseBonus; } }

        public Player Player { get { return _player; } }
        public Case Location { get { return _location; } }
        public int RemainingHitPoints {
            get { return _remainingHitPoints; }
            set
            {
                SetProperty(ref _remainingHitPoints, value);
            }
        }
        public int RemainingMovementPoints {
            get { return _remainingMovementPoints;  }
            set
            {
                SetProperty(ref _remainingMovementPoints, value);
            }
        }
        public bool HasAttacked { get; set; }
        public virtual string Texture { get; set; }
        #endregion

        #region constructors
        public Unit(Case location, Player player)
        {
            this._location = location;
            this._player = player;
            Reset();
        }
        #endregion

        #region methods

        public void Attack(Case c)
        {
            if (c.HasUnits)
            {
                Unit opponent = c.MostDefensiveUnit;

                // Seize the territory, move the unit onto it.
                if (!c.HasUnits || Attack(opponent))
                {
                    MoveTo(c);
                }
            }
            else
            {
                MoveTo(c);
            }

            HasAttacked = true;
        }

        /// <summary>
        /// Starts an attack on another unit.
        /// </summary>
        /// <param name="opponent"></param>
        /// <returns></returns>
        protected bool Attack(Unit opponent)
        {
            if (opponent.DefenseTotal == 0)
            {
                opponent.Kill();
            }

            int hits = Math.Max(RemainingHitPoints, opponent.RemainingHitPoints) + 2;
            int rounds = Game.Game.random.Next(3, hits);

            for (int i = 0; i < rounds; i++)
            {
                double ratio = AttackTotal / opponent.DefenseTotal;
                double proba = 0.5 * ratio;

                if (Game.Game.random.NextDouble() < proba)
                {
                    if (--opponent.RemainingHitPoints == 0)
                    {
                        opponent.Kill();
                        return true;
                    }
                }
                else
                {
                    if (--RemainingHitPoints == 0)
                    {
                        Kill();
                        return false;
                    }
                }
            }
            return false;
        }

        public virtual void Kill()
        {
            _player.RemoveUnit(this);
            _location.RemoveUnit(this);
        }

        public void MoveTo(Case destination)
        {
            _location.RemoveUnit(this);
            destination.AddUnit(this);
            RemainingMovementPoints = Math.Max(0, RemainingMovementPoints - _location.DistanceTo(destination));
            _location = destination;
        }

        public bool CanMoveTo(Case destination)
        {
            if (Location == destination ||
                (destination.HasUnits && destination.Occupant != Player) ||
                (destination.HasCity && destination.Occupant != Player))
            {
                return false;
            }

            return RemainingMovementPoints >= Location.DistanceTo(destination);
        }

        public bool CanMove()
        {
            return RemainingMovementPoints > 0;
        }

        public bool CanAttack(Case c)
        {
            return CanAttack() && Location.DistanceTo(c) == 1;
        }

        public bool CanAttack()
        {
            return !HasAttacked && AttackTotal > 0;
        }

        public abstract bool CanBuildCity();

        /// <summary>
        /// Resets the hit points and movement points (to call at the beginning of a new turn).
        /// </summary>
        public void Reset()
        {
            RemainingHitPoints = HitPoints;
            RemainingMovementPoints = MovementPoints;
            HasAttacked = false;
        }
        #endregion
    }
}
