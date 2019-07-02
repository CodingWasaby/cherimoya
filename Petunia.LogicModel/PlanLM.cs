using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int ReferenceCount { get; set; }

        public int AuthFlag { get; set; }
    }
}
