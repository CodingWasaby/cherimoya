using Mathy.Model.Common;
using Mathy.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.DAL
{
    public class CoefficientDAL : BaseDAL
    {
        public int AddCoefficient(Coefficient coefficient, SqlTransaction tran)
        {
            string sql = @" INSERT  INTO dbo.Coefficient
                                    ( CoefficientName ,
                                      DeleteFlag ,
                                      CoefficientContent,
                                      Creator,
                                      CreateTime
                                    )
                            VALUES  ( @CoefficientName ,
                                      0 ,
                                      @CoefficientContent,
                                      @Creator,
                                      GetDate()
                                    )  SELECT @@IDENTITY ";
            return SaveAndReturnID<int>(sql, coefficient, tran);
        }

        public bool UpdateCoefficient(Coefficient coefficient, SqlTransaction tran)
        {
            string sql = @"UPDATE  Coefficient
                    SET     CoefficientName = @CoefficientName ,
                            CoefficientContent = @CoefficientContent
                    WHERE   CoefficientID = @CoefficientID ";
            return Excute(sql, coefficient, tran);
        }

        public bool AddCoefficientDetails(List<CoefficientDetail> coefficientDetails, SqlTransaction tran)
        {
            return BulkToDB(coefficientDetails, "CoefficientDetail", tran);
        }

        public PageList<Coefficient> GetCoefficients(PageInfo page, string coefficientName)
        {
            string sql = @" SELECT  *
                                            FROM    dbo.Coefficient(NOLOCK)
                                            WHERE   DeleteFlag = 0 ";
            page.OrderField = " CoefficientID";
            page.DescString = " DESC";
            if (!string.IsNullOrEmpty(coefficientName))
            {
                sql += " AND CoefficientName Like '%" + coefficientName + "%'";
            }
            return QueryPage<Coefficient>(sql, page, new { });
        }

        public Coefficient GetCoefficient(int coefficientID)
        {
            string sql = @" SELECT  *
                                            FROM    dbo.Coefficient(NOLOCK)
                                            WHERE  CoefficientID=@CoefficientID  ";
            return QueryFirstOrDefault<Coefficient>(sql, new { CoefficientID = coefficientID });
        }

        public List<CoefficientDetail> GetCoefficientDetail(int coefficientID)
        {
            string sql = @" 
                                SELECT  CoefficientDetailName ,
                                        CoefficientDetailValue ,
                                        CoefficientDetailIndex ,
                                        CoefficientDetailRow
                                FROM    dbo.CoefficientDetail (NOLOCK)
                                WHERE   CoefficientID = @CoefficientID ";
            return Query<CoefficientDetail>(sql, new { CoefficientID = coefficientID }).ToList();
        }

        public List<CoefficientDetail> GetCoefficientDetail(string coefficientName)
        {
            string sql = @"  SELECT CoefficientDetailName ,
                                                    CoefficientDetailValue ,
                                                    CoefficientDetailRow
                                             FROM   dbo.CoefficientDetail d ( NOLOCK )
                                                    JOIN dbo.Coefficient c ( NOLOCK ) ON d.CoefficientID = c.CoefficientID
                                             WHERE  c.CoefficientName = @CoefficientName
                                                    AND c.DeleteFlag = 0 ";
            return Query<CoefficientDetail>(sql, new { CoefficientName = coefficientName }).ToList();
        }

        public bool DeleteCoefficient(int coefficientID)
        {
            string sql = " UPDATE Coefficient SET DeleteFlag =1 WHERE CoefficientID =@CoefficientID ";
            return Excute(sql, new { CoefficientID = coefficientID });
        }

        public bool DeleteCoefficientDetails(int coefficientID, SqlTransaction tran)
        {
            string sql = " DELETE CoefficientDetail  WHERE CoefficientID = @CoefficientID ";
            return Excute(sql, new { CoefficientID = coefficientID }, tran);
        }
    }
}
