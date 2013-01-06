using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Units;

namespace INSAWars.Game
{
    /// <summary>
    /// Adds an amount of food or iron to a case.
    /// </summary>
    [Serializable]
    public abstract class CaseDecorator : Case
    {
        protected Case decoratedCase;

        public override City City
        {
            get { return decoratedCase.City; }
        }

        public override bool IsFree
        {
            get { return decoratedCase.IsFree; }
        }

        public override bool IsUsed
        {
            get { return decoratedCase.IsUsed; }
        }

        public override bool HasCity
        {
            get { return decoratedCase.IsUsed; }
        }

        public override bool HasUnits
        {
            get { return decoratedCase.HasUnits; }
        }

        public override Player Occupant
        {
            get { return decoratedCase.Occupant; }
        }

        public override int X
        {
            get { return decoratedCase.X; }
        }

        public override int Y
        {
            get { return decoratedCase.Y; }
        }

        public override IEnumerable<Unit> Units
        {
            get { return decoratedCase.Units; }
        }

        public override IEnumerable<Unit> Students
        {
            get
            {
                return decoratedCase.Students;
            }
        }

        public override IEnumerable<Unit> Teachers
        {
            get
            {
                return decoratedCase.Teachers;
            }
        }

        public override Unit Head
        {
            get
            {
                return decoratedCase.Head;
            }
        }

        public CaseDecorator(Case decoratedCase)
        {
            this.decoratedCase = decoratedCase;
        }

        public override void BuildCity(City city)
        {
            decoratedCase.BuildCity(city);
        }

        public override void Use(City city)
        {
            decoratedCase.Use(city);
        }

        public override void Free()
        {
            decoratedCase.Free();
        }

        public override void AddUnit(Unit unit)
        {
            decoratedCase.AddUnit(unit);
        }

        public override void RemoveUnit(Unit unit)
        {
            decoratedCase.RemoveUnit(unit);
        }

        public override string ToString()
        {
            return decoratedCase.ToString();
        }
    }
}
