﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Game
{
    public interface IMapGenerator
    {
        public Map generate();
    }
}