using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Game
{
    public class IronCaseDecorator : CaseDecorator
    {
        public int GetFood()
        {
            return decoratedCase.GetFood();
        }

        public int GetIron()
        {
            return decoratedCase.GetIron() + 2;
        }
    }
}
