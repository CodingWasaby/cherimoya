using Mathy.Model.Common;
using Mathy.Model.Entity;
using Mathy.Model.Serach;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Mathy.DAL
{
    public class UserDAL : BaseDAL
    {
        public int InnitUser(UserEntity userEntity, SqlTransaction transaction = null)
        {
            return SaveAndReturnID<int>(@"INSERT  INTO dbo.UserDB
                                        ( Email ,
                                          Password ,
                                          Name ,
                                          CreateTime ,
                                          Company ,
                                          CellPhone ,
                                          TelPhone ,
                                          EnableDate ,
                                          DeleteFlag
                                        )
                                VALUES  ( @Email ,
                                          @Password ,
                                          @Name ,
                                          @CreateTime ,
                                          @Company ,
                                          @CellPhone ,
                                          @TelPhone ,
                                          @EnableDate ,
                                          0
                                        ) SELECT @@IDENTITY ", userEntity, transaction);
        }

        public UserEntity GetUser(string email)
        {
            string sql = @" SELECT  *
                            FROM    dbo.UserDB (NOLOCK)
                            WHERE   DeleteFlag = 0
                                    AND Email = @Email ";
            return QueryFirstOrDefault<UserEntity>(sql, new { Email = email });
        }

        public UserEntity GetUser(string email, string passWord)
        {
            string sql = @" SELECT  *
                            FROM    dbo.UserDB (NOLOCK)
                            WHERE   DeleteFlag = 0
                                    AND Email = @Email AND PassWord=@PassWord";
            return QueryFirstOrDefault<UserEntity>(sql, new { Email = email, PassWord = passWord });
        }

        public List<UserEntity> GetUsers(UserSearch search)
        {
            string sql = @" SELECT  *
                            FROM    dbo.UserDB (NOLOCK)
                            WHERE   DeleteFlag = 0 ";
            if (!string.IsNullOrEmpty(search.UserName))
            {
                sql += " AND Name Like '%" + search.UserName + "%'";
            }
            if (!string.IsNullOrEmpty(search.Company))
            {
                sql += " AND Company Like '%" + search.Company + "%'";
            }
            if (search.BeginDate.HasValue)
            {
                sql += " AND CreateTime > @BeginDate ";
            }
            if (search.EndDate.HasValue)
            {
                sql += " AND CreateTime < @EndDate ";
            }
            sql += " ORDER BY CreateTime DESC";
            return Query<UserEntity>(sql, search).ToList();
        }

        public PageList<UserEntity> GetUsers(UserSearch search, PageInfo page)
        {
            page.OrderField = "CreateTime";
            page.OrderField = "DESC";
            string sql = @" SELECT  *
                            FROM    dbo.UserDB (NOLOCK)
                            WHERE   DeleteFlag = 0 ";
            if (!string.IsNullOrEmpty(search.UserName))
            {
                sql += " AND UserName Like '%" + search.UserName + "%'";
            }
            if (!string.IsNullOrEmpty(search.Company))
            {
                sql += " AND Company Like '%" + search.Company + "%'";
            }
            if (search.BeginDate.HasValue)
            {
                sql += " AND CreateTime > @BeginDate ";
            }
            if (search.EndDate.HasValue)
            {
                sql += " AND CreateTime < @EndDate ";
            }
            return QueryPage<UserEntity>(sql, page, search);
        }

        public bool DeleteUser(string email, SqlTransaction transaction = null)
        {
            string sql = @" UPDATE dbo.UserDB SET DeleteFlag =1 WHERE Email = @Email";
            return Excute(sql, new { Email = email }, transaction);
        }

        public bool UpdateUserInfo(UserEntity userEntity, SqlTransaction transaction = null)
        {
            string sql = @"UPDATE  dbo.UserDB
                            SET     Name = @Name ,
                                    Company = @Company ,
                                    CellPhone = @CellPhone ,
                                    TelPhone = @TelPhone
                            WHERE   Email = @Email AND DeleteFlag = 0 ";
            return Excute(sql, userEntity, transaction);
        }

        public bool UpdateUserEnableDate(UserEntity userEntity, SqlTransaction transaction = null)
        {
            string sql = @" UPDATE  dbo.UserDB
                            SET    EnableDate = @EnableDate
                            WHERE   Email = @Email  AND DeleteFlag =0 ";
            return Excute(sql, userEntity, transaction);
        }

        public bool UpdateUserPass(UserEntity userEntity, SqlTransaction transaction = null)
        {
            string sql = @" UPDATE  dbo.UserDB
                            SET    Password = @Password
                            WHERE   Email = @Email  AND DeleteFlag =0 ";
            return Excute(sql, userEntity, transaction);
        }

    }
}
