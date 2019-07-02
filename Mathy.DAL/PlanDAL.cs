using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.DAL
{
    public class PlanDAL : BaseDAL
    {
        public bool UpdatePlanAuthFlag(string planID, int authFlag)
        {
            string sql = @"
                            UPDATE dbo.PlanDB SET AuthFlag =@AuthFlag
                            WHERE ID = @PlanID ";
            return Excute(sql, new { AuthFlag = authFlag, PlanID = planID });
        }
    }
}
