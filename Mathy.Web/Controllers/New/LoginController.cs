using Mathy.DAL;
using Mathy.Model.Entity;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mathy.Web.Controllers.New
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            //try
            //{
            //    Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            //    var a = excel.WorksheetFunction.Sinh(0.2);
            //}
            //catch (Exception ex)
            //{
            //    return Json(ex.Message, JsonRequestBehavior.AllowGet);
            //}
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult RegisterInfo(string e, string t)
        {
            if (string.IsNullOrEmpty(e))
            {
                return View("~/Views/NotFound.cshtml");
            }
            try
            {
                //var dateStr = CommonTool.Decrypt(t);
                //var sendtime = Convert.ToDateTime(dateStr);
                //sendtime = sendtime.AddHours(1);
                e = CommonTool.Decrypt(e);
                //if ((DateTime.Now - sendtime) > TimeSpan.FromHours(1))
                //{
                //    ViewBag.info = "时间已失效，请重新发送验证邮件";
                //    return View("~/Views/NotFound.cshtml");
                //}
                ViewBag.email = e;
                return View();
            }
            catch (Exception ex)
            {
                return View("~/Views/NotFound.cshtml");
            }
        }

        public ActionResult ResetPass(string e, string t)
        {
            if (string.IsNullOrEmpty(e))
            {
                return View("~/Views/NotFound.cshtml");
            }
            try
            {
                var dateStr = CommonTool.Decrypt(t);
                var sendtime = Convert.ToDateTime(dateStr);
                sendtime = sendtime.AddYears(1);
                e = CommonTool.Decrypt(e);
                if ((DateTime.Now - sendtime) > TimeSpan.FromHours(1))
                {
                    ViewBag.info = "时间已失效，请重新发送验证邮件";
                    return View("~/Views/NotFound.cshtml");
                }
                ViewBag.email = e;
                return View();
            }
            catch (Exception ex)
            {
                return View("~/Views/NotFound.cshtml");
            }
        }

        public ActionResult ForgetPass()
        {
            return View();
        }

        [HttpPost]
        public string ResetPass(UserEntity user)
        {
            var dal = new UserDAL();
            var result = dal.UpdateUserPass(user);
            if (result)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }

        [HttpPost]
        public string LoginSumbit(string email, string password)
        {
            var dal = new UserDAL();
            var user = dal.GetUser(email, password);
            if (user == null)
                return "false";
            else
            {
                if (user.EnableDate.HasValue && user.EnableDate < DateTime.Now)
                    return "overDate";
                var cookie = new HttpCookie("user");
                cookie.Value = user.Email;
                HttpContext.Response.Cookies.Set(cookie);
                var id = new HttpCookie("userid");
                id.Value = user.UserID.ToString();
                HttpContext.Response.Cookies.Set(id);

                var roles = new RoleDAL().GetUserRoles();

                var role = new HttpCookie("Role");
                var temp = roles.FirstOrDefault(m => m.UserID == user.UserID);
                if (temp != null)
                    role.Value = temp.RoleID.ToString();
                else
                    role.Value = "2";
                HttpContext.Response.Cookies.Set(role);

                return "true";
            }
        }

        [HttpPost]
        public string SendRegisterMail(string email)
        {
            try
            {
                //CommonTool.Sendmail(email, @"欢迎注册UES,请点击下面链接
                //        http://" + HttpContext.Request.Url.Authority + "/login/RegisterInfo?e=" + CommonTool.Encrypt(email) + "&t=" + CommonTool.Encrypt(DateTime.Now.ToString()),
                //        "欢迎注册UES");
                return CommonTool.Encrypt(email);
            }
            catch (Exception ex)
            {
                return "error";
            }
        }

        [HttpPost]
        public string SendForgetMail(string email)
        {
            try
            {
                CommonTool.Sendmail(email, @"这是一封来自UES的找回密码邮件,请点击下面链接
                        http://" + HttpContext.Request.Url.Authority + "/login/ResetPass?e=" + CommonTool.Encrypt(email) + "&t=" + CommonTool.Encrypt(DateTime.Now.ToString()),
                        "UES找回密码");
                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        public string RgisterSumbit(UserEntity user)
        {
            var dal = new UserDAL();
            user.CreateTime = DateTime.Now;
            user.EnableDate = DateTime.Now.AddMonths(3);
            var result = dal.InnitUser(user);
            if (result > 0)
            {
                var roleDAL = new RoleDAL();
                roleDAL.InserUserRole(new UserRoleEntity
                {
                    RoleID = 2,
                    UserID = result,
                    DeleteFlag = 0
                });
                var cookie = new HttpCookie("user");
                cookie.Value = user.Email;
                HttpContext.Response.Cookies.Set(cookie);
                var id = new HttpCookie("userid");
                id.Value = result.ToString();
                HttpContext.Response.Cookies.Set(id);
                var role = new HttpCookie("Role");
                role.Value = "2";
                HttpContext.Response.Cookies.Set(role);
                return "true";
            }
            else
            {

                return "false";
            }
        }

        public ActionResult Error(string error)
        {
            ViewBag.info = error;
            return View("~/Views/NotFound.cshtml");
        }

    }
}