using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
//using System.Web.UI.WebControls;

namespace CJ.MerianPartyStore.PL.UI.Admin.App_Start
{
    public class WebApiConfig
    {
     
            public static void Register(HttpConfiguration config)
            {
                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id =  RouteParameter.Optional }
                );
            
        }
    }
}