using System.Web.Mvc;
using System.Web.Routing;
using DoddleReport.Web;

namespace WebPortal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // DoddleReport can automatically determine which IReportWriter to use based on the file extension of the route.  
            //routes.MapReportingRoute();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
