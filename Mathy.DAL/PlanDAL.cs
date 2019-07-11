using Mathy.Model.Entity;
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

        public bool AddPlanRepository(string planRepositoryID, string text)
        {
            string sql = @" INSERT  INTO [dbo].[PlanRepository]
                                            ( [PlanID], [Text] )
                                    VALUES  ( @PlanID, @Text ) ";
            return Excute(sql, new { PlanID = planRepositoryID, Text = text });
        }

        public PlanRepositoryEntity GetRepository(string planRepositoryID)
        {
            string sql = @" SELECT * FROM  [dbo].[PlanRepository] (nolock) where PlanID = @PlanID ";
            return QueryFirstOrDefault<PlanRepositoryEntity>(sql, new { PlanID = planRepositoryID });
        }

        public bool CopyRepository(string formID, string toID)
        {
            string sql = @" INSERT  INTO [dbo].[PlanRepository]
        ( [PlanID] ,
          [Text]
        )
        SELECT  @NewID ,
                [Text]
        FROM    [dbo].[PlanRepository]
        WHERE   planID = @OldID ";
            return Excute(sql, new { OldID = formID, NewID = toID });
        }
    }
}
