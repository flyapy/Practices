﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practices.Commons
{
    public class Validate
    {
        public bool LengthBetween(string s, int min, int max)
        {
            return s != null && s.Length >= min && s.Length <= max;
        }
    }
}
