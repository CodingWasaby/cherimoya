using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.BaseLib
{
    public class Vector
    {
        public Vector(int size)
        {
            Size = size;
            items = new double[Size];
        }


        public int Size { get; private set; }


        private double[] items;


        public double this[int index]
        {
            get 
            {
                return items[index];
            }
            set
            {
                items[index] = value;
            }
        }

        public double GetAverage()
        {
            return items.Average();
        }

        public double GetSum()
        {
            return items.Sum();
        }
    }
}
