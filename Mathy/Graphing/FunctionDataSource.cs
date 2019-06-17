using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.Graphing
{
    public class FunctionDataSource : ContinousDataSource
    {
        public Func<double, double> Function;

        protected override double Map(double x)
        {
            return Function.Invoke(x);
        }
    }
}
