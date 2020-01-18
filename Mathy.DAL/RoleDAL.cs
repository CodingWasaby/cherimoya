using Mathy.Model.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Mathy.DAL
{
    public class RoleDAL : BaseDAL
    {
        public List<UserRoleEntity> GetUserRoles()
        {
            string sql = @" SELECT * FROM dbo.UserRoleDB";
            return Query<UserRoleEntity>(sql).ToList();
        }

        public bool InserUserRole(UserRoleEntity userRole)
        {
            string sql = @" INSERT INTO dbo.UserRoleDB
                            ( UserID ,
                              RoleID ,
                              CreateTime ,
                              DeleteFlag
                            )
                    VALUES  ( @UserID , -- UserID - int
                              @RoleID , -- RoleID - int
                              GETDATE() , -- CreateTime - datetime
                              0  -- DeleteFlag - int
                            ) ";
            return Excute(sql, userRole);
        }

        public bool UpdateUserRole(UserRoleEntity userRole)
        {
            string sql = @" UPDATE  dbo.UserRoleDB
                            SET     RoleID = @RoleID
                            WHERE   UserID = @UserID ";
            return Excute(sql, userRole);
        }
    }
}
