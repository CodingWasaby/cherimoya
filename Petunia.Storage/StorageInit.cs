using JacarandaX;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
