using JacarandaX;
using Petunia.LogicModel;
using Petunia.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petunia.Storage
{
    public class JobStorage
    {
        public static int Save(JobLM lm)
        {
            JobDB db = JobDB.ToDB(lm);
            db.Save();

            lm.AutoID = db.AutoID;
            return lm.AutoID;
        }

        public static JobLM Get(int autoID)
        {
            return JobDB.ToLM(Database.Table<JobDB>().First(i => i.AutoID == autoID));
        }

        public static JobLM[] Search(int userAutoID, int pageIndex, int pageSize, string jobName, string planName, string begindate, string enddate, string isFinish)
        {
            var query = Database.Table<JobDB>();
            if (!string.IsNullOrEmpty(jobName))
                query = query.Where(m => m.Name.Contains(jobName));
            if (!string.IsNullOrEmpty(planName))
                query = query.Where(m => m.PlanTitle.Contains(planName));
            if (!string.IsNullOrEmpty(begindate))
                query = query.Where(m => m.CreateTime >= Convert.ToDateTime(begindate));
            if (!string.IsNullOrEmpty(enddate))
                query = query.Where(m => m.CreateTime <= Convert.ToDateTime(enddate).AddDays(1));
            if (!string.IsNullOrEmpty(isFinish) && isFinish != "不限")
            {
                if (isFinish == "完成")
                    query = query.Where(m => m.IsComplete == true);
                else
                    query = query.Where(m => m.IsComplete == false);
            }
            if (userAutoID == 0)
                return query.Skip(pageIndex * pageSize).Take(pageSize).OrderByDescending(i => i.UpdateTime).ToArray().Select(i => JobDB.ToLM(i)).ToArray();
            return query.Where(i => i.UserAutoID == userAutoID).Skip(pageIndex * pageSize).Take(pageSize).OrderByDescending(i => i.UpdateTime).ToArray().Select(i => JobDB.ToLM(i)).ToArray();
        }

        public static int GetCount(int userAutoID)
        {
            if (userAutoID == 0)
                Database.Table<JobDB>().Count();
            return Database.Table<JobDB>().Where(m => m.UserAutoID == userAutoID).Count();
        }

        public static void Delete(int autoID)
        {
            Database.ForEach<JobDB>(i => i.AutoID == autoID).Delete();
        }

        public static void DeleteByPlanAutoID(int planAutoID)
        {
            Database.ForEach<JobDB>(i => i.PlanAutoID == planAutoID).Delete();
        }
    }
}
