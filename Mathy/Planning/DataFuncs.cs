using Mathy.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.Planning
{
    class DataFuncs
    {
        public static object CreateDefaultValue(DataType dataType)
        {
            if (dataType == DataType.Number)
            {
                return (double)0;
            }
            else if (dataType == DataType.String)
            {
                return string.Empty;
            }
            else if (dataType == DataType.Vector)
            {
                return new Vector(0);
            }
            else
            {
                return new Matrix(0, 0);
            }
        }

        public static object DeserializeValue(DataType type, string s)
        {
            if (type == DataType.Number)
            {
                return double.Parse(s);
            }
            else if (type == DataType.String)
            {
                return s;
            }
            else if (type == DataType.Vector)
            {
                return Matrix.Parse(s).ToVector();
            }
            else
            {
                return Matrix.Parse(s);
            }
        }
    }
}
