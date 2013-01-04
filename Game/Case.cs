﻿#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Units;
using Drawing = System.Drawing;
#endregion

namespace INSAWars.Game
{
    public abstract class Case
    {
        #region fields
        protected int x;
        protected int y;
        protected CaseStatus status;
        protected City city;
        protected HashSet<Unit> units;
        protected Player occupant;
        #endregion

        #region properties
        public virtual City City
        {
            get { return city; }
        }

        public virtual bool IsFree
        {
            get { return status == CaseStatus.FREE; }
        }

        public virtual bool IsUsed
        {
            get { return status == CaseStatus.USED; }
        }

        public virtual bool HasCity
        {
            get { return status == CaseStatus.CITY; }
        }

        public virtual bool HasUnits
        {
            get { return units.Count > 0; }
        }

        public virtual Player Occupant
        {
            // TODO: assign and update
            get { return occupant; }
        }

        public virtual IEnumerable<Unit> Units
        {
            get { return units.OrderByDescending(unit => unit.DefensePoints); }
        }

        /// <summary>
        /// The visible unit on this case (the unit with the highest defense).
        /// </summary>
        public virtual Unit MostDefensiveUnit
        {
            get { return units.OrderByDescending(unit => unit.DefensePoints).First(); }
        }

        public virtual int X
        {
            get { return x; }
        }

        public virtual int Y
        {
            get { return y; }
        }

        public abstract int Food
        {
            get;
        }

        public abstract int Iron
        {
            get;
        }

        public abstract string Texture
        {
            get;
        }
        #endregion properties

        #region constructor
        public Case()
        { }

        public Case(int x, int y)
        {
            this.x = x;
            this.y = y;
            status = CaseStatus.FREE;
        }
        #endregion

        #region methods
        /// <summary>
        /// Tries to build a city on this case.
        /// </summary>
        /// <param name="city">The city to build.</param>
        /// <returns>False if the case is already occupied, true otherwise.</returns>
        public virtual bool BuildCity(City city)
        {
            // TODO: allow a player to build a city on a used case of his own.

            if (status != CaseStatus.FREE)
            {
                return false;
            }
            else
            {
                this.city = city;
                status = CaseStatus.CITY;
                return true;
            }
        }

        public virtual bool Use(City city)
        {
            if (status != CaseStatus.FREE)
            {
                return false;
            }
            else
            {
                this.city = city;
                status = CaseStatus.USED;
                return true;
            }
        }

        public virtual void Free()
        {
            city = null;
            status = CaseStatus.FREE;
        }

        public virtual void AddUnit(Unit unit)
        {
            this.units.Add(unit);
        }

        public virtual void RemoveUnit(Unit unit)
        {
            this.units.Remove(unit);
        }

        public override string ToString()
        {
            return "Case (" + x + ", " + y + ")";
        }
        #endregion

        /// <summary>
        /// A city is built on the case,
        /// or the case is used by a city to produce resources,
        /// or it is unused.
        /// </summary>
        protected enum CaseStatus
        {
            USED,
            CITY,
            FREE
        }
    }
}
