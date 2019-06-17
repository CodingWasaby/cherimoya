using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.Tree
{
    public class VariableBranch : TreeBranch
    {
        public string VariableName { get; set; }

        public object Value { get; set; }
    }
}
