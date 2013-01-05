#region usings
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

        public virtual int Food
        {
            get;
            set;
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
            units = new HashSet<Unit>();
        }
        #endregion

        #region methods
        /// <summary>
        /// Builds a city on this case. If the case was used as a field by another city, it is removed.
        /// </summary>
        /// <param name="city">The city to build.</param>
        public virtual void BuildCity(City city)
        {
            if (status == CaseStatus.USED)
            {
                this.city.RemoveField(this);
            }

            this.city = city;
            status = CaseStatus.CITY;
            occupant = city.Player;
        }

        /// <summary>
        /// Makes the given city use this case as a field.
        /// </summary>
        /// <param name="city">The city the field belongs to.</param>
        public virtual void Use(City city)
        {
            if (status != CaseStatus.CITY)
            {
                this.city = city;
                status = CaseStatus.USED;
                occupant = city.Player;
            }
        }

        public virtual void Sack()
        {
            city.RemoveField(this);
            Free();
        }

        public virtual void Free()
        {
            city = null;
            status = CaseStatus.FREE;
        }

        /// <summary>
        /// Adds a unit on the case.
        /// </summary>
        /// <param name="unit">The new unit to occupy the case.</param>
        public virtual void AddUnit(Unit unit)
        {
            this.units.Add(unit);
            occupant = unit.Player;
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
        /// or the case is used by a city as a field to produce resources,
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
