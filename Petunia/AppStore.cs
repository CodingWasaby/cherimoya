using Mathy.Planning;
using Petunia.Auth;
using System.Configuration;

namespace Petunia
{
    class AppStore
    {
        public static PlanRepository PlanRepository { get; private set; }

        public static PathResolver PathResolver { get; private set; }

        public static UrlResolver UrlResolver { get; private set; }

        public static UserAuthenticator UserAuthenticator { get; private set; }

        public static FuncDoc[] Docs { get; set; }

        public static void Init()
        {
            PathResolver = new PathResolver(System.AppDomain.CurrentDomain.BaseDirectory);
            UrlResolver = new UrlResolver(ConfigurationManager.AppSettings["RootUrl"]);

            PlanRepository = new PlanRepository();

            if (ConfigurationManager.AppSettings["MockUserAuth"] == "true")
            {
                UserAuthenticator = new MockUserAuthenticator();
            }
            else
            {
                UserAuthenticator = new ProductionUserAuthenticator();
            }
        }
    }
}
