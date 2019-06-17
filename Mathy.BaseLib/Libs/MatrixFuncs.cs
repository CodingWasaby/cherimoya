using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.BaseLib.Libs
{
    public class MatrixFuncs
    {
        public static Matrix pow(Matrix m, double p)
        {
            return m.Pow(p);
        }

        public static double size(Matrix m)
        {
            return m.RowCount * m.ColumnCount;
        }

        public static double rowCount(Matrix m)
        {
            return m.RowCount;
        }

        public static double columnCount(Matrix m)
        {
            return m.ColumnCount;
        }

        public static double average(Matrix m)
        {
            return m.GetAverage();
        }

        public static double sum(Matrix m)
        {
            return m.GetSum();
        }

        public static double sqrvar(Matrix m)
        {
            double avg = m.GetAverage();

            return m.Items.Sum(i => (i - avg) * (i - avg)) / (m.RowCount * m.ColumnCount - 1);
        }

        public static double stdvar(Matrix m)
        {
            return Math.Sqrt(sqrvar(m));
        }

        public static double max(Matrix m)
        {
            return m.Items.Max();
        }

        public static double min(Matrix m)
        {
            return m.Items.Min();
        }
    }
}
