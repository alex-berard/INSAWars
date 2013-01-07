using System;
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

        public int CitiesCount
        {
            get { return Cities.Count(); }
        }

        public List<Unit> Units
        {
            get { return units.ToList(); }
        }

        public Head Head { get; set; }

        public bool HasHead
        {
            get { return Head != null; }
        }

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
            OnPropertyChanged("CitiesCount");
            OnPropertyChanged("Units");
        }

        public void AddCity(City city)
        {
            cities.Add(city);
            OnPropertyChanged("CitiesCount");
        }

        public void RemoveCity(City city)
        {
            cities.Remove(city);
            OnPropertyChanged("CitiesCount");

            // When a player has no city left he loses the game.
            if (cities.Count == 0)
            {
                Lose();
            }
        }

        public void AddUnit(Unit unit)
        {
            units.Add(unit);
            OnPropertyChanged("Units");
        }

        public void RemoveUnit(Unit unit)
        {
            units.Remove(unit);
            OnPropertyChanged("Units");
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
