using System.Web.Mvc;
using System.Web.Routing;

namespace Mvc4WebRole
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Recipe",
                    action = "Index",
                    id = UrlParameter.Optional
                }
                );

            routes.MapRoute(
                name: "Logs",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Info",
                    action = "Logs",
                    id = UrlParameter.Optional
                }
                );

            routes.MapRoute(
                name: "Info",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Info",
                    action = "Index",
                    id = UrlParameter.Optional
                }
                );
        }
    }
}