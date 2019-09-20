using Mathy.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.DAL
{
    public class MCMDAL : BaseDAL
    {
        public int SaveMCM(MCM mcm)
        {
            string sql = @" UPDATE MCM
                                        SET DeleteFlag = 1
                                        WHERE JobID = @JobID           
                                      INSERT INTO [dbo].[MCM]
                                   ([JobID]
                                   ,[MCMInfo]
                                   ,[DeleteFlag])
                             VALUES
                                   (@JobID
                                   ,@MCMInfo
                                   ,0)
                        SELECT @@identity  ";
            return SaveAndReturnID<int>(sql, mcm);
        }

        public MCM GetMCM(int jobID)
        {
            string sql = @" SELECT * from MCM (nolock)
                                     where JobID=@JobID  AND  DeleteFlag=0";
            return QueryFirstOrDefault<MCM>(sql, new { JobID = jobID });
        }
    }
}
