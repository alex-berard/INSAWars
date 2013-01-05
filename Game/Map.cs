using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace INSAWars.Game
{
    [Serializable]
    public class Map
    {
        private Case[,] cases;
        private Stack<Case> startingPositions;

        public int Size
        {
            get { return cases.GetLength(0); }
        }

        public Case GetCaseAt(int x, int y)
        {
            return cases[x, y];
        }

        public Case FreePosition
        {
            get { return startingPositions.Pop(); }
        }

        public List<Case> TerritoryAround(Case position, int radius)
        {
            List<Case> territory = new List<Case>();

            for (int x = Math.Max(0, position.X - radius); x <= Math.Min(Size - 1, position.X + radius); x++)
            {
                int offset = (int)Math.Sqrt(radius - x * x);

                for (int y = Math.Max(0, position.Y - offset); y <= Math.Min(Size - 1, position.Y + offset); y++)
                {
                    Case c = GetCaseAt(x, y);

                    if (!(c is Water))
                    {
                        territory.Add(GetCaseAt(x, y));
                    }
                }
            }

            return territory;
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
