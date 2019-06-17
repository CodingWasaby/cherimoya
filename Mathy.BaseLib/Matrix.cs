using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.BaseLib
{
    public class Matrix
    {
        public Matrix(int rowCount, int columnCount)
            : this(rowCount, columnCount, new double[rowCount * columnCount])
        {
        }

        public Matrix(int rowCount, int columnCount, double[] items)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;
            Items = items;
        }


        public int RowCount { get; private set; }

        public int ColumnCount { get; private set; }


        public double[] Items { get; private set; }


        public double this[int row, int column]
        {
            get 
            {
                return Items[row * ColumnCount + column];
            }
            set
            {
                Items[row * ColumnCount + column] = value;
            }
        }


        public Vector ToVector()
        {
            if (RowCount == 1)
            {
                Vector vector = new Vector(ColumnCount);
                for (int i = 0; i <= ColumnCount - 1; i++)
                {
                    vector[i] = this[0, i];
                }

                return vector;
            }
            else if (ColumnCount == 1)
            {
                Vector vector = new Vector(RowCount);
                for (int i = 0; i <= RowCount - 1; i++)
                {
                    vector[i] = this[i, 0];
                }

                return vector;
            }

            throw new Exception(string.Format("Cannot convert a matrix of {0}*{1} to a vector.", RowCount, ColumnCount));
        }


        public double GetAverage()
        {
            return Items.Average();
        }

        public double GetSum()
        {
            return Items.Sum();
        }

        public object Add(Matrix matrix)
        {
            if (RowCount != matrix.RowCount || ColumnCount != matrix.ColumnCount)
            {
                throw new Exception("Cannot add two matrixes not of the same size.");
            }


            return new Matrix(RowCount, ColumnCount)
            {
                Items = Items.Select((i, index) => i + matrix.Items[index]).ToArray()
            };
        }

        public Matrix Add(double d)
        {
            return new Matrix(RowCount, ColumnCount, Items.Select(i => i + d).ToArray());
        }

        public Matrix Subtract(double d)
        {
            return Add(-d);
        }

        public object GetNegative()
        {
            return new Matrix(RowCount, ColumnCount, Items.Select(i => -i).ToArray());
        }


        public Matrix ToNegative()
        {
            return new Matrix(RowCount, ColumnCount, Items.Select(i => -i).ToArray());
        }

        public Matrix Multiply(double d)
        {
            return new Matrix(RowCount, ColumnCount, Items.Select(i => i * d).ToArray());
        }

        public Matrix Multiply(Matrix m)
        {
            throw new Exception("The evaluation of the product of two matrixes has not been implemented.");
        }

        public Matrix Pow(double p)
        {
            return new Matrix(RowCount, ColumnCount, Items.Select(i => Math.Pow(i, p)).ToArray());
        }

        public Matrix ElementInvert()
        {
            return new Matrix(RowCount, ColumnCount, Items.Select(i => 1 / i).ToArray());
        }


        public override string ToString()
        {
            StringBuilder b = new StringBuilder();
            b.Append("[");

            for (int row = 0; row <= RowCount - 1; row++)
            {
                for (int column = 0; column <= ColumnCount - 1; column++)
                {
                    b.Append(this[row, column]).Append(column == ColumnCount - 1 ? "; " : ", ");
                }
            }


            string s = b.ToString();

            if (s.EndsWith("; "))
            {
                s = s.Substring(0, s.Length - 2);
            }


            return s + "]";
        }

        public static object Parse(string s)
        {
            if (s == null)
            {
                throw new Exception("Argument is null.");
            }


            s = s.Replace("\r\n", string.Empty);

            if (s.StartsWith("[") != s.EndsWith("]"))
            {
                throw new Exception("Matrix bracket does not match.");
            }

            if (s.StartsWith("["))
            {
                s = s.Substring(1, s.Length - 2);
            }


            List<List<double>> items = new List<List<double>>();

            foreach (string row in s.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                items.Add(new List<double>());

                foreach (string item in row.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    items.Last().Add(double.Parse(item));
                }
            }


            if (items.Count > 0)
            {
                if (!items.Skip(1).All(i => i.Count == items[0].Count))
                {
                    throw new Exception("Each row must have the same count of elements.");
                }
            }


            List<double> elements = new List<double>();

            foreach (List<double> row in items)
            {
                foreach (double item in row)
                {
                    elements.Add(item);
                }
            }


            return new Matrix(items.Count, items.Count == 0 ? 0 : items[0].Count, elements.ToArray());
        }
    }
}
