﻿#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Units;
#endregion

namespace INSAWars.Game
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class City
    {
        #region fields
        public const int radius = 3;

        private List<Case> territory;
        private List<Case> fields;
        private Case position;
        private Player player;
        private int population;
        
        private int food;
        private int requiredFood;
        private int iron;

        private string name;

        private List<Unit> pendingProductions;
        #endregion

        #region properties
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
        #endregion

        #region constructors
        /// <summary>
        /// Creates a new city on a given case.
        /// </summary>
        /// <param name="position">The position of the city.</param>
        /// <param name="player">The player to whom the city belongs.</param>
        /// <param name="name">The name of the city (defined by the player).</param>
        public City(Case position, Player player, string name, List<Case> territory)
        {
            this.position = position;
            this.player = player;
            this.name = name;
            this.fields = new List<Case>();
            this.fields.Add(this.position);
            this.pendingProductions = new List<Unit>();
            this.territory = territory;
            population = 1;
            food = 0;
            iron = 0;
            requiredFood = 10;
        }
        #endregion

        #region methods
        public void Invade(Player invader)
        {
            player = invader;
            player.RemoveCity(this);
            invader.AddCity(this);

            foreach (Case field in fields.ToList())
            {
                if (field.HasUnits)
                {
                    // The case is occupied by the enemy and cannot be used as a field.
                    RemoveField(field);
                    field.Free();
                }
                else
                {
                    // Changes the occupant of the field.
                    field.Use(this);
                }
            }
        }

        public void CancelProduction(int index)
        {
            pendingProductions.RemoveAt(index);
        }

        public void MakeStudent()
        {
            Student unit = player.Civilization.UnitFactory.CreateStudent(position, player);
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
            Teacher unit = player.Civilization.UnitFactory.CreateTeacher(position, player);
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
            Head unit = player.Civilization.UnitFactory.CreateHead(position, player);
            pendingProductions.Add(unit);
            food -= Factory.HeadFoodCost;
            iron -= Factory.HeadIronCost;
            player.Head = unit;
        }

        public bool CanMakeHead()
        {
            return player.Head == null && food >= Factory.HeadFoodCost && iron >= Factory.HeadIronCost;
        }

        public void Expand()
        {
            List<Case> freeTerritory = new List<Case>();

            foreach (Case c in territory)
            {
                if (c.IsFree && !c.HasUnits || c.Occupant != Player)
                {
                    freeTerritory.Add(c);
                }
            }

            if (freeTerritory.Count > 0)
            {
                int r = Game.random.Next(freeTerritory.Count);
                Case field = freeTerritory[r];

                fields.Add(field);
                field.Use(this);

                population++;
                food -= requiredFood;
                requiredFood += requiredFood / 2;
            }
        }

        public void RemoveField(Case field)
        {
            population--;
            requiredFood = requiredFood * 2 / 3;
            fields.Remove(field);

        }

        public bool CanExpand()
        {
            return food >= requiredFood && fields.Count < territory.Count;
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
            foreach (Case c in fields) {
                food += c.Food;
                iron += c.Iron;
            }
        }

        public void Destroy()
        {
            position.Free();
            player.RemoveCity(this);
        }

        public override string ToString()
        {
            return "City \"" + name + "\" of [" + Player + "] at coordinates [" + position.X + ", " + position.Y + "]";
        }
        #endregion
    }
}
