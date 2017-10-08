using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TeduShop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
            name: "Log Out",
            url: "dang-xuat",
            defaults: new { controller = "Acount", action = "LogOut", id = UrlParameter.Optional }
        );

            routes.MapRoute(
            name: "UserDetail",
            url: "thong-tin-user",
            defaults: new { controller = "Acount", action = "UserDetail", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "Dang Ky",
            url: "dang-ky",
            defaults: new { controller = "Acount", action = "Register", id = UrlParameter.Optional }
        );
            routes.MapRoute(
             name: "Contact",
             url: "lien-he",
             defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional }
         );
            routes.MapRoute(
            name: "Login",
            url: "dang-nhap",
            defaults: new { controller = "Acount", action = "Login", id = UrlParameter.Optional }
        );
            routes.MapRoute(
                name: "Cart",
                url: "gio-hang.html",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Products",
                url: "{alias}.pc-{id}.html",
                defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ProductDetail",
                url: "{alias}.p-{id}.html",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
