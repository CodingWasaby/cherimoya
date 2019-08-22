using Dandelion.Serialization;
using Mathy;
using Mathy.Planning;
using Petunia.LogicModel;
using Petunia.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petunia
{
    public class Script
    {
        public static void Init()
        {
            AppStore.Init();
            MathyShell.Init(AppStore.PathResolver.MathyPath);

            StorageInit.Init();
        }

        public static PlanLM[] SearchPlans(int pageIndex, int pageSize, string author, bool isAuth)
        {
            return PlanStorage.Search(pageIndex, pageSize, author, isAuth);
        }

        public static int GetPlanCount(string author, string planName, string begindate, string enddate, string content, bool isAuth)
        {
            return PlanStorage.GetCount(author, planName, begindate, enddate, content, isAuth);
        }

        public static Plan GetPlan(string id)
        {
            var planlm = PlanStorage.GetByID(id);
            var plan = AppStore.PlanRepository.Get(id);
            if (planlm != null)
                plan.Description = planlm.Description;
            return plan;
        }

        public static Plan GetPlan(int autoID)
        {
            var planlm = PlanStorage.Get(autoID);
            var plan = AppStore.PlanRepository.Get(planlm.ID);
            plan.Description = planlm.Description;
            return plan;
        }

        public static Plan GetPlan(int autoID, string textID)
        {
            var planlm = PlanStorage.Get(autoID);
            var plan = AppStore.PlanRepository.Get(textID);
            plan.Description = planlm.Description;
            return plan;
        }

        public static PlanLM GetPlanLM(int autoID)
        {
            return PlanStorage.Get(autoID);
        }

        public static PlanLM AddPlan(Plan p)
        {
            Stream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(new JsonSerializer() { PrettifyJson = true }.SerializeToString(p)));

            string id = Guid.NewGuid().ToString();
            Plan plan = AppStore.PlanRepository.Save(id, stream);
            PlanLM lm = new PlanLM()
            {
                ID = id,
                Title = plan.Title,
                Author = p.Author,
                Description = plan.Description,
                CreateTime = DateTime.Now,
                PlanType = p.PlanType
            };
            PlanStorage.Save(lm);
            return lm;
        }

        public static void UpdatePlan(string id, Plan newPlan)
        {
            Plan plan = GetPlan(id);

            plan.Title = newPlan.Title;
            plan.Description = newPlan.Description;
            plan.Author = newPlan.Author;
            plan.Variables = newPlan.Variables;
            plan.Expressions = newPlan.Expressions;
            plan.Styles = newPlan.Styles;
            plan.PlanType = newPlan.PlanType;

            AppStore.PlanRepository.Save(id, new MemoryStream(System.Text.Encoding.UTF8.GetBytes(new JsonSerializer() { PrettifyJson = true }.SerializeToString(plan))));


            PlanLM planLM = PlanStorage.GetByID(id);
            if (planLM != null)
            {
                planLM.Title = newPlan.Title;
                planLM.Description = newPlan.Description;
                planLM.Author = newPlan.Author;
                planLM.PlanType = newPlan.PlanType;
                PlanStorage.Save(planLM);
            }
        }

        public static void UpdatePlan(string id, Stream stream)
        {
            AppStore.PlanRepository.Save(id, stream);
        }

        public static UserLM GetAuthUser(string userID, string password)
        {
            AuthUser authUser = AppStore.UserAuthenticator.GetAuthUser(userID, password);

            if (authUser == null)
            {
                return null;
            }


            UserLM user = null;

            if (user == null)
            {
                user = new UserLM { ID = userID, Password = password, Name = authUser.Name };
            }


            return user;
        }

        /*
        public static UserLM GetAuthUser(string userID, string password)
        {
            AuthUser authUser = AppStore.UserAuthenticator.GetAuthUser(userID, password);

            if (authUser == null)
            {
                return null;
            }


            UserLM user = UserStorage.Get(userID);

            if (user == null)
            {
                user = new UserLM { ID = userID, Name = authUser.Name };
            }

            UserStorage.Save(user);


            return user;
        }
        */

        public static void CreateJob(int userAutoID, int planAutoID, string name, int decimalCount)
        {
            PlanLM planLM = GetPlanLM(planAutoID);
            Plan plan = GetPlan(planLM.AutoID);
            DateTime now = DateTime.Now;

            string id = Guid.NewGuid().ToString();
            AppStore.PlanRepository.Copy(planLM.ID, id);

            JobStorage.Save(
                new JobLM()
                {
                    Name = name,
                    CreateTime = now,
                    IsComplete = plan.Variables.Length == 0,
                    PlanAutoID = planAutoID,
                    PlanID = id,
                    PlanTitle = plan.Title,
                    UpdateTime = now,
                    UserAutoID = userAutoID,
                    Variables = new JsonSerializer().SerializeToString(new Dictionary<string, object>()),
                    DecimalCount = decimalCount
                });

            planLM.ReferenceCount++;
            PlanStorage.Save(planLM);
        }

        public static void UpdateJob(int autoID, string name)
        {
            JobLM job = JobStorage.Get(autoID);
            job.Name = name;

            JobStorage.Save(job);
        }


        public static JobLM[] SearchJobs(int userAutoID, int pageIndex, int pageSize, string jobName, string planName, string begindate, string enddate, string isFinish)
        {
            return JobStorage.Search(userAutoID, pageIndex, pageSize, jobName, planName, begindate, enddate, isFinish);
        }

        public static int GetJobCount(int userAutoID)
        {
            return JobStorage.GetCount(userAutoID);
        }

        public static JobLM GetJob(int autoID)
        {
            return JobStorage.Get(autoID);
        }

        public static void InitEvaluationContext(EvaluationContext context, JobLM job)
        {
            IDictionary variables = new JsonDeserializer().DeserializeString(job.Variables) as IDictionary;

            foreach (string key in variables.Keys)
            {
                context.SetValueString(key, (string)variables[key]);
            }
        }

        public static void UpdateJobDecimalCount(int jobAutoID, int decimalCount)
        {
            JobLM job = JobStorage.Get(jobAutoID);

            if (job != null)
            {
                job.DecimalCount = decimalCount;
                JobStorage.Save(job);
            }
        }

        public static void UpdateJob(int jobAutoID, EvaluationContext context)
        {
            Dictionary<string, string> variables = new Dictionary<string, string>();

            foreach (KeyValuePair<string, object> variable in context.SourceVariables)
            {
                if (variable.Value != null)
                {
                    variables.Add(variable.Key, variable.Value.ToString());
                }
            }


            JobLM job = JobStorage.Get(jobAutoID);
            job.IsComplete = context.Steps.All(i => i.State != StepState.Unready);
            job.Variables = new JsonSerializer().SerializeToString(variables);
            job.UpdateTime = DateTime.Now;


            JobStorage.Save(job);
        }

        public static void DeleteJob(int autoID)
        {
            JobLM job = JobStorage.Get(autoID);

            JobStorage.Delete(autoID);

            PlanLM plan = PlanStorage.Get(job.PlanAutoID);
            plan.ReferenceCount--;
            PlanStorage.Save(plan);
        }

        public static void DeletePlan(int autoID)
        {
            PlanLM plan = GetPlanLM(autoID);


            PlanStorage.Delete(autoID);
            JobStorage.DeleteByPlanAutoID(autoID);

            AppStore.PlanRepository.Delete(plan.ID);
        }

        public static Stream GetPlanStream(string id)
        {
            return new FileStream(AppStore.PathResolver.GetPlanPath(id), FileMode.Open, FileAccess.Read);
        }

        public static FuncDoc[] GetFuncDocs()
        {
            if (AppStore.Docs == null)
            {
                FuncDoc[] docs = new JsonDeserializer().DeserializeString(System.IO.File.ReadAllText(AppStore.PathResolver.FuncDocPath, System.Text.Encoding.UTF8), typeof(FuncDoc[])) as FuncDoc[];

                foreach (FuncDoc doc in docs)
                {
                    doc.Article = Article.Parse(AppStore.PathResolver.GetFuncArticlePath(doc.Name));
                }


                AppStore.Docs = docs;
            }


            return AppStore.Docs;
        }
    }
}
