﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Game
{
    public class Plain : Case
    {
        public override int Food
        {
            get { return 3; }
        }

        public override int Iron
        {
            get { return 1; }
        }

        public Plain(int x, int y) : base(x, y) { }

        public override string ToString()
        {
            return "Plain (" + x + ", " + y + ")";
        }
    }
}
