using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Game
{
    public class Player
    {
        private bool alive;
        private HashSet<City> cities;
        private Civilization civilization;
        private string name;
        private HashSet<Unit> units;

        public Civilization Civilization
        {
            get { return civilization; }
        }

        public Player(Civilization civilization, string name)
        {
            alive = true;
            cities = new HashSet<City>();
            this.civilization = civilization;
            this.name = name;
        }

        public void AddCity(City city)
        {
            cities.Add(city);
        }

        public void RemoveCity(City city)
        {
            cities.Remove(city);
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
            alive = false;

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
                unit.NextTurn();
            }
        }
    }
}
