using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Game
{
    public class Game
    {
        private Queue<Player> players;
        private Map map;
        private int nbTurns;

        public Game(Map map, List<Player> players)
        {
            this.players = new Queue<Player>();

            foreach (Player player in players) {
                this.players.Enqueue(player);
            }

            this.map = map;

            this.nbTurns = 0;
        }

        public void Attack(Unit unit, Case c) {

        }

        public void BuildCity(Teacher teacher)
        {

        }

        public void NextTurn()
        {

        }

        public void Over()
        {

        }
    }
}
