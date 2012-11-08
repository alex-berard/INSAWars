using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Game
{
    public class Map
    {
        public struct Vector
        {
            int x;
            int y;
        }

        private Case[][] cases;

        public Map(int size)
        {
            cases = new Case[size][];

            for (int i = 0; i < size; i++)
            {
                cases[i] = new Case[size];
            }
        }
    }
}
