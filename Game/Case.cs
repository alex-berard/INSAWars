using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Game
{
    public abstract class Case
    {
        #region fields
        private Map.Vector coordinates;
        private CaseStatus status;
        private City city;
        private HashSet<Unit> units;
        private Texture texture;
        private Player occupant;
        #endregion

        public List<Unit> Units {
            get { return units.ToList<Unit>(); }
        }

        public Unit AttackedUnit
        {
            get { return null; }
        }

        public Case(Map.Vector coordinates)
        {
            this.coordinates = coordinates;
            status = CaseStatus.FREE;
        }

        public Case()
        {

        }

        public virtual int GetFood();
        public virtual int GetIron();

        public void BuildCity(City city)
        {
            this.city = city;
            status = CaseStatus.CITY;
        }

        public void UsedBy(City city)
        {
            this.city = city;
            status = CaseStatus.USED;
        }

        public void Free()
        {
            city = null;
            status = CaseStatus.FREE;
        }

        public void AddUnit(Unit unit)
        {
            this.units.Add(unit);
        }

        public void RemoveUnit(Unit unit)
        {
            this.units.Remove(unit);
        }

        enum CaseStatus
        {
            FREE,
            USED,
            CITY
        }
    }
}
