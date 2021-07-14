using System.Web.Mvc;

namespace Mathy.Web.Controllers.New.Filter
{
    public class AuthValidateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userCookie = context.HttpContext.Request.Cookies["user"];
            if (userCookie == null)
            {
                context.Result = new RedirectResult("http://" + context.HttpContext.Request.Url.Authority + "/index.html");
                return;
            }
            context.Controller.ViewData["Username"] = userCookie.Value;
            context.Controller.ViewData["userRole"] = 0;
            //context.Controller.ViewData["userRole"] = context.HttpContext.Request.Cookies["role"].Value;
        }

        //public override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    if (filterContext.Exception != null)
        //    {
        //        filterContext.ExceptionHandled = true;
        //        filterContext.Result = new RedirectResult("/Login/error?error=" + filterContext.Exception.Message.Replace("\r\n", "$").Replace("\n", "$"));
        //        base.OnActionExecuted(filterContext);
        //    }
        //}

    }
}