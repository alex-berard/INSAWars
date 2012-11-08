using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Game
{
    public class FoodCaseDecorator : CaseDecorator
    {
        public int GetFood()
        {
            return decoratedCase.GetFood() + 2;
        }

        public int GetIron()
        {
            return decoratedCase.GetIron();
        }
    }
}
