using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.Libs
{
    public class StringFuncs
    {
        public static int len(string s)
        {
            return s.Length;
        }

        public static string reverse(string s)
        {
            return new string(s.Reverse().ToArray());
        }
    }
}
