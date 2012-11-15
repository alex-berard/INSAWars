using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Units.EII
{
    class InfoTeacher : Teacher
    {
        public virtual double DefensePoints { get { return 1.0; } }
        public virtual double MovementPoints { get { return 3.0; } }
        public virtual double HitPoints { get { return 1.0; } }
    }
}
