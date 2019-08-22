using Mathy.Planning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.Libs
{
    public class CustomFuncFactory
    {

        //public static int Loop(string steps, int times)
        //{
        //    var a = steps;
        //    return times;
        //}

        public static int Loop(string steps, int times, object ec)
        {
            EvaluationContext e = ec as EvaluationContext;
            e.DoStep(steps, times);

            return times;
        }
    }
}
