using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Units;
using INSAWars.Utils;

namespace INSAWars.Game
{
    public class Game
    {
        private InfiniteQueue<Player> players;
        private Map map;
        private int nbTurns;

        public Game(Map map, List<Player> players)
        {
            this.players = new InfiniteQueue<Player>(players);

            this.map = map;

            this.nbTurns = 0;
        }

        public bool Attack(Unit unit, Case c) {
            return false;
        }

        /// <summary>
        /// Builds a city on the case on which the given teacher stands.
        /// </summary>
        /// <param name="name">Name of the city.</param>
        /// <param name="teacher">Teacher building the city.</param>
        /// <returns></returns>
        public void BuildCity(string name, Teacher teacher)
        {
            // TODO: kill the teacher. Check that a city does not already exist on this case.
            Player player = teacher.Player;
            Case position = teacher.CurrentCase;

            City city = new City(position, player, name);

            position.BuildCity(city);
            player.AddCity(city);
        }

        public bool MoveUnit(Unit unit, Case to)
        {
            // TODO: Check that the case is accessible (not water, and not occupied by an enemy).
            // Check that the unit has enough movement points.
            return unit.MoveTo(to);
        }

        public void NextTurn()
        {
            players.next().NextTurn();
        }

        public void Over()
        {

        }

        static int Main(string[] args)
        {
            return 0;
        }
    }
}
