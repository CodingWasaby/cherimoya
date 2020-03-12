using System;

namespace Petunia.LogicModel
{
    public class JobLM
    {
        public int AutoID { get; set; }

        public int PlanAutoID { get; set; }

        public string PlanID { get; set; }

        public string Name { get; set; }

        public int UserAutoID { get; set; }

        public string PlanTitle { get; set; }

        public bool IsComplete { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public string Variables { get; set; }

        public int DecimalCount { get; set; }

        public int DeleteFlag { get; set; }
    }
}
