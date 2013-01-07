using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Game
{
    [Serializable]
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

        public override string Texture
        {
            get { return decoratedCase.Texture; }
        }

        public FoodCaseDecorator(Case decoratedCase)
            : base(decoratedCase) {}

        public override string ToString()
        {
            return decoratedCase.ToString();
        }
    }
}
