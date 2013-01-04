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
        private Player player;
        private int population;
        
        private int food;
        private int iron;

        private int id;
        private string name;

        private List<Unit> pendingProductions;

        public int Food
        {
            get { return food; }
        }

        public int Iron
        {
            get { return iron; }
        }

        public Player Player
        {
            get { return player; }
        }

        public Case Position
        {
            get { return position; }
        }

        private AbstractUnitFactory Factory
        {
            get { return player.Civilization.UnitFactory; }
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
            this.player = player;
            this.name = name;
            this.cases = new List<Case>();
            this.cases.Add(this.position);
            this.pendingProductions = new List<Unit>();
        }

        public void AddCase(Case c)
        {
            cases.Add(c);
        }

        public void CapturedBy(Player invader)
        {
            player = invader;
            player.RemoveCity(this);
            invader.AddCity(this);
        }

        public void CancelProduction(int index)
        {
            pendingProductions.RemoveAt(index);
        }

        public void MakeStudent()
        {
            Student unit = player.Civilization.UnitFactory.CreateStudent(this);
            pendingProductions.Add(unit);
            food -= Factory.StudentFoodCost;
            iron -= Factory.StudentIronCost;
        }

        public bool CanMakeStudent()
        {
            return food >= Factory.StudentFoodCost && iron >= Factory.StudentIronCost;
        }

        public void MakeTeacher()
        {
            Teacher unit = player.Civilization.UnitFactory.CreateTeacher(this);
            pendingProductions.Add(unit);
            food -= Factory.TeacherFoodCost;
            iron -= Factory.TeacherIronCost;
        }

        public bool CanMakeTeacher()
        {
            return food >= Factory.TeacherFoodCost && iron >= Factory.TeacherIronCost;
        }

        public void MakeHead()
        {
            Teacher unit = player.Civilization.UnitFactory.CreateTeacher(this);
            pendingProductions.Add(unit);
            food -= Factory.HeadFoodCost;
            iron -= Factory.HeadIronCost;
        }

        public bool CanMakeHead()
        {
            return food >= Factory.HeadFoodCost && iron >= Factory.HeadIronCost;
        }

        public void NextTurn()
        {
            CollectResources();
            HandleProduction();
        }

        private void HandleProduction()
        {
            foreach (Unit unit in pendingProductions)
            {
                player.AddUnit(unit);
                position.AddUnit(unit);
            }

            pendingProductions = new List<Unit>();
        }

        private void CollectResources()
        {
            foreach (Case c in cases) {
                food += c.Food;
                iron += c.Iron;
            }
        }

        public void Destroy()
        {
            position.Free();
            player.RemoveCity(this);
        }
    }
}
