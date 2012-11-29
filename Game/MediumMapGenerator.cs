using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Game
{
    public class MediumMapGenerator : IMapGenerator
    {
        private const int SIZE = 25;
        private static Case[] casesInstances = { new Mountain(), new Desert(), new Plain() };
        private static Random generator = new Random();

        public Map generate()
        {
            
            var generator = new Random();

            var cases = new Case[SIZE][];

            for (int i = 0; i < SIZE; i++)
            {
                cases[i] = new Case[SIZE];

                for (int j = 0; j < SIZE; j++)
                {
                    cases[i][j] = RandomCase(i, j);
                }
            }

            return new Map(cases);
        }

        private static Case RandomCase(int x, int y)
        {
            int i = generator.Next(casesInstances.Length);
            return (Case) casesInstances[i].Clone(x, y);
        }
    }
}
