﻿using Cherimoya.Expressions;
using Dandelion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cherimoya.Reduction
{
    static class Funcs
    {
        public static bool IsConstant(this Expression e, double value)
        {
            return e is ConstantExpression &&
                    Types.IsConvertable((e as ConstantExpression).Value, typeof(double)) &&
                    Types.ConvertValue<double>((e as ConstantExpression).Value) == value;
        }
    }
}
