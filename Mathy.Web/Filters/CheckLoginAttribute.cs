using System.Web.Mvc;

namespace Mathy.Web.Filters
{
    public class CheckLoginAttribute : ActionFilterAttribute
    {
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{ 
        //    var session = filterContext.HttpContext.Session;

        //    bool isAjax = filterContext.HttpContext.Request.IsAjaxRequest();


        //    if (session["User"] == null)
        //    {
        //        HttpCookieCollection cookies = filterContext.HttpContext.Request.Cookies;
        //        if (cookies["ID"] != null)
        //        {
        //            session["User"] = Script.GetAuthUser(cookies["ID"].Value, cookies["Password"].Value);
        //        }
        //    }

        //    if (session["User"] == null)
        //    {
        //        if (!isAjax)
        //        {
        //            session["RedirectUrl"] = filterContext.HttpContext.Request.RawUrl;
        //            filterContext.Result = new RedirectResult("/Admin/Login");
        //        }
        //        else
        //        {
        //            filterContext.HttpContext.Response.StatusCode = 500;
        //            filterContext.Result = new JsonResult() { Data = new { success = false, message = "用户未登录。" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //        }
        //    }
        //    else
        //    {
        //        filterContext.Controller.ViewData["UserName"] = (session["User"] as UserLM).Name;
        //    }
        //}
    }
}