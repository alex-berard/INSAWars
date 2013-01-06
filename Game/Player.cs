#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Units;
#endregion

namespace INSAWars.Game
{
    [Serializable]
    public class Player
    {
        #region fields
        private bool isDead;
        private HashSet<City> cities;
        private ICivilization civilization;
        private string name;
        private HashSet<Unit> units;
        #endregion

        #region properties
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
        }

        public ICivilization Civilization
        {
            get { return civilization; }
        }
        #endregion

        #region constructors
        public Player(ICivilization civilization, string name)
        {
            isDead = false;
            cities = new HashSet<City>();
            units = new HashSet<Unit>();
            this.civilization = civilization;
            this.name = name;
            Head = null;
        }
        #endregion

        #region methods
        public void AddCity(City city)
        {
            cities.Add(city);
        }

        public void RemoveCity(City city)
        {
            cities.Remove(city);

            if (LoseCondition())
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

            if (LoseCondition())
            {
                Lose();
            }
        }

        private bool LoseCondition()
        {
            return cities.Count == 0 && !units.ToList().Exists(item => item is Teacher);
        }

        public void Lose()
        {
            isDead = true;

            // If the player has surrendered
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
        #endregion
    }
}
