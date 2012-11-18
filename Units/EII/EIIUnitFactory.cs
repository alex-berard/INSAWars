using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Game;

namespace INSAWars.Units.EII
{
    class EIIUnitFactory : AbstractUnitFactory
    {
        public override Head CreateHead(City city)
        {
            return new EIIHead(city.Position);
        }

        public override Student CreateStudent(City city)
        {
            return new EIIStudent(city.Position);
        }

        public override Teacher CreateTeacher(City city)
        {
            return new EIITeacher(city.Position);
        }
    }
}
