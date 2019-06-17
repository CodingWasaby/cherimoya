using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petunia.Auth
{
    abstract class UserAuthenticator : SqlBase
    {
        public abstract AuthUser GetAuthUser(string id, string password);
    }
}
