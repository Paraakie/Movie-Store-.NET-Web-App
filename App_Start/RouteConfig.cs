using System.Web.Mvc;
using System.Web.Routing;

namespace Tuto4
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //For create to work properly
            routes.MapRoute(
                name: "ProductsCreate",
                url: "Products/Create",
                defaults: new { controller = "Products", action = "Create" }
                );

            //route for paging on category page
            routes.MapRoute(
              name: "ProductsbyCategoryByPage",
              url: "Products/{category}/Page{page}",
              defaults: new { controller = "Products", action = "Index" }
              );

            // route for paging on products page
            routes.MapRoute(
               name: "ProductsbyPage",
               url: "Products/Page{page}",
               defaults: new { controller = "Products", action = "Index" }
            );

            routes.MapRoute(
                name: "ProductsByCategory",
                url: "Products/{category}",
                defaults: new { controller = "Products", action = "Index" }
            );

            routes.MapRoute(
                name: "ProductsIndex",
                url: "Products",
                defaults: new { controller = "Products", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
