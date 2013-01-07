using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Game;

namespace INSAWars.Units
{
    [Serializable]
    public abstract class Student : Unit
    {
        public Student(Case location, Player player)
            : base(location, player)
        {
        }

        public override string ToString()
        {
            return "Student";
        }
    }
}
