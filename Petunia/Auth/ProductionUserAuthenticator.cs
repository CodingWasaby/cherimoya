using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petunia.Auth
{
    class ProductionUserAuthenticator : UserAuthenticator
    {
        public override AuthUser GetAuthUser(string id, string password)
        {
            return QueryFirstOrDefault<AuthUser>(@" SELECT  [UserID] ,
                                                            [ID] ,
                                                            [Name] ,
                                                            [Password] ,
                                                            [Email] ,
                                                            [CreateTime]
                                                    FROM    [TwilightPlain].[dbo].[UserDB]
                                                    WHERE   ID = @ID
                                                            AND Password = @Password ", new { ID = id, Password = password });
        }
    }
}


