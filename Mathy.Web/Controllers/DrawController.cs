using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mathy.Web.Controllers
{
    public class DrawController : Controller
    {
        // GET: Draw
        public ActionResult a()
        {
            return View("均值");
        }
        public ActionResult b()
        {
            return View("比对图");
        }
        public ActionResult c()
        {
            return View("线性回归");
        }
        public ActionResult d()
        {
            return View("直方图");
        }
        public ActionResult e()
        {
            return View("hk");
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportGrid(HttpPostedFileBase file)
        {
            try
            {
                List<DataColumn> datas = new List<DataColumn>();
                var excel = new Workbook(file.InputStream);
                foreach (Cell n in excel.Worksheets[0].Cells)
                {
                    var dc = datas.FirstOrDefault(m => m.Index == n.Column);
                    if (dc == null)
                    {
                        dc = new DataColumn
                        {
                            Index = n.Column,
                            Name = n.StringValue,
                            Rows = new List<DataRow>()
                        };
                        datas.Add(dc);
                    }
                    else
                    {
                        dc.Rows.Add(new DataRow { RowData = n.StringValue, RowIndex = n.Row });
                    }
                }
                return View(datas);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new JsonResult() { Data = new { message = ex.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public class DataRow
        {
            public int RowIndex { get; set; }
            public string RowData { get; set; }
        }

        public class DataColumn
        {
            public string Name { get; set; }
            public int Index { get; set; }
            public List<DataRow> Rows { get; set; }
        }
    }
}