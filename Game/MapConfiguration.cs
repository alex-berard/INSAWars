using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INSAWars.Game
{
    public class MapConfiguration
    {
        public double[] terrains;
        public double[] decorators;
        public static int[] inaccessibleTerrains = new int[] { (int) Terrains.WATER };

        public MapConfiguration()
        {
            terrains = new double[4];
            Array.Clear(terrains, 0, terrains.Length);
            decorators = new double[2];
            Array.Clear(decorators, 0, decorators.Length);
        }

        public static Case GetCase(int terrainIndex, int x, int y)
        {
            switch (terrainIndex)
            {
                case (int)Terrains.WATER:
                    return new Water(x, y);
                case (int)Terrains.DESERT:
                    return new Desert(x, y);
                case (int)Terrains.MOUNTAIN:
                    return new Mountain(x, y);
                default:
                    return new Plain(x, y);
            }
        }

        public static Case GetDecorator(int decoratorIndex, Case decoratedCase)
        {
            switch (decoratorIndex)
            {
                case (int)Decorators.IRON:
                    return new IronCaseDecorator(decoratedCase);
                default:
                    return new FoodCaseDecorator(decoratedCase);
            }
        }
    }

    public enum Terrains
    {
        WATER = 0,
        DESERT = 1,
        PLAIN = 2,
        MOUNTAIN = 3
    }

    public enum Decorators
    {
        FOOD = 0,
        IRON = 1
    }
}
