using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.DAL
{
    public class JobDAL : BaseDAL
    {
        public bool DeleteJob(int jobID)
        {
            string sql = @" UPDATE JobDB
                                    SET DeleteFlag = 1
                                    WHERE AutoID = @AutoID";
            return Excute(sql, new { AutoID = jobID });
        }
    }
}
