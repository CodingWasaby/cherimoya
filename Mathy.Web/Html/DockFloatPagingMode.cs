using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mathy.Web.Html
{
    /// <summary>
    /// A PagingMode that arranges items so that the count of docked items at the
    /// beginning and end will always be fixed, shown as follows:
    /// [1] 2 3 4 5 6 ... 15 16 17 18 19 20 
    /// 1 2 3 4 5 6 7 8 9 [10] 11 12 13 ... 15 16 17 18 19 20 
    /// 1 2 3 4 5 6 ... 15 16 17 18 19 [20] 
    /// </summary>
    public class DockFloatPagingMode : PagingMode
    {
        public int FloatingItemCount { get; set; }

        public int DockedItemCount { get; set; }

        internal override int[] GetItems(int pageCount, int pageIndex)
        {
            List<int> items = new List<int>();

            if (pageCount <= FloatingItemCount * 2 + DockedItemCount * 2 + 1)
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    items.Add(i);
                }
            }
            else
            {
                for (int i = 1; i <= DockedItemCount; i++)
                {
                    items.Add(i);
                }

                for (int i = Math.Max(pageIndex - FloatingItemCount, DockedItemCount + 1); i <= Math.Min(pageIndex - 1, pageCount - DockedItemCount); i++)
                {
                    items.Add(i);
                }

                if (pageIndex > DockedItemCount && pageIndex < pageCount - DockedItemCount + 1)
                {
                    items.Add(pageIndex);
                }

                for (int i = Math.Max(pageIndex + 1, DockedItemCount + 1); i <= Math.Min(pageIndex + FloatingItemCount, pageCount - DockedItemCount); i++)
                {
                    items.Add(i);
                }

                for (int i = pageCount - DockedItemCount + 1; i <= pageCount; i++)
                {
                    items.Add(i);
                }
            }

            return items.ToArray();
        }
    }
}