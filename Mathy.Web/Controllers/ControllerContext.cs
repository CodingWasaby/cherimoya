using Mathy.Web.Html;
using Mathy.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Mathy.Web.Controllers
{
    public class ControllerContext : Controller
    {
        protected PageButtonVM[] Paging(int pageIndex, int count, string sessionKey, int pageSize = 3)
        {
            int pageCount = (int)Math.Ceiling((float)count / pageSize);

            if (pageIndex > pageCount - 1)
            {
                pageIndex = pageCount - 1;
            }


            Session[sessionKey] = pageIndex;


            PagingBuilder pb = new PagingBuilder();
            pb.PageCount = pageCount;
            pb.PageIndex = pageIndex + 1;
            pb.ItemTemplates = new string[] { "{0}", "javascript:onPagingButtonClick('{0}');" };
            pb.Mode = new BalancedPagingMode()
            {
                FloatingItemCount = 3,
                TotalItemCount = 12
            };

            return pb.Build().Items.Select(i => new PageButtonVM()
            {
                ClassName = GetClassName(i),
                Text = i.Text,
                IsGap = i.IsGap,
                OnClick = string.Format("javascript:onPagingButtonClick({0})", int.Parse(i.Text) - 1)
            }).ToArray();
        }

        private static string GetClassName(PagingItem item)
        {
            if (!item.IsCurrent)
            {
                return " class=\"paging-button\"";
            }
            else
            {
                return " class=\"paging-button-current\"";
            }
        }
    }
}