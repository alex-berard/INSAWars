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

        public bool Over { get { return over; } }

        public int NbTurns { get { return nbTurns; } }

        public Map Map { get { return map; } }

        public Player CurrentPlayer { get { return alivePlayers.Peek(); } }

        /// <summary>
        /// Makes a new game with the given map and the given players.
        /// Players play turn by turn until only one player remains.
        /// </summary>
        /// <param name="map">Map of the game.</param>
        /// <param name="players">Players of the game.</param>
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
                Unit opponent = c.MostDefensiveUnit;
                unit.Attack(opponent);

                // Seize the territory, move the unit onto it.
                if (!c.HasUnits)
                {
                    unit.MoveTo(c);
                }
            }
            else
            {
                unit.MoveTo(c);
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

            City city = new City(position, player, name, map.TerritoryAround(position, 5));

            position.BuildCity(city);
            player.AddCity(city);

            // The teacher sacrifices himself to build the city (may he rest in peace).
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

        /// <summary>
        /// Makes the game go to the next turn.
        /// Changes the current player, and updates the state of the game.
        /// </summary>
        public void NextTurn()
        {
            Player player;

            int count = players.Count;
            // Updates the queue of alive players (remove the dead ones).
            for (int i = 0; i < count; i++)
            {
                player = alivePlayers.Dequeue();
                
                if (!player.IsDead)
                {
                    alivePlayers.Enqueue(player);
                }
            }

            // If there are less than 1 alive player left, then the game is over.
            if (alivePlayers.Count <= 1)
            {
                over = true;
                return;
            }
            else
            {
                nbTurns++;
                // Puts the player to the end of the queue.
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
            foreach (Case _c in map.TerritoryAround(c, 3))
            {
                if (_c.HasCity && _c.Occupant == CurrentPlayer)
                {
                    return true;
                }
            }

            foreach (Case _c in map.TerritoryAround(c, 2))
            {
                if (_c.HasUnits && _c.Occupant == CurrentPlayer)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Saves the current game state into the given file.
        /// </summary>
        /// <param name="filename">Location of the save file.</param>
        public void Save(string filename)
        {

        }
    }
}
