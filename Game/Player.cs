using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Units;

namespace INSAWars.Game
{
    public class Player
    {
        private bool alive;
        private HashSet<City> cities;
        private ICivilization civilization;
        private string name;
        private HashSet<Unit> units;

        public string Name
        {
            get { return name; }
        }

        public ICivilization Civilization
        {
            get { return civilization; }
        }

        public Player(ICivilization civilization, string name)
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
                unit.Reset();
            }
        }

        /// <summary>
        /// Returns all the units visible by the current player
        /// (the units in the field of view of this player's units and cities).
        /// Cities have a field of view of 3 cases, units of 2 cases.
        /// </summary>
        /// <returns></returns>
        public List<Unit> GetVisibleUnits()
        {
            return null;
        }
    }
}
