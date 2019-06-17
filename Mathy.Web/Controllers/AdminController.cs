using Mathy.Web.ServiceModels;
using Petunia;
using Petunia.LogicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mathy.Web.Controllers
{
    public class AdminController : ControllerContext
    {
        [OutputCache(Duration = 0)]
        public ActionResult Login()
        {
            if (Session["LoginError"] != null)
            {
                string message = (string)Session["LoginError"];
                Session.Remove("LoginError");
                ViewData["Message"] = message;
            }
            return View();
        }

        [HttpPost]
        [OutputCache(Duration = 0)]
        public ActionResult Login(LoginUserSM user)
        {
            UserLM userLM = Script.GetAuthUser(user.ID, user.Password);

            if (userLM == null)
            {
                Session["LoginError"] = "用户名或密码错误。";
                return Redirect("/Admin/Login");
            }
            else
            {
                Session["User"] = userLM;

                HttpCookie cookie = new HttpCookie("ID", userLM.ID);
                cookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(cookie);

                cookie = new HttpCookie("Password", userLM.Password);
                cookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(cookie);

                string url = (string)Session["RedirectUrl"];
                Session.Remove("RedirectUrl");
                return Redirect(url ?? "/Home/Index");
            }
        }


        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult Logout(LoginUserSM user)
        {
            Session.Remove("User");
            return Redirect("/Admin/Login");
        }
	}
}