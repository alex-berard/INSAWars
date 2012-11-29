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
        protected Drawing.Point coordinates;
        protected CaseStatus status;
        protected City city;
        protected HashSet<Unit> units;
        protected Texture texture;
        protected Player occupant;
        #endregion

        #region properties
        public virtual List<Unit> Units
        {
            get { return units.ToList<Unit>(); }
        }

        public abstract int Food
        {
            get;
        }

        public abstract int Iron
        {
            get;
        }
        #endregion properties

        #region constructor
        protected Case()
        {
            status = CaseStatus.FREE;
        }
        #endregion

        #region methods

        /// <summary>
        /// Clones this case, and changes its coordinates.
        /// </summary>
        /// <param name="x">The new x coordinate.</param>
        /// <param name="y">The new y coordinate.</param>
        /// <returns>The cloned case.</returns>
        public Case Clone(int x, int y)
        {
            Case case_ = (Case) this.MemberwiseClone();
            case_.coordinates = new Drawing.Point(x, y);

            return case_;
        }

        /// <summary>
        /// Returns the next target on this case (the unit with the highest defense).
        /// </summary>
        /// <returns>The unit with the highest defense.</returns>
        public virtual Unit GetAttackedUnit()
        {
            double bestDefense = 0;
            Unit bestUnit = null;

            foreach (Unit unit in units)
            {
                if (unit.DefensePoints >= bestDefense)
                {
                    bestUnit = unit;
                    bestDefense = unit.DefensePoints;
                }
            }

            return bestUnit;
        }

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
            return "Case " + coordinates;
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
