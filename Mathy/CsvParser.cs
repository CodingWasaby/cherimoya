using Mathy.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy
{
    class CsvParser
    {
        public Matrix Parse(string s)
        {
            s = s.Replace("[", "").Replace("]", "");
            s = s.Replace(";", "\r\n");
            List<string[]> rows = new List<string[]>();
            int columnCount = 0;

            foreach (string line in s.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] columns = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                int currColumnCount = columns.Length;
                if (columnCount != 0 && columnCount != currColumnCount)
                {
                    throw new Exception("矩阵每行的列数不相等。");
                }
                else if (columnCount == 0)
                {
                    columnCount = currColumnCount;
                }
                rows.Add(columns);
            }

            Matrix m = new Matrix(rows.Count, columnCount);
            for (int i = 0; i <= rows.Count - 1; i++)
            {
                for (int j = 0; j <= rows[i].Length - 1; j++)
                {
                    m[i, j] = Convert.ToDouble(rows[i][j]);
                }
            }

            return m;
        }
    }
}
