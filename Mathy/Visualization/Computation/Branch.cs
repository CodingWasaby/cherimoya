using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.Visualization.Computation
{
    class Branch
    {
        public Node Node { get; set; }

        public double LineX1 { get; set; }

        public double LineY1 { get; set; }

        public double LineX2 { get; set; }

        public double LineY2 { get; set; }

        public GraphicsPath ArrowPath { get; set; }
    }
}
