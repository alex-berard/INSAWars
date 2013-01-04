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

        public Map Map
        {
            get { return map; }
        }

        public Player CurrentPlayer
        {
            get { return alivePlayers.Peek();  }
        }

        public Game(Map map, List<Player> players)
        {
            this.players = players;
            this.alivePlayers = new Queue<Player>(players);

            this.map = map;

            this.over = false;
            this.nbTurns = 0;
        }

        public void Attack(Unit unit, Case c)
        {
            if (c.HasUnits)
            {
                // Seize the territory, move the unit onto it
                Unit opponent = c.Unit;
                unit.Attack(opponent);

                if (!c.HasUnits)
                {
                    // Seize the territory, move the unit onto it
                }
            }
            else
            {
                // Seize the territory, move the unit onto it
            }

            unit.HasAttacked = true;
        }

        public bool CanAttack(Unit unit, Case c)
        {
            return !unit.HasAttacked && unit.AttackPoints > 0 && !c.IsFree && c.Occupant != unit.Player;
        }

        public void MakeStudent(City city)
        {
            city.MakeStudent();
        }

        public bool CanMakeStudent(City city)
        {
            return city.CanMakeStudent();
        }

        public void MakeTeacher(City city)
        {
            city.MakeTeacher();
        }

        public bool CanMakeTeacher(City city)
        {
            return city.CanMakeTeacher();
        }

        public void MakeHead(City city)
        {
            city.MakeHead();
        }

        public bool CanMakeHead(City city)
        {
            return city.CanMakeHead();
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
            return !teacher.Location.HasCity;
        }

        public void MoveUnit(Unit unit, Case destination)
        {
            unit.MoveTo(destination);
        }

        public bool CanMoveUnit(Unit unit, Case destination)
        {
            // TODO: Check that the case is accessible (not water, and not occupied by an enemy).
            // Check that the unit has enough movement points.
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

        /// <summary>
        /// Tells wether or not the given case is visible by the current player.
        /// </summary>
        /// <param name="c">Given case</param>
        /// <returns>True if the case is in the field of view of the current player, false otherwise.</returns>
        public bool IsVisible(Case c)
        {
            return false;
        }

        public void Save(string filename)
        {

        }

        private void ExpandCity(City city)
        {

        }
    }
}
