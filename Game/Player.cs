﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Units;
using INSAWars.MVVM;
using System.ComponentModel;

namespace INSAWars.Game
{
    [Serializable]
    public class Player : ObservableObject
    {
        private bool isDead;
        private HashSet<City> cities;
        private ICivilization civilization;
        private string name;
        private HashSet<Unit> units;

        public List<City> Cities
        {
            get { return cities.ToList(); }
        }

        public Head Head { get; set; }

        public bool IsDead
        {
            get { return isDead; }
        }

        public string Name
        {
            get { return name; }
            set
            {
                SetProperty(ref name, value);
            }
        }

        public ICivilization Civilization
        {
            get { return civilization; }
        }

        public Player(ICivilization civilization, string name)
        {
            isDead = false;
            cities = new HashSet<City>();
            units = new HashSet<Unit>();
            this.civilization = civilization;
            Name = name;
            Head = null;
        }

        public void AddCity(City city)
        {
            cities.Add(city);
        }

        public void RemoveCity(City city)
        {
            cities.Remove(city);

            // When a player has no city left he loses the game.
            if (cities.Count == 0)
            {
                Lose();
            }
        }

        public void AddUnit(Unit unit)
        {
            units.Add(unit);
        }

        public void RemoveUnit(Unit unit)
        {
            units.Remove(unit);
        }

        public void Lose()
        {
            isDead = true;

            foreach (City city in cities)
            {
                city.Destroy();
            }

            foreach (Unit unit in units)
            {
                unit.Kill();
            }
        }

        public void NextTurn()
        {
            foreach (City city in cities)
            {
                city.NextTurn();
            }

            foreach (Unit unit in units)
            {
                unit.Reset();
            }
        }

        public override bool Equals(object obj)
        {
            Player playerObj = obj as Player;

            return playerObj != null && name == playerObj.name;
        }

        public override string ToString()
        {
            return "Player " + name + " (" + (isDead ? "dead" : "alive") + ")";
        }
    }
}
