using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjectFilm_CNPM
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //khai bao cho URL dong
            routes.MapRoute(
            name: "dangnhap",
            url: "dang-nhap",
            defaults: new { controller = "User", action = "DangNhap" }
        );
            routes.MapRoute(
            name: "dangky",
            url: "dang-ky",
            defaults: new { controller = "User", action = "DangKy" }
        );
            routes.MapRoute(
              name: "Siteslug",
              url: "{slug}",
              defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Site", action = "Home", id = UrlParameter.Optional }
            );
            


        }
    }
}
