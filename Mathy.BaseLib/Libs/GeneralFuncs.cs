using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.BaseLib.Libs
{
    public class GeneralFuncs
    {
        public static double sin(double d)
        {
            return Math.Sin(d);
        }

        public static double cos(double d)
        {
            return Math.Cos(d);
        }

        public static double tan(double d)
        {
            return Math.Tan(d);
        }

        public static double ctg(double d)
        {
            return 1 / Math.Tan(d);
        }

        public static double asin(double d)
        {
            return Math.Asin(d);
        }

        public static double acos(double d)
        {
            return Math.Acos(d);
        }

        public static double atan(double d)
        {
            return Math.Atan(d);
        }

        public static double actg(double d)
        {
            return Math.Atan(1 / d);
        }

        public static double pow(double x, double y)
        {
            return Math.Pow(x, y);
        }

        public static double root(double x, double y)
        {
            return Math.Pow(x, 1 / y);
        }

        public static double abs(double x)
        {
            return Math.Abs(x);
        }

        public static double lg(double x)
        {
            return Math.Log(Math.E, x);
        }

        public static double ln(double x)
        {
            return Math.Log(10, x);
        }

        public static double log(double b, double x)
        {
            return Math.Log(b, x);
        }
    }
}
