﻿using Mathy.Planning;
using Petunia.Auth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            PathResolver = new PathResolver(ConfigurationManager.AppSettings["RootPath"]);
            UrlResolver = new UrlResolver(ConfigurationManager.AppSettings["RootUrl"]);

            PlanRepository = new PlanRepository();
            PlanRepository.Load(PathResolver.PlanRepositoryPath);


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
