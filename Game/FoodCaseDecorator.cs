using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Game
{
    public class FoodCaseDecorator : CaseDecorator
    {
        public override int Food
        {
            get { return decoratedCase.Food + 2; }
        }

        public override int Iron
        {
            get { return decoratedCase.Iron; }
        }

        public FoodCaseDecorator(Case decoratedCase)
            : base(decoratedCase) {}
    }
}
