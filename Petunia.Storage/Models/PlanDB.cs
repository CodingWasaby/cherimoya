using JacarandaX;
using Petunia.LogicModel;
using System;

namespace Petunia.Storage.Models
{
    [Table]
    class PlanDB : Entity
    {
        [Field]
        [DbPrimaryKey]
        public int AutoID { get; set; }

        [Field]
        public string ID { get; set; }

        [Field]
        public string Title { get; set; }

        [Field]
        public string Author { get; set; }

        [Field]
        public string Description { get; set; }

        [Field]
        public DateTime CreateTime { get; set; }

        [Field]
        public int ReferenceCount { get; set; }

        [Field]
        public int PlanType { get; set; }

        [Field]
        public string PlanCategory { get; set; }

        [Field]
        public int AuthFlag { get; set; }

        [Field]
        public int DeleteFlag { get; set; }


        public static PlanLM ToLM(PlanDB db)
        {
            return db == null ? null : new PlanLM()
            {
                AutoID = db.AutoID,
                ID = db.ID,
                Title = db.Title,
                Author = db.Author,
                Description = db.Description,
                CreateTime = db.CreateTime,
                ReferenceCount = db.ReferenceCount,
                PlanType = db.PlanType,
                AuthFlag = db.AuthFlag,
                PlanCategory = db.PlanCategory
            };
        }

        public static PlanDB ToDB(PlanLM lm)
        {
            return new PlanDB()
            {
                AutoID = lm.AutoID,
                ID = lm.ID,
                Title = lm.Title,
                Author = lm.Author,
                Description = lm.Description,
                CreateTime = lm.CreateTime,
                ReferenceCount = lm.ReferenceCount,
                PlanType = lm.PlanType,
                AuthFlag = lm.AuthFlag,
                PlanCategory = lm.PlanCategory
            };
        }

    }
}
