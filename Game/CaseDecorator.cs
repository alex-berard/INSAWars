using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Game
{
    public abstract class CaseDecorator : Case
    {
        protected Case decoratedCase;

        public List<Unit> Units {
            get { return decoratedCase.Units; }
        }

        public Unit AttackedUnit
        {
            get { return decoratedCase.AttackedUnit; }
        }

        public CaseDecorator(Case decoratedCase)
        {
            this.decoratedCase = decoratedCase;
        }

        public virtual int GetFood();
        public virtual int GetIron();

        public void BuildCity(City city)
        {
            decoratedCase.BuildCity(city);
        }

        public void UsedBy(City city)
        {
            decoratedCase.UsedBy(city);
        }

        public void Free()
        {
            decoratedCase.Free();
        }

        public void AddUnit(Unit unit)
        {
            decoratedCase.AddUnit(unit);
        }

        public void RemoveUnit(Unit unit)
        {
            decoratedCase.RemoveUnit(unit);
        }
    }
}
