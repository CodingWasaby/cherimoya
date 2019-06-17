using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.Tree
{
    public class ExpressionTree
    {
        private ExpressionBranch primaryBranch;
        public ExpressionBranch PrimaryBranch
        {
            get { return primaryBranch; }
            set
            {
                primaryBranch = value;
                primaryBranch.IsPrimaryBranch = true;
            }
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Angle { get; set; }
    }
}
