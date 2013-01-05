using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Game;

namespace INSAWars.Units.Info
{
    [Serializable]
    class InfoUnitFactory : AbstractUnitFactory
    {
        public override Head CreateHead(City city)
        {
            return new InfoHead(city.Position, city.Player);
        }

        public override Student CreateStudent(City city)
        {
            return new InfoStudent(city.Position, city.Player);
        }

        public override Teacher CreateTeacher(City city)
        {
            return new InfoTeacher(city.Position, city.Player);
        }
    }
}
