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
    public class PlanStorage
    {
        public static void Save(PlanLM lm)
        {
            PlanDB db = PlanDB.ToDB(lm);
            db.Save();

            lm.AutoID = db.AutoID;
        }


        public static PlanLM Get(int autoID)
        {
            return PlanDB.ToLM(Database.Table<PlanDB>().Where(i => i.AutoID == autoID).First());
        }

        public static PlanLM GetByID(string id)
        {
            return PlanDB.ToLM(Database.Table<PlanDB>().Where(i => i.ID == id).First());
        }

        public static PlanLM[] Search(int pageIndex, int pageSize, string author)
        {
            var temp = Database.Table<PlanDB>().Where(m => m.DeleteFlag == 0).OrderByDescending(i => i.CreateTime).OrderByDescending(m => m.ReferenceCount).Skip(pageIndex * pageSize).Take(pageSize);
            if (!string.IsNullOrEmpty(author))
                temp = temp.Where(m => m.Author == author);
            return temp.ToArray().Select(i => PlanDB.ToLM(i)).ToArray();
        }

        public static int GetCount(string author, string planName, string begindate, string enddate, string content)
        {
            var query = Database.Table<PlanDB>();
            if (!string.IsNullOrEmpty(author))
                query = query.Where(m => m.Author == author);

            if (!string.IsNullOrEmpty(planName))
            {
                query = query.Where(m => m.Title.Contains(planName));
            }
            if (!string.IsNullOrEmpty(content))
            {
                query = query.Where(m => m.Description.Contains(content));
            }
            if (!string.IsNullOrEmpty(begindate))
            {
                var d = DateTime.Now;
                if (DateTime.TryParse(begindate, out d))
                    query = query.Where(m => m.CreateTime >= d);
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                var d = DateTime.Now;
                if (DateTime.TryParse(enddate, out d))
                    query = query.Where(m => m.CreateTime < d.Date.AddDays(1));
            }

            return query.Count();
        }

        public static void Delete(int autoID)
        {
            Database.ForEach<PlanDB>(i => i.AutoID == autoID).Delete();
        }
    }
}
