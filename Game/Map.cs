using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using INSAWars.Units;

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

            for (int i = -radius; i <= radius; i++)
            {
                int x = position.X + i;

                if (x < 0 || x > Size - 1)
                {
                    continue;
                }

                int offset = (int)Math.Sqrt(radius - i * i);

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

        public Case BestCase(Case position, int radius)
        {
            int bestWeight = 0;
            Case bestCase = null;

            foreach (Case c in cases)
            {
                if (c.DistanceTo(position) <= radius)
                {
                    int weight = 0;

                    foreach (Case field in TerritoryAround(c, City.radius))
                    {
                        weight += field.Food;
                        weight += field.Iron;
                    }

                    if (weight > bestWeight)
                    {
                        bestCase = c;
                        weight = bestWeight;
                    }
                }
            }

            return bestCase;
        }

        public List<Case> BestCases(Teacher teacher)
        {
            var casesWeights = new List<KeyValuePair<Case, double>>();

            foreach (Case c in cases)
            {
                if (c.HasCity || (c.Occupant != null && c.Occupant != teacher.Player))
                {
                    continue;
                }

                if (c.DistanceTo(teacher.Location) <= teacher.MovementPoints * 3)
                {
                    double weight = 0;

                    foreach (Case field in TerritoryAround(c, City.radius))
                    {
                        if (c.Occupant != null && c.Occupant != teacher.Player)
                        {
                            weight -= 10;
                        }
                        else if (c.HasCity)
                        {
                            weight -= 5;
                        }
                        else if (c.IsUsed)
                        {
                            weight -= 2;
                        }
                        else
                        {
                            weight += field.Units.Count();
                            weight += field.Food;
                            weight += field.Iron;
                        }
                    }

                    if (weight > 0)
                    {
                        casesWeights.Add(new KeyValuePair<Case, double>(c, weight));
                    }
                }
            }

            casesWeights.Sort((a, b) => a.Value.CompareTo(b.Value));

            var bestCases = new List<Case>();

            for (int i = 0; i < 3 && i < casesWeights.Count; i++)
            {
                bestCases.Add(casesWeights[i].Key);
            }

            return bestCases;
        }

        public Map(Case[,] cases)
        {
            this.cases = cases;
            startingPositions = new Stack<Case>();

            startingPositions.Push(BestCase(cases[0, 0], 5));
            startingPositions.Push(BestCase(cases[Size - 1, Size - 1], 5));
            startingPositions.Push(BestCase(cases[Size - 1, 0], 5));
            startingPositions.Push(BestCase(cases[0, Size - 1], 5));
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
