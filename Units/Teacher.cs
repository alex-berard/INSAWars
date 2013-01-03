using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Game;

namespace INSAWars.Units
{
    public class Teacher : Unit
    {
        public override uint IronCost { get { return 60; } }

        public Teacher(Case location, Player player)
            : base(location, player)
        {
        }

        public bool CanBuildCity()
        {
            return false;
        }

        public void BuildCity()
        {

        }
    }
}
