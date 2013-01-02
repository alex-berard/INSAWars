using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWrapper;
using INSAWars.Units;

namespace INSAWars.Game
{
    public class GameBuilder
    {
        private string size;
        private IMapGenerator mapGenerator;
        private Dictionary<string, ICivilization> players;
        private Dictionary<string, double> terrainFrequencies;

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

        public void SetFrequency(string terrain, double frequency)
        {

        }

        public Game Build()
        {
            Map map = mapGenerator.generate();

            List<Player> players = new List<Player>();

            foreach (var entry in this.players)
            {
                Player player = new Player(entry.Value, entry.Key);
                players.Add(player);
            }

            return new Game(map, players);
        }
    }
}
