using Dandelion.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.Maths.Table
{
    static class Grubbs
    {
        static Grubbs()
        {
            table = SearchTable<int, int, float>.Load(MathyContext.PathResolver.GetTablesFilePath("grubbs.txt"));
        }


        private static SearchTable<int, int, float> table;

        public static float GetValue(int alpha, int n)
        {
            return table[n, alpha];
        }
    }
}
