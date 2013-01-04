using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWrapper;
using INSAWars.Units;
using INSAWars.Game;

namespace INSAWars.Game
{
    public class GameBuilder
    {
        private MapGenerator mapGenerator;
        private Dictionary<string, ICivilization> players;
        private MapConfiguration mapConfig;

        public GameBuilder()
        {
            mapConfig = new MapConfiguration();
            players = new Dictionary<string, ICivilization>();
        }

        public void SetSize(string size)
        {
            if (size == "Small")
            {
                mapGenerator = new SmallMapGenerator();
            }
            else
            {
                mapGenerator = new MediumMapGenerator();
            }
        }

        public void AddPlayer(string name, ICivilization civ)
        {
            players.Add(name, civ);
        }

        public void RemovePlayer(string name)
        {
            players.Remove(name);
        }

        /// <summary>
        /// Sets the frequency of the given terrain.
        /// The sum of the frequencies must be 1.
        /// </summary>
        /// <param name="terrainIndex">Enum Terrain</param>
        /// <param name="frequency"></param>
        public void SetTerrainFrequency(int terrainIndex, double frequency)
        {
            mapConfig.terrains[terrainIndex] = frequency;
        }

        public void UseDefaultFrequencies()
        {
            mapConfig.terrains[(int) Terrains.DESERT] = 0.3;
            mapConfig.terrains[(int) Terrains.PLAIN] = 0.5;
            mapConfig.terrains[(int) Terrains.MOUNTAIN] = 0.2;
            mapConfig.decorators[(int) Decorators.FOOD] = 0.2;
            mapConfig.decorators[(int) Decorators.IRON] = 0.2;
        }

        /// <summary>
        /// Sets the probability for each case to have the given decorator.
        /// </summary>
        /// <param name="decoratorIndex">Enum Decorator</param>
        /// <param name="probability"></param>
        public void SetDecoratorProbability(int decoratorIndex, double probability)
        {
            mapConfig.decorators[decoratorIndex] = probability;
        }

        public Game Build()
        {
            Map map = mapGenerator.generate(mapConfig);

            List<Player> players = new List<Player>();

            foreach (var entry in this.players)
            {
                Player player = new Player(entry.Value, entry.Key);
                players.Add(player);
                initPlayer(player, map);
            }

            return new Game(map, players);
        }

        public Game LoadGame(string filename)
        {
            return null;
        }

        private void initPlayer(Player player, Map map)
        {
            Case position = map.FreePosition;
            player.AddCity(new City(position, player, "Main city of " + player.Name, map.TerritoryAround(position)));
        }
    }
}
