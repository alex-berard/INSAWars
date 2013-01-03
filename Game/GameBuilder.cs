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
        
        private IMapGenerator mapGenerator;
        private Dictionary<string, ICivilization> players;
        private MapConfiguration mapConfig;

        public GameBuilder()
        {
            mapConfig = new MapConfiguration();
            players = new Dictionary<string, ICivilization>();
        }

        public void SetSize(string size)
        {
            if (size == "small")
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
                initPlayer(player, map.FreePosition);
            }

            return new Game(map, players);
        }

        private void initPlayer(Player player, Case position)
        {
            player.AddCity(new City(position, player, "Main city of " + player.Name));
        }
    }
}
