using JacarandaX;
using Petunia.LogicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petunia.Storage.Models
{
    [Table]
    class UserDB : Entity
    {
        [Field]
        [DbPrimaryKey]
        public int AutoID { get; set; }

        [Field]
        public string ID { get; set; }

        [Field]
        public string Name { get; set; }


        public static UserLM ToLM(UserDB db)
        {
            return db == null ? null : new UserLM()
            {
                AutoID = db.AutoID,
                ID = db.ID,
                Name = db.Name
            };
        }

        public static UserDB ToDB(UserLM lm)
        {
            return new UserDB()
            {
                AutoID = lm.AutoID,
                ID = lm.ID,
                Name = lm.Name
            };
        }
    }
}
