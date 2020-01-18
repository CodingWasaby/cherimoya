using Petunia.LogicModel;
using System;

namespace Mathy.Web.Models
{
    public class JobListCellVM
    {
        public JobListCellVM(JobLM lm, int pageIndex)
        {
            AutoID = lm.AutoID;
            Name = lm.Name;
            PlanTitle = lm.PlanTitle;
            IsComplete = lm.IsComplete;
            CreateTime = lm.CreateTime;
            UpdateTime = lm.UpdateTime;
            ViewLink = string.Format("/Home/ViewJob?autoID={0}", lm.AutoID);
            UpdateLink = string.Format("/Home/UpdateJob?autoID={0}", lm.AutoID);
            DeleteLink = string.Format("javascript:deleteJob({0},{1});", lm.AutoID, pageIndex);
        }


        public int AutoID { get; set; }

        public int PageIndex { get; set; }

        public string Name { get; set; }

        public string PlanTitle { get; set; }

        public bool IsComplete { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public string ViewLink { get; set; }

        public string UpdateLink { get; set; }

        public string DeleteLink { get; set; }
    }
}