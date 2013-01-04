using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Game;

namespace INSAWars.Units
{
    public abstract class AbstractUnitFactory
    {
        public virtual int HeadIronCost { get { return 200; } }
        public virtual int StudentIronCost { get { return 100; } }
        public virtual int TeacherIronCost { get { return 60; } }
        public virtual int HeadFoodCost { get { return 0; } }
        public virtual int StudentFoodCost { get { return 0; } }
        public virtual int TeacherFoodCost { get { return 0; } }

        public abstract Head CreateHead(City city);
        public abstract Student CreateStudent(City city);
        public abstract Teacher CreateTeacher(City city);
    }
}
