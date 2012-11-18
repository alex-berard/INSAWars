using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Game;

namespace INSAWars.Units
{
    public abstract class Student : Unit
    {
        public override double IronCost { get { return 100.0; } }

        public Student(Case location)
            : base(location)
        {
        }
    }
}
