using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INSAWars.Game;
using INSAWars.MVVM;
using INSAWars.Units;
using System.ComponentModel;

namespace UI.Views
{
    public class PlayerView : ObservableObject
    {
        private string _name;
        private int _citiesCount;
        private int _headCount;
        private int _teachersCount;
        private int _studentsCount;

        public PlayerView(Player p)
        {
            p.PropertyChanged += new PropertyChangedEventHandler(delegate(object sender, PropertyChangedEventArgs args)
            {
                switch (args.PropertyName)
                {
                    case "Name":
                        Name = ((Player)sender).Name;
                        break;
                    case "CitiesCount":
                        CitiesCount = ((Player)sender).CitiesCount;
                        break;
                    case "Units":
                        TeachersCount = ((Player)sender).Units.Where(u => u is Teacher).Count();
                        StudentsCount = ((Player)sender).Units.Where(u => u is Student).Count();
                        HeadCount = (((Player)sender).HasHead ? 1 : 0);
                        break;
                    default:
                        break;
                }
                
            });

            Name = p.Name;
            CitiesCount = p.CitiesCount;
            TeachersCount = p.Units.Where(u => u is Teacher).Count();
            StudentsCount = p.Units.Where(u => u is Student).Count();
            HeadCount = (p.HasHead ? 1 : 0);
        }

        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
            }
        }

        public int CitiesCount
        {
            get { return _citiesCount; }
            set
            {
                SetProperty(ref _citiesCount, value);
            }
        }

        public int TeachersCount
        {
            get { return _teachersCount;  }
            set
            {
                SetProperty(ref _teachersCount, value);
            }
        }

        public int StudentsCount
        {
            get { return _studentsCount; }
            set
            {
                SetProperty(ref _studentsCount, value);
            }
        }

        public int HeadCount
        {
            get { return _headCount; }
            set
            {
                SetProperty(ref _headCount, value);
            }
        }
    }
}
