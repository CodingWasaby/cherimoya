using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mathy.Web.Html
{
    public abstract class PagingMode
    {
        internal abstract int[] GetItems(int pageCount, int pageIndex);
    }
}