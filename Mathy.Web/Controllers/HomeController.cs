using Dandelion.Serialization;
using Mathy.DAL;
using Mathy.Maths;
using Mathy.Model.Entity;
using Mathy.Planning;
using Mathy.Templates;
using Mathy.Visualization.Computation;
using Mathy.Web.Controllers.New.Filter;
using Mathy.Web.Filters;
using Mathy.Web.Models;
using Mathy.Web.ServiceModels;
using Petunia;
using Petunia.LogicModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mathy.Web.Controllers
{
    [AuthValidate]
    public class HomeController : ControllerContext
    {
        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult Index(int? pageIndex, string jobName, string planName, string begindate, string enddate, string isFinish)
        {
            pageIndex = pageIndex == null ? 1 : pageIndex;

            //if (Session["JobPageIndex"] != null)
            //{
            //    pageIndex = (int)Session["JobPageIndex"];
            //}

            return View(GetJobList(pageIndex.Value, jobName, planName, begindate, enddate, isFinish));
        }

        public ActionResult AllJob(int? pageIndex, string jobName, string planName, string begindate, string enddate, string isFinish)
        {
            pageIndex = pageIndex == null ? 1 : pageIndex;

            //if (Session["JobPageIndex"] != null)
            //{
            //    pageIndex = (int)Session["JobPageIndex"];
            //}

            return View("index", GetJobList(pageIndex.Value, jobName, planName, begindate, enddate, isFinish, 1));
        }

        public ActionResult DashBoard()
        {
            var user = new UserEntity();
            user.Email = HttpContext.Request.Cookies["user"].Value;
            user.Company = "";
            user.Role = HttpContext.Request.Cookies["role"].Value;
            return View(user);
        }

        public ActionResult Main()
        {
            return View();
        }


        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult SearchJobs(int pageIndex, string jobName, string planName, string begindate, string enddate, string isFinish)
        {
            return PartialView("JobList", GetJobList(pageIndex, jobName, planName, begindate, enddate, isFinish));
        }

        /*
        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult DownloadPlan(string planID)
        {
            return File(Script.GetPlanStream(planID), "text/plain; charset=utf-8");
        }
         */

        private object GetJobList(int pageIndex, string jobName, string planName, string begindate, string enddate, string isFinish, int isall = 0)
        {
            //UserLM user = Session["User"] as UserLM;
            var userid = Convert.ToInt32(HttpContext.Request.Cookies["userid"].Value);
            if (isall == 1)
                userid = 0;
            //ViewData["PagingButtons"] = Paging(pageIndex, Script.GetJobCount(), "JobPageIndex", 10);
            ViewBag.pageIndex = pageIndex;
            ViewBag.TotalPage = Script.GetJobCount(userid) / 10 + 1;
            ViewBag.jobName = jobName;
            ViewBag.planName = planName;
            ViewBag.begindate = begindate;
            ViewBag.enddate = enddate;
            ViewBag.isall = isall;
            return Script.SearchJobs(userid, pageIndex - 1, 10, jobName, planName, begindate, enddate, isFinish).Select(i => new JobListCellVM(i, pageIndex - 1));
        }

        [CheckLogin]
        public ActionResult Docs()
        {
            return View(new DocVM(Script.GetFuncDocs()));
        }

        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult GetFuncArticle(int index)
        {
            return PartialView("FuncArticle", new FuncDocVM(Script.GetFuncDocs()[index], index)
            {
                Article = new FuncArticleVM(Script.GetFuncDocs()[index].Article)
            });
        }



        public ActionResult Plans(int? pageIndex, string planName, string begindate, string enddate, string content)
        {
            pageIndex = pageIndex == null ? 1 : pageIndex;
            var userid = HttpContext.Request.Cookies["userid"].Value;
            return View(GetPlanList(pageIndex, planName, begindate, enddate, content, userid));
        }


        public ActionResult PlansTotal(int? pageIndex, string planName, string begindate, string enddate, string content)
        {
            pageIndex = pageIndex == null ? 1 : pageIndex;
            //if (Session["PlanPageIndex"] != null)
            //{
            //    pageIndex = (int)Session["PlanPageIndex"];
            //}
            return View(GetPlanList(pageIndex, planName, begindate, enddate, content));
        }

        public object GetPlanList(int? pageIndex, string planName, string begindate, string enddate, string content, string author = "")
        {
            int pageSize = string.IsNullOrEmpty(author) ? 10 : 10;
            //ViewData["PagingButtons"] = Paging(pageIndex, Script.GetPlanCount(author), "PlanPageIndex", pageSize);
            ViewBag.pageIndex = pageIndex;
            ViewBag.TotalPage = Script.GetPlanCount(author, planName, begindate, enddate, content) / pageSize + 1;
            ViewBag.planName = planName;
            ViewBag.begindate = begindate;
            ViewBag.enddate = enddate;
            ViewBag.content = content;
            IEnumerable<PlanLM> list = Script.SearchPlans(0, int.MaxValue, author);
            if (!string.IsNullOrEmpty(planName))
            {
                list = list.Where(m => m.Title != null && m.Title.Contains(planName));
            }
            if (!string.IsNullOrEmpty(content))
            {
                list = list.Where(m => m.Description != null && m.Description.Contains(content));
            }
            if (!string.IsNullOrEmpty(begindate))
            {
                var d = DateTime.Now;
                if (DateTime.TryParse(begindate, out d))
                    list = list.Where(m => m.CreateTime >= d);
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                var d = DateTime.Now;
                if (DateTime.TryParse(enddate, out d))
                    list = list.Where(m => m.CreateTime < d.Date.AddDays(1));
            }
            var dal = new UserDAL();
            var users = dal.GetUsers(new Model.Serach.UserSearch());

            var setTemp = (from n in list
                           join u in users on n.Author equals u.UserID.ToString()
                           select n.Author = u.Name).ToList();
            return list.Skip((pageIndex.Value - 1) * pageSize).Take(pageSize)

                .Select(i => new PlanListCellVM(i, pageIndex.Value - 1, author)).ToList();
        }


        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult ViewPlan(string planID)
        {
            EvaluationContext context = Script.GetPlan(planID).CreateEvaluationContext();
            Session["Context"] = context;
            Session.Remove("UpdateCount");
            return View(new PlanVM(context));
        }


        private void SetUpdateCount(string editID)
        {
            if (Session["EditID"] == null || (string)Session["EditID"] != editID)
            {
                Session["UpdateCount"] = 0;
            }


            Session["UpdateCount"] = (int)Session["UpdateCount"] + 1;
            Session["EditID"] = editID;
        }

        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult SetPreviewPlan(EditPlanSM sm)
        {
            try
            {
                Plan plan = sm.ToPlan();

                EvaluationContext context = plan.CreateEvaluationContext();
                Session["Context"] = context;

                SetUpdateCount(sm.EditID);

                return new JsonResult() { Data = new { success = true }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new JsonResult() { Data = new { message = ex.Message.Replace("\r\n", "<br/>") }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult GetPlanStepList()
        {
            return PartialView("PlanStepList", new PlanVM(Session["Context"] as EvaluationContext));
        }

        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult GetPlanStepArea(int stepIndex)
        {
            EvaluationContext context = Session["Context"] as EvaluationContext;
            int updateCount = Session["UpdateCount"] == null ? 0 : (int)Session["UpdateCount"];
            return PartialView("PlanStepArea", new PlanStepAreaVM(context, context.Steps[stepIndex], stepIndex, updateCount));
        }


        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult CreateJob(int planAutoID)
        {
            ViewData["PageTitle"] = "创建实验";

            PlanLM plan = Script.GetPlanLM(planAutoID);

            return View("EditJob", new EditJobVM(new JobLM()
            {
                PlanAutoID = plan.AutoID,
                Name = plan.Title,
                PlanTitle = plan.Title
            }));
        }

        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult UpdateJob(int autoID)
        {
            ViewData["PageTitle"] = "修改实验";

            JobLM job = Script.GetJob(autoID);
            return View("EditJob", new EditJobVM(job));
        }

        [CheckLogin]
        [HttpPost]
        [OutputCache(Duration = 0)]
        public ActionResult UpdateJob(JobSM job)
        {
            if (job.AutoID != 0)
            {
                Script.UpdateJob(job.AutoID, job.Name);
            }
            else
            {
                //UserLM user = Session["User"] as UserLM;
                var userid = Convert.ToInt32(HttpContext.Request.Cookies["userid"].Value);
                Script.CreateJob(userid, job.PlanAutoID, job.Name, 3);
            }


            return Redirect("/Home/Index");
        }

        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult DeleteJob(int autoID, int pageIndex)
        {
            try
            {
                Script.DeleteJob(autoID);
                //UserLM user = Session["User"] as UserLM;
                var userid = Convert.ToInt32(HttpContext.Request.Cookies["userid"].Value);
                ViewData["PagingButtons"] = Paging(pageIndex, Script.GetJobCount(userid), "JobPageIndex");
                return View("JobList", Script.SearchJobs(userid, pageIndex, 3, "", "", "", "", "").Select(i => new JobListCellVM(i, pageIndex)));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new JsonResult() { Data = new { message = ex.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }


        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult ViewJob(int autoID)
        {
            JobLM job = Script.GetJob(autoID);
            Session["JobAutoID"] = job.AutoID;
            Session["JobLM"] = job;

            EvaluationContext context = Script.GetPlan(job.PlanID).CreateEvaluationContext();
            context.Settings = new Settings() { DecimalDigitCount = job.DecimalCount };

            Session["Context"] = context;

            Script.InitEvaluationContext(context, job);

            context.Update();

            return View(new JobVM(context, job));
        }

        [CheckLogin]
        [HttpPost]
        [OutputCache(Duration = 0)]
        public ActionResult UpdateSettings(SettingsSM settingsSM)
        {
            try
            {
                Settings settings = settingsSM.ToLM();

                EvaluationContext context = Session["Context"] as EvaluationContext;
                context.Settings = settings;
                context.Update();


                Script.UpdateJobDecimalCount(settingsSM.JobAutoID, settings.DecimalDigitCount);

                JobLM job = (JobLM)Session["JobLM"];
                if (job != null)
                {
                    job.DecimalCount = settings.DecimalDigitCount;
                }


                return new JsonResult() { Data = new { success = true }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (CustomDataException ex)
            {
                Response.StatusCode = 500;
                return new JsonResult() { Data = ex.Data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult GetJobStepList()
        {
            return PartialView("JobStepList", new JobVM(Session["Context"] as EvaluationContext, Session["JobLM"] as JobLM));
        }

        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult GetJobStepArea(int stepIndex)
        {
            EvaluationContext context = Session["Context"] as EvaluationContext;
            return PartialView("JobStepArea", new JobStepAreaVM(context, context.Steps[stepIndex], stepIndex, Session["UpdateCount"] == null ? 0 : (int)Session["UpdateCount"]));
        }

        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult GetStepImage(int stepIndex)
        {
            EvaluationContext context = Session["Context"] as EvaluationContext;
            return File(context.Steps[stepIndex].ImageData, "image/png");
        }

        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult GetResultImage(int stepIndex, int variableIndex)
        {
            EvaluationContext context = Session["Context"] as EvaluationContext;

            using (MemoryStream stream = new MemoryStream())
            {
                Bitmap bitmap = context.GetValue(context.Steps[stepIndex].OutVariables[variableIndex]) as Bitmap;
                bitmap.Save(stream, ImageFormat.Png);
                return File(stream.GetBuffer().Take((int)stream.Length).ToArray(), "image/png");
            }
        }

        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult SaveJobPlan()
        {
            try
            {
                if (Session["JobAutoID"] != null)
                {
                    int jobAutoID = (int)Session["JobAutoID"];
                    string planID = Script.GetJob(jobAutoID).PlanID;

                    EvaluationContext context = Session["Context"] as EvaluationContext;

                    Script.UpdatePlan(planID, context.Plan);
                    Script.UpdateJob(jobAutoID, context);
                }


                return new JsonResult() { Data = new { success = true }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new JsonResult() { Data = new { message = ex.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [CheckLogin]
        [HttpPost]
        [OutputCache(Duration = 0)]
        public ActionResult UpdateInVariables(InVariableSM[] variables)
        {
            try
            {
                EvaluationContext context = Session["Context"] as EvaluationContext;

                foreach (InVariableSM variable in variables)
                {
                    context.SetValueString(variable.Name, variable.Value);
                }


                context.Update();

                return new JsonResult() { Data = new { success = true }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new JsonResult() { Data = new { message = ex.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [CheckLogin]
        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult ExportAsWordDocument()
        {
            EvaluationContext context = Session["Context"] as EvaluationContext;

            MemoryStream stream = new MemoryStream();
            Report.FromEvaluationContext(context).SaveAsWordDocument(stream);

            return File(stream.GetBuffer().Take((int)stream.Length).ToArray(), "application/msword", context.Plan.Title + ".docx");
        }

        [CheckLogin]
        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult GetGraph()
        {
            EvaluationContext context = Session["Context"] as EvaluationContext;

            using (MemoryStream stream = new MemoryStream())
            {
                Bitmap bitmap = ComputePlanVisualizer.Visualize(context);
                bitmap.Save(stream, ImageFormat.Png);
                return File(stream.GetBuffer().Take((int)stream.Length).ToArray(), "image/png");
            }
        }

        [CheckLogin]
        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult GetGridContent(int stepIndex, int variableIndex)
        {
            EvaluationContext context = Session["Context"] as EvaluationContext;
            object value = context.Steps[stepIndex].InValues[variableIndex];

            SourceVariable variable = context.Steps[stepIndex].InSourceVariables[variableIndex];
            bool isMatrix = variable.Type == Planning.DataType.Matrix;

            Style style = context.Plan.Styles.FirstOrDefault(i => i.Target == variable.Name);
            string[] columnNames = style != null && style.ColumnNames != null ? style.ColumnNames : new string[] { };
            string rowName = style != null ? style.RowName : "";
            int rowHeaderWidth = style != null && style.RowHeaderWidth != 0 ? style.RowHeaderWidth : 50;

            int rowCount = isMatrix ? 2 : 1;
            int columnCount;

            bool allowEditRow;
            bool allowEditColumn;

            if (isMatrix)
            {
                allowEditRow = true;
                if (columnNames.Length == 0)
                {
                    allowEditColumn = true;
                    columnCount = 5;
                }
                else
                {
                    allowEditColumn = false;
                    columnCount = columnNames.Length;
                }
            }
            else
            {
                bool hasSize = style != null && style.Size > 0;
                allowEditRow = false;
                allowEditColumn = !hasSize;
                columnCount = !hasSize ? 5 : style.Size;
            }

            string[] cells = new string[] { };

            if (value is Matrix)
            {
                Matrix matrix = value as Matrix;
                rowCount = matrix.RowCount;
                columnCount = matrix.ColumnCount;
                cells = matrix.Items.Select(i => !double.IsNaN(i) ? i.ToString() : "-").ToArray();
            }
            else if (value is Vector)
            {
                Vector vector = value as Vector;
                columnCount = vector.Size;
                cells = vector.Items.Select(i => !double.IsNaN(i) ? i.ToString() : "-").ToArray();
            }


            return Json(new
            {
                rowCount = rowCount,
                columnCount = columnCount,
                cells = cells,
                rowHeaderWidth = rowHeaderWidth,
                rowName = rowName,
                columnNames = columnNames,
                allowEditRow = allowEditRow,
                allowEditColumn = allowEditColumn
            }, JsonRequestBehavior.AllowGet);
        }

        [CheckLogin]
        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult UploadPlan(string planID)
        {
            if (Session["ErrorMessage"] != null)
            {
                ViewData.Add("Message", Session["ErrorMessage"]);
                Session.Remove("ErrorMessage");
            }


            ViewData["PlanID"] = planID;

            return View();
        }

        /*
        [CheckLogin]
        [HttpPost]
        [OutputCache(Duration = 0)]
        public ActionResult UploadPlan(string planID, HttpPostedFileBase file)
        {
            if (file == null)
            {
                Session["ErrorMessage"] = "请选择一个实验文件上传。";
                return Redirect("/Home/UploadPlan");
            }


            try
            {
                if (string.IsNullOrEmpty(planID))
                {
                    Script.AddPlan(file.InputStream);
                }
                else
                {
                    Script.UpdatePlan(planID, file.InputStream);
                }

                return Redirect("/Home/Plans");
            }
            catch (Exception ex)
            {
                Session["ErrorMessage"] = ex.Message;
                return Redirect("/Home/UploadPlan");
            }
        }
         */

        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult DeletePlan(int autoID, int pageIndex)
        {
            try
            {
                Script.DeletePlan(autoID);
                ViewData["PagingButtons"] = Paging(pageIndex, Script.GetPlanCount("", "", "", "", ""), "PlanPageIndex");
                return PartialView("PlanList", Script.SearchPlans(pageIndex, 3).Select(i => new PlanListCellVM(i, pageIndex)));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new JsonResult() { Data = new { message = ex.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult CreatePlan()
        {
            return View("EditPlan", new EditPlanVM());
        }

        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult EditPlan(string planID)
        {
            return View(new EditPlanVM(planID, Script.GetPlan(planID)));
        }

        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult SavePlan(EditPlanSM sm)
        {
            try
            {
                //UserLM user = Session["User"] as UserLM;
                var userid = Convert.ToInt32(HttpContext.Request.Cookies["userid"].Value);
                Plan plan = sm.ToPlan();
                plan.Author = userid.ToString();
                EvaluationContext context = plan.CreateEvaluationContext();

                string id;

                if (!string.IsNullOrEmpty(sm.ID))
                {
                    id = sm.ID;
                    Script.UpdatePlan(sm.ID, plan);
                }
                else
                {
                    id = Script.AddPlan(plan).ID;
                }

                return new JsonResult() { Data = new { success = true, PlanID = id }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                //Response.StatusCode = 500;
                return new JsonResult() { Data = new { message = ex.Message.Replace("\r\n", "<br/>") }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult DeleteStep(string editID, int stepIndex)
        {
            try
            {
                EvaluationContext context = Session["Context"] as EvaluationContext;
                context.DeleteStep(stepIndex);
                context.Refresh();

                int newStepIndex = stepIndex;
                if (newStepIndex > context.Steps.Length - 1)
                {
                    newStepIndex = context.Steps.Length - 1;
                }


                SetUpdateCount(editID);

                return new JsonResult() { Data = new { success = true, stepIndex = newStepIndex }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new JsonResult() { Data = new { message = ex.Message.Replace("\r\n", "<br/>") }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult EditVariables()
        {
            EvaluationContext context = Session["Context"] as EvaluationContext;
            return PartialView("EditVariables", new EditPlanVM((Session["JobLM"] as JobLM).PlanID, context.Plan).Variables);
        }

        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult SaveVariables(EditPlanSM sm)
        {
            try
            {
                EvaluationContext context = Session["Context"] as EvaluationContext;
                context.Plan.Variables = sm.ToSourceVariables();
                context.Plan.Styles = sm.ToStyles();
                context.Refresh();


                SetUpdateCount(sm.EditID);

                return new JsonResult() { Data = new { success = true }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new JsonResult() { Data = new { message = ex.Message.Replace("\r\n", "<br/>") }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult InsertStep(string editID, int stepIndex)
        {
            try
            {
                EvaluationContext context = Session["Context"] as EvaluationContext;
                List<SourceExpression> expressions = context.Plan.Expressions.ToList();
                expressions.Insert(stepIndex, new SourceExpression()
                {
                    Title = "New step " + stepIndex,
                    Description = "New step " + stepIndex,
                    Expression = "1"
                });
                context.Plan.Expressions = expressions.ToArray();
                context.Refresh();

                SetUpdateCount(editID);

                return new JsonResult() { Data = new { success = true }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new JsonResult() { Data = new { message = ex.Message.Replace("\r\n", "<br/>") }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult EditStep(int stepIndex)
        {
            EvaluationContext context = Session["Context"] as EvaluationContext;
            return PartialView("EditStep", new EditStepVM(context.Plan.Expressions[stepIndex]));
        }

        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult UpdateStep(EditStepSM sm)
        {
            try
            {
                EvaluationContext context = Session["Context"] as EvaluationContext;
                SourceExpression expression = context.Plan.Expressions[sm.Index];
                expression.Title = sm.Title;
                expression.Description = sm.Description;
                expression.Expression = sm.Expression;
                expression.Condition = sm.Condition;


                context.Refresh();

                SetUpdateCount(sm.EditID);


                return new JsonResult() { Data = new { success = true }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new JsonResult() { Data = new { message = ex.Message.Replace("\r\n", "<br/>") }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [CheckLogin]
        [OutputCache(Duration = 0)]
        [HttpPost]
        public ActionResult ImportGrid(int stepIndex, int variableIndex, HttpPostedFileBase file)
        {
            try
            {
                EvaluationContext context = Session["Context"] as EvaluationContext;

                string variableName = context.Steps[stepIndex].InSourceVariables[variableIndex].Name;
                context.ImportMatrix(variableName, file.InputStream);

                context.Update();

                return new JsonResult() { Data = new { success = true }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new JsonResult() { Data = new { message = ex.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [CheckLogin]
        [OutputCache(Duration = 0)]
        public ActionResult ExportGrid(int stepIndex, int variableIndex)
        {
            try
            {
                EvaluationContext context = Session["Context"] as EvaluationContext;

                string variableName = context.Steps[stepIndex].InSourceVariables[variableIndex].Name;
                Matrix m = context.GetValue(variableName) as Matrix;
                Session["ExportGrid"] = m;
                var file = CsvParser.ParseToWorkbook(m);
                var stream = file.SaveToStream();
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", variableName + ".xls");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new JsonResult() { Data = new { message = ex.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public string ChangePass(string oldPass, string newPass)
        {
            var user = new UserEntity();
            var email = HttpContext.Request.Cookies["user"].Value;
            var dal = new UserDAL();
            user = dal.GetUser(email, oldPass);
            if (user == null)
                return "oldPass";
            else
            {
                user.Password = newPass;
                var result = dal.UpdateUserPass(user);
                if (result)
                {
                    return "success";
                }
                else
                    return "error";
            }
        }

        [CheckLogin]
        public ActionResult DrawTest()
        {
            return View();
        }
    }
}