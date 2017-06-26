using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UM_LOGISTIC_V1.Routing;

namespace UM_LOGISTIC_V1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
			routes.Add("Default", new DefaultRoute());

        }
    }
}
