using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Units;

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

        public bool BuildCity(string name, Teacher teacher)
        {
            Player player = teacher.Player;
            Case position = teacher.CurrentCase;

            City city = new City(position, player, name);

            position.BuildCity(city);
            player.AddCity(city);

            return false;
        }

        public bool MoveUnit(Unit unit, Case to)
        {
            // TODO: Verify that the case is empty, and that the unit has enough movement points.
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
            Console.Out.WriteLine("Banane");

            var gen = new MediumMapGenerator();
            Map m = gen.generate();
            Console.Out.WriteLine(m);

            while (true) ;
            return 0;
        }
    }
}
