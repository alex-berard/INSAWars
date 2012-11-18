using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Units;

namespace INSAWars.Game
{
    /// <summary>
    /// 
    /// </summary>
    public class City
    {
        private List<Case> cases;
        private Case position;
        private Player occupant;
        private int population;
        
        private int food;
        private int iron;

        private int id;
        private string name;

        private Dictionary<Unit, int> pendingProductions;

        public Case Position
        {
            get { return position; }
        }

        /// <summary>
        /// Creates a new city on a given case.
        /// </summary>
        /// <param name="position">The position of the city.</param>
        /// <param name="player">The player to whom the city belongs.</param>
        /// <param name="name">The name of the city (defined by the player).</param>
        public City(Case position, Player player, string name)
        {
            this.position = position;
            this.occupant = player;
            this.name = name;
            this.pendingProductions = new Dictionary<Unit, int>();
        }

        public void AddCase(Case c)
        {
            cases.Add(c);
        }

        public void CapturedBy(Player invader)
        {
            occupant = invader;
        }

        public bool MakeStudent()
        {
            Student unit = occupant.Civilization.UnitFactory.CreateStudent(this);
            return false;
        }

        public bool MakeTeacher()
        {
            return false;
        }

        public bool MakeHead()
        {
            return false;
        }

        /// <summary>
        /// Handles the production for the next turn.
        /// </summary>
        public void NextTurn()
        {
            var productions = new Dictionary<Unit, int>();

            foreach (var production in pendingProductions)
            {
                int remainingTurns = production.Value - 1;
                Unit unit = production.Key;

                if (remainingTurns == 0)
                {
                    occupant.AddUnit(unit);
                    position.AddUnit(unit);
                }
                else
                {
                    productions.Add(unit, remainingTurns);
                }
            }

            pendingProductions = productions;
        }

        public void Destroy()
        {
            position.Free();
            occupant.RemoveCity(this);
        }
    }
}
