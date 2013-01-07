using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INSAWars.Game;
using INSAWars.MVVM;
using System.ComponentModel;
using INSAWars.Units;
using INSAWars.Units.Info;

namespace UI.Views
{
    public class CaseView : ObservableObject
    {
        private string _type;
        private int _food;
        private int _iron;
        private List<Unit> _units;

        public CaseView(Case c)
        {           
            Units = c.Units.ToList();
            Type = c.ToString();
            Food = c.Food;
            Iron = c.Iron;
        }

        public string Type
        {
            get { return _type; }
            set
            {
                SetProperty(ref _type, value);
            }
        }

        public int Food
        {
            get { return _food; }
            set
            {
                SetProperty(ref _food, value);
            }
        }

        public int Iron
        {
            get { return _iron; }
            set
            {
                SetProperty(ref _iron, value);
            }
        }

        public List<Unit> Units
        {
            get { return _units; }
            set
            {
                SetProperty(ref _units, value);
            }
        }
    }
}
