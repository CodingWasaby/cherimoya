using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mathy.Web.Html
{
    /// <summary>
    /// A PagingMode that arranges items so that the total count of items remain the same regardless of
    /// the current page index, shown as follows: <br/>
    /// [1] 2 3 4 ... 12 13 14 15 16 17 18 19 20  <br/>
    /// 1 2 3 ... 7 8 9 [10] 11 12 13 ... 18 19 20  <br/>
    /// 1 2 3 4 5 6 7 8 9 ... 17 18 19 [20]  <br/>
    /// </summary>
    public class BalancedPagingMode : PagingMode
    {
        public int FloatingItemCount { get; set; }

        public int TotalItemCount { get; set; }


        internal override int[] GetItems(int pageCount, int pageIndex)
        {
            List<int> items = new List<int>();

            if (pageCount <= TotalItemCount + 1)
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    items.Add(i);
                }
            }
            else
            {
                int leftFloatingCount = pageIndex - Math.Max(pageIndex - FloatingItemCount, 1);
                int rightFloatingCount = Math.Min(pageIndex + FloatingItemCount, pageCount) - pageIndex;

                int leftDockedCount;
                int rightDockedCount;

                if (pageIndex <= TotalItemCount / 2 + 1)
                {
                    leftDockedCount = Math.Min(pageIndex - leftFloatingCount - 1, TotalItemCount / 2 - FloatingItemCount);
                    rightDockedCount = TotalItemCount - leftDockedCount - leftFloatingCount - rightFloatingCount;
                }
                else if (pageIndex >= pageCount - (TotalItemCount / 2))
                {
                    rightDockedCount = Math.Min((pageCount - pageIndex + 1) - rightFloatingCount - 1, TotalItemCount / 2 - FloatingItemCount);
                    leftDockedCount = TotalItemCount - rightDockedCount - leftFloatingCount - rightFloatingCount;
                }
                else
                {
                    leftDockedCount = TotalItemCount / 2 - FloatingItemCount;
                    rightDockedCount = TotalItemCount / 2 - FloatingItemCount;
                }


                for (int i = 1; i <= leftDockedCount; i++)
                {
                    items.Add(i);
                }

                for (int i = pageIndex - leftFloatingCount; i <= pageIndex + rightFloatingCount; i++)
                {
                    items.Add(i);
                }

                for (int i = pageCount - rightDockedCount + 1; i <= pageCount; i++)
                {
                    items.Add(i);
                }
            }

            return items.ToArray();
        }
    }

}