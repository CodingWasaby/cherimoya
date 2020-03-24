using Mathy.Model.Common;
using Mathy.Model.Entity;
using Petunia.LogicModel;
using System;

namespace Mathy.DAL
{
    public class PlanDAL : BaseDAL
    {

        public PageList<PlanLM> GetPlanList(int pageIndex, int pageSize, string planName, string begindate, string enddate, string content, string author = "", bool isAuth = true, string category = "")
        {
            string sql = @" SELECT
	                                    *
                                    FROM PlanDB(NOLOCK)
                                    WHERE DeleteFlag = 0 ";

            if (!string.IsNullOrEmpty(planName))
            {
                sql += " and Title LIKE '%" + planName + "%'";
            }
            if (!string.IsNullOrEmpty(content))
            {
                sql += " and Description LIKE '%" + content + "%'";
            }
            if (!string.IsNullOrEmpty(begindate))
            {
                sql += " and CreateTime >= @BeginDate";
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                sql += " and CreateTime <= @EndDate";
            }
            if (!string.IsNullOrEmpty(author))
            {
                sql += " and Author =@Author ";
            }
            if (!string.IsNullOrEmpty(planName))
            {
                sql += " and Title LIKE '%" + planName + "%'";
            }
            if (!string.IsNullOrEmpty(category))
            {
                sql += " and PlanCategory =@PlanCategory ";
            }
            sql += isAuth ? " and AuthFlag=1 AND PlanType<>2 " : " and AuthFlag=0 and PlanType=0";
            return QueryPage<PlanLM>(sql, new PageInfo { PageIndex = pageIndex, PageSize = pageSize, OrderField = " ISNULL(SeqNo, 99999), CreateTime ", DescString = "DESC" }, new { BeginDate = begindate, EndDate = enddate, Author = author, PlanCategory = category });
        }

        public bool UpdatePlanAuthFlag(string planID, int authFlag)
        {
            string sql = @"
                            UPDATE dbo.PlanDB SET AuthFlag =@AuthFlag
                            WHERE ID = @PlanID ";
            return Excute(sql, new { AuthFlag = authFlag, PlanID = planID });
        }

        public bool UpdatePlanSeq(int planID, int seqNo)
        {
            string sql = @"
                            UPDATE dbo.PlanDB SET SeqNo =@SeqNo
                            WHERE AutoID = @PlanID ";
            return Excute(sql, new { SeqNo = seqNo, PlanID = planID });
        }

        public bool AddPlanRepository(string planRepositoryID, string text)
        {
            string sql = @" Delete  [dbo].[PlanRepository] where PlanID =@PlanID
                                     INSERT  INTO [dbo].[PlanRepository]
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
