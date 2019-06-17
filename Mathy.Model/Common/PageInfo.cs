using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.Model.Common
{
    public class PageInfo
    {
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public string OrderField { get; set; }
        public string DescString { get; set; }

        public int OffSetRows
        {
            get
            {
                return (PageIndex - 1) * PageSize;
            }
        }
    }

    public class PageList<T>
    {
        public PageInfo Page { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
