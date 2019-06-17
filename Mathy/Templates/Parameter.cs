﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.Templates
{
    public class Parameter
    {
        public string Name { get; set; }

        public DataType Type { get; set; }

        public object Default { get; set; }

        public string Unit { get; set; }

        public string Description { get; set; }
    }
}
