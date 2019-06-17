using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.Model.Entity
{
    public class UserRoleEntity
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public int DeleteFlag { get; set; }
    }
}
