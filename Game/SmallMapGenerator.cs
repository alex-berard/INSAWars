using System;
using System.Collections.Generic;
using INSAWrapper;

namespace INSAWars.Game
{
    public class SmallMapGenerator : IMapGenerator
    {
        private const int SIZE = 25;

        public override Map generate(MapConfiguration config)
        {
            PerlinMapWrapper perlinMap = new PerlinMapWrapper(SIZE, config.terrains, config.decorators);
            Case[,] cases = new Case[SIZE, SIZE];
            List<Case> startingPositions = new List<Case>();

            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    int terrainIndex = perlinMap.GetTerrain(i, j);
                    int decoratorIndex = perlinMap.GetDecorator(i, j);
                    cases[i, j] = MapConfiguration.GetDecorator(decoratorIndex, MapConfiguration.GetCase(terrainIndex, i, j));
                }
            }

            int[,] positions = perlinMap.GetStartingPositions(MapConfiguration.inaccessibleTerrains);
            for (int i = 0; i < positions.Length; i++)
            {
                int x = positions[i, 0];
                int y = positions[i, 1];
                startingPositions.Add(cases[x, y]);
            }

            return new Map(cases, startingPositions);
        }
    }
}
