using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy
{
    class MathyContext
    {
        public static PathResolver PathResolver { get; private set; }


        public static void Init(string rootPath)
        {
            PathResolver = new PathResolver(rootPath);
        }
    }
}
