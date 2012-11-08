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
        private int maxPopulation;
        
        private int food;
        private int iron;

        private string name;

        private Unit pendingProduction;
        private int remainingTurns;

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
