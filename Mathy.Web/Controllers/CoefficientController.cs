using Aspose.Cells;
using Mathy.DAL;
using Mathy.Model.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Mathy.Web.Controllers
{
    public class CoefficientController : Controller
    {

        public ActionResult Index(string coefficientName, int pageIndex)
        {
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            var dal = new CoefficientDAL();
            var result = dal.GetCoefficients(new Model.Common.PageInfo { PageIndex = pageIndex, PageSize = 5 }, coefficientName);
            ViewBag.pageIndex = pageIndex;
            ViewBag.TotalPage = result.Page.TotalCount / result.Page.PageSize + 1;
            ViewBag.CoefficientName = coefficientName;

            var user = new UserEntity();
            user.Email = HttpContext.Request.Cookies["user"].Value;
            user.Company = "";
            user.Role = HttpContext.Request.Cookies["role"].Value;
            ViewBag.User = user;
            return View(result.Data);
        }

        public ActionResult CreateCoefficient(int coefficientID, bool ro = false)
        {
            CoefficientVM vm;
            if (coefficientID == 0)
            {
                vm = new CoefficientVM();
                vm.CoefficientDetails = new List<CoefficientDetail>();
            }
            else
            {
                var dal = new CoefficientDAL();
                var c = dal.GetCoefficient(coefficientID);
                vm = new CoefficientVM();
                vm.CoefficientID = c.CoefficientID;
                vm.CoefficientName = c.CoefficientName;
                vm.CoefficientContent = c.CoefficientContent;
                vm.CoefficientDetails = new CoefficientDAL().GetCoefficientDetail(coefficientID);
            }
            ViewBag.ro = ro;
            return View(vm);
        }

        [HttpPost]
        public ActionResult ImportGrid(HttpPostedFileBase file)
        {
            var workbook = new Workbook(file.InputStream);
            int headRow = -1;
            var sheet = workbook.Worksheets[0];
            foreach (Cell c in sheet.Cells)
            {
                if (c.Value != null)
                    headRow = c.Row;
                break;
            }
            if (headRow < 0)
            {
                return Content("列头读取有误，请检查！");
            }
            var heads = new Dictionary<string, int>();
            try
            {
                var row = sheet.Cells.GetRow(headRow);
                for (var i = 0; i <= sheet.Cells.MaxDataColumn; i++)
                {
                    heads.Add(row.GetCellByIndex(i).Value.ToString(), i);
                }
            }
            catch (Exception ex)
            {
                return Content("列名称有误，请检查！");
            }
            var list = new List<CoefficientDetail>();
            foreach (Row r in sheet.Cells.Rows)
            {
                if (r.Index != headRow)
                {
                    foreach (var n in heads)
                    {
                        var cell = r.GetCellOrNull(n.Value);
                        var cValue = cell == null ? "0" : cell.Value.ToString();

                        decimal dv = 0;
                        if (decimal.TryParse(cValue, out dv))
                        {
                            list.Add(new CoefficientDetail
                            {
                                CoefficientDetailIndex = n.Value,
                                CoefficientDetailName = n.Key,
                                CoefficientDetailRow = r.Index,
                                CoefficientDetailValue = dv
                            });
                        }
                        else
                        {
                            string msg = string.Format("行{0}列{1}数据有误，请检查！", r.Index, n.Value);
                            return Content(msg);
                        }
                    }
                }
            }
            Session.Add("coeData", list);
            return View(list);
        }

        [HttpPost]
        public string AddCoefficient(CoefficientVM coefficient)
        {
            var dal = new CoefficientDAL();
            var details = (List<CoefficientDetail>)Session["coeData"];
            if (details == null || details.Count == 0)
            {
                coefficient.CoefficientID = -1;
                return JsonConvert.SerializeObject(coefficient);
            }
            using (var tran = dal.GetTransaction())
            {
                if (coefficient.CoefficientID > 0)
                {
                    dal.UpdateCoefficient(coefficient, tran);
                }
                else
                {
                    if (dal.GetCoefficientDetail(coefficient.CoefficientName).Count > 0)
                    {
                        coefficient.CoefficientID = -2;
                        return JsonConvert.SerializeObject(coefficient);
                    }
                    coefficient.Creator = HttpContext.Request.Cookies["user"].Value;
                    coefficient.CoefficientID = dal.AddCoefficient(coefficient, tran);
                }
                details.ForEach(m => m.CoefficientID = coefficient.CoefficientID);
                dal.DeleteCoefficientDetails(coefficient.CoefficientID, tran);
                dal.AddCoefficientDetails(details, tran);
                tran.Commit();
            }
            return JsonConvert.SerializeObject(coefficient);
        }

        [HttpPost]
        public bool DeleteCoefficient(int autoID)
        {
            return new CoefficientDAL().DeleteCoefficient(autoID);
        }
    }
}