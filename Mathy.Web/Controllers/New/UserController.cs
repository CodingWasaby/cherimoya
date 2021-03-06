﻿using Mathy.DAL;
using Mathy.Model.Entity;
using Mathy.Model.Serach;
using Mathy.Web.Controllers.New.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Mathy.Web.Controllers.New
{
    [AuthValidate]
    public class UserController : Controller
    {
        public ActionResult Index(string username, string company, string beginDate, string endDate, string role, string email)
        {
            var userDal = new UserDAL();
            var roleDal = new RoleDAL();
            var search = new UserSearch();
            search.UserName = username;
            search.Company = company;

            if (DateTime.TryParse(beginDate, out DateTime b))
                search.BeginDate = b;
            if (DateTime.TryParse(endDate, out DateTime e))
                search.EndDate = e.AddDays(1).AddSeconds(-1);
            var list = userDal.GetUsers(search);
            var rolelist = roleDal.GetUserRoles();

            var setTemp = (from l in list
                           join r in rolelist on l.UserID equals r.UserID
                           let x = r.RoleID
                           select new
                           {
                               a = l.Role = GetRoleName(x)
                           }).ToList();
            role = string.IsNullOrEmpty(role) ? "不限" : role;
            if (role != "不限")
            {
                list = list.Where(m => m.Role == role).ToList();
            }
            if (!string.IsNullOrEmpty(email))
                list = list.Where(m => m.Email == null || m.Email.Contains(email)).ToList();
            ViewBag.username = username;
            ViewBag.company = company;
            ViewBag.beginDate = beginDate;
            ViewBag.endDate = endDate;
            ViewBag.role = role;
            ViewBag.email = email;
            return View(list);
        }

        [HttpPost]
        public List<UserEntity> GetUserList(UserSearch search)
        {
            var userDal = new UserDAL();
            var list = userDal.GetUsers(new UserSearch());
            return list;
        }

        [HttpPost]
        public string UpdateRole(string userids, string role)
        {
            int r = 0;
            if (role == "管理员")
            {
                r = 1;
            }
            if (role == "普通用户")
            {
                r = 2;
            }
            if (role == "尊享用户")
            {
                r = 3;
            }
            var dal = new RoleDAL();
            return dal.UpdateUserRole(userids.Split(',').ToList(), r).ToString();
        }



        [HttpPost]
        public string UpdateDate(string emails, string date)
        {
            if (DateTime.TryParse(date, out DateTime d))
            {
                var dal = new UserDAL();
                return dal.UpdateUserEnableDate(emails.Split(',').ToList(), d).ToString();
            }
            return "fales";
        }

        public ActionResult ROle()
        {
            ViewBag.info = "权限管理暂时未开放";
            return View("~/Views/NotFound.cshtml");
        }

        public static string GetRoleName(int roleid)
        {
            if (roleid == 1)
            {
                return "管理员";
            }
            if (roleid == 3)
            {
                return "尊享用户";
            }
            return "普通用户";
        }
    }
}