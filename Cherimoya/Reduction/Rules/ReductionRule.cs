using Cherimoya.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cherimoya.Reduction.Rules
{
    abstract class ReductionRule
    {
        public abstract Expression Reduce(Expression root, ExpressionReductor reductor);
    }
}
