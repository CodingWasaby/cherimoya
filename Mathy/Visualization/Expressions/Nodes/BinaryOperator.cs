using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.Visualization.Expressions.Nodes
{
    public enum BinaryOperator
    {
        None,
        Add,
        Subtract,
        Multiply,
        Divide,
        Assign,
        LessThan,
        LessEqualThan,
        GreaterThan,
        GreaterEqualThan,
        Equal,
        NotEqual
    }
}
