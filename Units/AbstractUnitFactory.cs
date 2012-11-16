using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Units
{
    abstract class AbstractUnitFactory
    {
        public abstract Head CreateHead(City city);
        public abstract Student CreateStudent(City city);
        public abstract Teacher CreateTeacher(City city);
    }
}
