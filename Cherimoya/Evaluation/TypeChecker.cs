using Cherimoya.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cherimoya.Evaluation
{
    public abstract class TypeChecker
    {
        public abstract void PerformTypeChecking(Expression expression, TypeCheckingContext context);
    }
}
