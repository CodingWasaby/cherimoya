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
            string sql = @" SELECT p.AutoID,
                                     p.ID,
                                     p.Title,
                                     p.Author,
                                     p.Description,
                                     p.CreateTime,
                                     p.PlanType,
                                     p.DeleteFlag,
                                     p.AuthFlag,
                                     p.PlanCategory,
                                     p.PlanRepository,
                                     p.SeqNo,
                                     p.UserRole,
                                     p.ReferenceCount ReferenceCountOld,
                               COUNT(j.PlanID) ReferenceCount
                        FROM dbo.PlanDB p (NOLOCK)
                            LEFT JOIN dbo.Quote j (NOLOCK)
                                ON p.AutoID = j.PlanID {0}      
	                          WHERE p.DeleteFlag=0
                         ";
            string order = " ISNULL(SeqNo, 99999), CreateTime ";
            string jParam = "";
            if (!string.IsNullOrEmpty(planName))
            {
                sql += " and p.Title LIKE '%" + planName + "%'";
            }
            if (!string.IsNullOrEmpty(content))
            {
                sql += " and p.Description LIKE '%" + content + "%'";
            }
            if (!string.IsNullOrEmpty(begindate))
            {
                jParam += " and j.CreateTime >= @BeginDate";
            }
            var endDate = Convert.ToDateTime("2100-01-01");
            if (!string.IsNullOrEmpty(enddate))
            {
                jParam += " and j.CreateTime <= @EndDate";
                endDate = Convert.ToDateTime(endDate).Date.AddDays(1).AddSeconds(-1);
            }
            sql = string.Format(sql, jParam);
            if (!string.IsNullOrEmpty(author))
            {
                sql += " and p.Author =@Author ";
                order = " CreateTime ";
            }
            if (!string.IsNullOrEmpty(planName))
            {
                sql += " and Title LIKE '%" + planName + "%'";
            }
            if (!string.IsNullOrEmpty(category))
            {
                sql += " and PlanCategory =@PlanCategory ";
            }

            sql += isAuth ? " and ( AuthFlag=1 or ( PlanType<>2 OR Author = @Author ) )" : " and AuthFlag=0 and PlanType=0";
            sql += @"GROUP BY p.AutoID,
                                 p.ID,
                                 p.Title,
                                 p.Author,
                                 p.Description,
                                 p.CreateTime,
                                 p.PlanType,
                                 p.DeleteFlag,
                                 p.AuthFlag,
                                 p.PlanCategory,
                                 p.PlanRepository,
                                 p.UserRole,
                                 p.ReferenceCount,
                                 p.SeqNo";
            var pageData = QueryPage<PlanLM>(sql, new PageInfo { PageIndex = pageIndex, PageSize = pageSize, OrderField = order, DescString = "DESC" }, new { BeginDate = begindate, EndDate = endDate, Author = author, PlanCategory = category });
            if (!string.IsNullOrEmpty(begindate))
            {
                var bg = Convert.ToDateTime(begindate);
                var line = Convert.ToDateTime("2021-09-01");
                if (bg < line)
                {
                    foreach (var n in pageData.Data)
                    {
                        n.ReferenceCount += n.ReferenceCountOld;
                    }
                }
            }
            return pageData;
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

        public bool UpdatePlanRole(int planID, int userRole)
        {
            string sql = @"
                            UPDATE dbo.PlanDB SET UserRole =@UserRole
                            WHERE AutoID = @PlanID ";
            return Excute(sql, new { UserRole = userRole, PlanID = planID });
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

        public void CreateUserRoleColumn()
        {
            string sql = @"IF NOT EXISTS
                            (
                                SELECT *
                                FROM syscolumns
                                WHERE id = OBJECT_ID('PlanDB')
                                      AND name = 'UserRole'
                            )
                                ALTER TABLE[PlanDB] ADD UserRole INT; ";
            Excute(sql);

            sql = @" UPDATE dbo.PlanDB
                    SET UserRole = 2
                    WHERE UserRole IS NULL; ";
            Excute(sql);
        }

        public void CreateQuoteTable()
        {
            string sql = @"
                if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Quote]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

                CREATE TABLE [dbo].Quote (


	                [PlanID] [int]  NOT NULL,

	                [CreateTime] [datetime] NOT NULL 

                ) ON [PRIMARY] ";
            Excute(sql);
        }

        public void IncreaseQuote(int planID)
        {
            string sql = @"INSERT INTO [dbo].[Quote]
                       ([PlanID]
                       ,[CreateTime])
                 VALUES
                       (@PlanID,
                       getdate()) ";
            Excute(sql, new { PlanID = planID });
        }
    }
}
