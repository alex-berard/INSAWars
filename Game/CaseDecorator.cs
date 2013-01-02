﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Units;

namespace INSAWars.Game
{
    /// <summary>
    /// Adds an amount of food or iron to a case.
    /// </summary>
    public abstract class CaseDecorator : Case
    {
        protected Case decoratedCase;

        public override int X
        {
            get { return decoratedCase.X; }
        }

        public override int Y
        {
            get { return decoratedCase.Y; }
        }

        public override List<Unit> Units
        {
            get { return decoratedCase.Units; }
        }

        public CaseDecorator(Case decoratedCase)
        {
            this.decoratedCase = decoratedCase;
        }

        public Case Clone(Case decoratedCase)
        {
            CaseDecorator case_ = (CaseDecorator)this.MemberwiseClone();
            case_.decoratedCase = decoratedCase;

            return case_;
        }

        public override Unit GetAttackedUnit()
        {
            return decoratedCase.GetAttackedUnit();
        }

        public override bool BuildCity(City city)
        {
            return decoratedCase.BuildCity(city);
        }

        public override bool Use(City city)
        {
            return decoratedCase.Use(city);
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
    }
}
