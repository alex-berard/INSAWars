using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Game
{
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

        private Unit pendingProduction;
        private int remainingTurns;

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
            if (pendingProduction != null)
            {
                remainingTurns--;

                if (remainingTurns == 0)
                {
                    occupant.AddUnit(pendingProduction);
                    position.AddUnit(pendingProduction);
                    pendingProduction = null;
                }
            }
        }

        public void Destroy()
        {
            position.Free();
            occupant.RemoveCity(this);

            // Kill the units created in this city?
        }
    }
}
