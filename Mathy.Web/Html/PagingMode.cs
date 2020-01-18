namespace Mathy.Web.Html
{
    public abstract class PagingMode
    {
        internal abstract int[] GetItems(int pageCount, int pageIndex);
    }
}