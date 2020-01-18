using System;

namespace Mathy.Web.Html
{
    /// <summary>
    /// An item that store the templated strings in a Paging. <br/>
    /// An item could either be an actual Paging item or a gap. Whether an item is a gap could be
    /// determined with isGap(). 
    /// If an item is a gap, calling other methods on the object 
    /// makes no sense and an IllegalStateException will be thrown.
    /// </summary>
    public class PagingItem
    {
        private int index;
        /// <summary>
        /// Get the page index of the item.
        /// </summary>
        public int Index
        {
            get
            {
                if (IsGap)
                {
                    throw new InvalidOperationException();
                }


                return index;
            }
            set
            {
                index = value;
            }
        }
        internal void setIndex(int index)
        {
            this.index = index;
        }

        /// <summary>
        /// Get whether the item represents a gap.
        /// </summary>
        public bool IsGap { get; internal set; }


        public string[] Texts { get; internal set; }

        /// <summary>
        /// Get the first text of the item.
        /// </summary>
        public string Text
        {
            get { return GetText(0); }
        }


        /// <summary>
        /// Get text of the item at the specified index.
        /// </summary>
        /// <param name="index">index index of the text.</param>
        /// <returns>text of the item as the specified index.</returns>
        public string GetText(int index)
        {
            if (IsGap)
            {
                throw new InvalidOperationException();
            }


            return Texts[index];
        }


        private bool isCurrent;
        /**
         * Get whether the item is the the selected item.
         * An item is the selected item if its index equals to the pageIndex specified when creating the Paging.
         * @return true if the item is selected, false otherwise.
         */
        public bool IsCurrent
        {
            get
            {
                if (IsGap)
                {
                    throw new InvalidOperationException();
                }


                return isCurrent;
            }
            internal set
            {
                isCurrent = value;
            }
        }
    }
}