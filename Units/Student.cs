﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Units
{
    abstract class Student : Unit
    {
        public virtual double IronCost { get { return 100.0; } }
    }
}