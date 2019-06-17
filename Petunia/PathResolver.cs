using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petunia
{
    class PathResolver
    {
        public PathResolver(string rootPath)
        {
            this.rootPath = rootPath;
        }


        private string rootPath;


        public string MathyPath
        {
            get { return System.IO.Path.Combine(rootPath, @"Repository\Mathy"); }
        }

        public string PlanRepositoryPath
        {
            get { return System.IO.Path.Combine(rootPath, @"Repository\Plans"); }
        }

        public string GetPlanPath(string id)
        {
            return System.IO.Path.Combine(rootPath, @"Repository\Plans", id + ".txt");
        }

        public string FuncDocPath
        {
            get { return System.IO.Path.Combine(rootPath, @"Repository\Docs\Funcs.txt"); }
        }

        public string GetFuncArticlePath(string name)
        {
            return System.IO.Path.Combine(rootPath, string.Format(@"Repository\Docs\Details\{0}.txt", name));
        }
    }
}
