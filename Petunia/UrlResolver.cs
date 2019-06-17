using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petunia
{
    class UrlResolver
    {
        public UrlResolver(string rootUrl)
        {
            this.rootUrl = rootUrl;
        }


        private string rootUrl;


        public string GetPlanUrl(string id)
        {
            return rootUrl + string.Format("/Repository/Plans/{0}.txt", id);
        }
    }
}
