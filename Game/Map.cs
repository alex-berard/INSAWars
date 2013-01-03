using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace INSAWars.Game
{
    public class Map
    {
        private Case[,] cases;
        private Stack<Case> startingPositions;

        public Case GetCaseAt(int x, int y)
        {
            return cases[x, y];
        }

        public Case FreePosition
        {
            get { return startingPositions.Pop(); }
        }

        public Map(Case[,] cases, List<Case> startingPositions)
        {
            this.cases = cases;
            this.startingPositions = new Stack<Case>(startingPositions);
        }

        public override string ToString()
        {
            var builder = new StringBuilder("Map: \n");
            
            foreach (Case _case in cases)
            {
                builder.AppendLine(_case.ToString());
            }

            return builder.ToString();
        }
    }
}
