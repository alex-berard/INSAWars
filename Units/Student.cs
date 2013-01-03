using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Game;

namespace INSAWars.Units
{
    public abstract class Student : Unit
    {
        public override uint IronCost { get { return 100; } }

        public Student(Case location, Player player)
            : base(location, player)
        {
        }
    }
}
