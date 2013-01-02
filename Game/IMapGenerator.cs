using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Game
{
    interface IMapGenerator
    {
        Map generate();
        void placePlayers(List<Player> players);
    }
}
