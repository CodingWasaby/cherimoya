using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy
{
    class PathResolver
    {
        public PathResolver(string rootPath)
        {
            this.rootPath = rootPath;
        }


        private string rootPath;


        public string GetTablesFilePath(string fileName)
        {
            return Path.Combine(rootPath, "Tables", fileName);
        }
    }
}
