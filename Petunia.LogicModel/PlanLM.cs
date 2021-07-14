using System;

namespace Petunia.LogicModel
{
    public class PlanLM
    {
        public int AutoID { get; set; }

        public string ID { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public DateTime CreateTime { get; set; }

        public int PlanType { get; set; }

        public string PlanCategory { get; set; }

        public int ReferenceCount { get; set; }

        public int AuthFlag { get; set; }

        public int SeqNo { get; set; }
        public int UserRole { get; set; }
    }
}
