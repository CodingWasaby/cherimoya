using Petunia.LogicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mathy.Web.Models
{
    public class EditJobVM
    {
        public EditJobVM(JobLM lm)
        {
            AutoID = lm.AutoID;
            PlanAutoID = lm.PlanAutoID;
            Name = lm.Name;
            PlanTitle = lm.PlanTitle;
        }


        public int AutoID { get; set; }

        public int PlanAutoID { get; set; }

        public string Name { get; set; }

        public string PlanTitle { get; set; }
    }
}