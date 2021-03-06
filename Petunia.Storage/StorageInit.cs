﻿using JacarandaX;
using System.Configuration;
using System.Reflection;

namespace Petunia.Storage
{
    public class StorageInit
    {
        public static void Init()
        {
            Database.AddConfig(ConfigurationManager.AppSettings["ConnectionString"], JacarandaX.DatabaseType.SqlServer);
            Database.Register(Assembly.GetExecutingAssembly());

            // Database.Create(false);
        }
    }
}
