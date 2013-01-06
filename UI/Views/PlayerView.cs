using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INSAWars.Game;
using INSAWars.MVVM;
using System.ComponentModel;

namespace UI.Views
{
    public class PlayerView : ObservableObject
    {
        private string _name;
        private int _citiesCount;

        public PlayerView(Player p)
        {
            p.PropertyChanged += new PropertyChangedEventHandler(delegate(object sender, PropertyChangedEventArgs args)
            {
                Name = ((Player)sender).Name;
            });
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
    }
}
