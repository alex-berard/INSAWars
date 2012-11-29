using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace INSAWars.Game
{
    public class Map
    {
        private Case[][] cases;

        public Map(Case[][] cases)
        {
            this.cases = cases;
        }

        public override string ToString()
        {
            var builder = new StringBuilder("Map: \n");
            
            foreach (Case[] _cases in cases)
            {
                foreach (Case _case in _cases)
                {
                    builder.AppendLine(_case.ToString());
                }
            }

            return builder.ToString();
        }
    }
}
