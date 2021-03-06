﻿using JacarandaX;
using Petunia.LogicModel;
using Petunia.Storage.Models;
using System.Linq;

namespace Petunia.Storage
{
    public class UserStorage
    {
        public static void Save(UserLM lm)
        {
            UserDB db = UserDB.ToDB(lm);
            db.Save();

            lm.AutoID = db.AutoID;
        }

        public static UserLM Get(string id)
        {
            return UserDB.ToLM(Database.Table<UserDB>().First(i => i.ID == id));
        }
    }
}
