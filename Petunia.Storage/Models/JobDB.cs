using JacarandaX;
using Petunia.LogicModel;
using System;

namespace Petunia.Storage.Models
{
    [Table]
    class JobDB : Entity
    {
        [Field]
        [DbPrimaryKey]
        public int AutoID { get; set; }

        [Field]
        public int PlanAutoID { get; set; }

        [Field]
        public string PlanID { get; set; }

        [Field]
        public string Name { get; set; }

        [Field]
        public int UserAutoID { get; set; }

        [Field]
        public string PlanTitle { get; set; }

        [Field]
        public bool IsComplete { get; set; }

        [Field]
        public DateTime CreateTime { get; set; }

        [Field]
        public DateTime UpdateTime { get; set; }

        [Field]
        public string Variables { get; set; }

        [Field]
        public int DecimalCount { get; set; }


        public static JobLM ToLM(JobDB db)
        {
            return db == null ? null : new JobLM()
            {
                AutoID = db.AutoID,
                PlanAutoID = db.PlanAutoID,
                PlanID = db.PlanID,
                Name = db.Name,
                UserAutoID = db.UserAutoID,
                PlanTitle = db.PlanTitle,
                IsComplete = db.IsComplete,
                CreateTime = db.CreateTime,
                UpdateTime = db.UpdateTime,
                Variables = db.Variables,
                DecimalCount = db.DecimalCount
            };
        }

        public static JobDB ToDB(JobLM lm)
        {
            return new JobDB()
            {
                AutoID = lm.AutoID,
                PlanAutoID = lm.PlanAutoID,
                PlanID = lm.PlanID,
                Name = lm.Name,
                UserAutoID = lm.UserAutoID,
                PlanTitle = lm.PlanTitle,
                IsComplete = lm.IsComplete,
                CreateTime = lm.CreateTime,
                UpdateTime = lm.UpdateTime,
                Variables = lm.Variables,
                DecimalCount = lm.DecimalCount
            };
        }
    }
}
