using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Units;

namespace INSAWars.Game
{
    public class Game
    {
        public static Random random = new Random();
        private List<Player> players;
        private Queue<Player> alivePlayers;
        private Map map;
        private int nbTurns;
        private Boolean over;

        public Player CurrentPlayer
        {
            get { return alivePlayers.Peek();  }
        }

        public Case GetCaseAt(int x, int y)
        {
            return map.GetCaseAt(x, y);
        }

        public Game(Map map, List<Player> players)
        {
            this.players = players;
            this.alivePlayers = new Queue<Player>(players);

            this.map = map;

            this.nbTurns = 0;
        }

        public void Attack(Unit unit, Case c)
        {
            if (c.Units.Count == 0)
            {

            }
            else
            {
                Unit opponent = c.GetAttackedUnit();
                unit.Attack(opponent);
            }
        }

        public bool CanAttack(Unit unit, Case c)
        {
            return false;
        }

        /// <summary>
        /// Builds a city on the case on which the given teacher stands.
        /// </summary>
        /// <param name="name">Name of the city.</param>
        /// <param name="teacher">Teacher building the city.</param>
        public void BuildCity(string name, Teacher teacher)
        {
            Player player = teacher.Player;
            Case position = teacher.Location;

            City city = new City(position, player, name);

            position.BuildCity(city);
            player.AddCity(city);
            teacher.Kill();
        }

        public bool CanBuildCity(Teacher teacher)
        {
            return false;
        }

        public void MoveUnit(Unit unit, Case to)
        {
            // TODO: Check that the case is accessible (not water, and not occupied by an enemy).
            // Check that the unit has enough movement points.
            unit.MoveTo(to);
        }

        public bool CanMoveUnit(Unit unit, Case to)
        {
            return false;
        }

        public void NextTurn()
        {
            Player player;

            int count = players.Count;
            for (int i = 0; i < count; i++)
            {
                player = alivePlayers.Dequeue();
                
                if (!player.IsDead)
                {
                    alivePlayers.Enqueue(player);
                }
            }


            if (players.Count <= 1)
            {
                over = true;
                return;
            }
            else
            {
                nbTurns++;
                player = alivePlayers.Dequeue();
                alivePlayers.Enqueue(player);
                player.NextTurn();
            }
        }

        static int Main(string[] args)
        {
            return 0;
        }
    }
}
