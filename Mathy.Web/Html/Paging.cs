using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mathy.Web.Html
{
    /// <summary>
    /// Paging is intended for implementing paging bars in HTML pages. <br/>
    /// A Paging is the abstract representation of a data structure that stores a list of templated strings
    /// in a list whose allowed size is much smaller than the count of the objects. <br/>
    /// In order to do so, objects expect for that are close enough to the current one or close enough
    /// to the beginning or end of the original list are removed, creating gaps. How the objects are filtered
    /// is determined by the PagingMode that is supplied to the PagingBuilder when creating the Paging.
    /// </summary>
    public class Paging
    {
        /// <summary>
        /// Get the items of the Paging.
        /// </summary>
        public PagingItem[] Items { get; internal set; }
        /**
         * Get the items of the Paging.
         * @return the items of the Paging.
         */
    }
}