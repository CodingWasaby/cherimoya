using Aspose.Cells;
using Mathy.Libs;
using Mathy.Model.Draw;
using Mathy.Planning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mathy.Web.Controllers
{
    public class DrawController : Controller
    {

        public ActionResult GetDraw(string key, int? count)
        {
            EvaluationContext context = (EvaluationContext)Session["Context"];
            var data = context.SourceVariables[key];
            if (key.Contains("Draw_MeanValue"))
            {
                return MV((MeanValue)data);
            }
            if (key.Contains("Draw_HK"))
            {
                return HK((HK)data);
            }
            if (key.Contains("Draw_LinearRegression"))
            {
                return LR((LinearRegression)data);
            }
            if (key.Contains("Draw_Histogram"))
            {
                return His((Histogram)data);
            }
            if (key.Contains("Draw_Comparison"))
            {
                return COM((Comparison)data);
            }
            if (key.Contains("MCM"))
            {
                Histogram his = DrawFuncs.Draw_Histogram((double[])data, context.Settings.DecimalDigitCount, count.Value);
                return His(his);
            }
            return View();
        }

        public ActionResult MV(MeanValue meanValue)
        {
            return View("均值", meanValue);
        }

        public ActionResult COM(Comparison com)
        {
            return View("比对图", com);
        }
        public ActionResult LR(LinearRegression lr)
        {
            return View("线性回归", lr);
        }
        public ActionResult His(Histogram his)
        {
            return View("直方图", his);
        }
        public ActionResult HK(HK hk)
        {
            return View("hk", hk);
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