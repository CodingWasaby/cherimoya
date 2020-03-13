using System.Collections.Generic;

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

        //当前页开始编号
        private int _rowStart;
        public int RowStart
        {
            get
            {
                return _rowStart == 0 ? (PageIndex - 1) * PageSize + 1 : _rowStart;
            }
            set
            {
                _rowStart = value;
            }
        }
        //当前页结束编号
        private int _rowEnd;
        public int RowEnd
        {
            get { return _rowEnd == 0 ? PageIndex * PageSize : _rowEnd; }
            set { _rowEnd = value; }
        }
    }

    public class PageList<T>
    {
        public PageInfo Page { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
