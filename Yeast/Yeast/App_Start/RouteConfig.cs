﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Yeast
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute("Sitemap", "sitemap", new { controller = "XML", action = "Sitemap" }, new[] { "Yeast.Controllers" });
			routes.MapRoute("RSS", "rss", new { controller = "XML", action = "RSS" }, new[] { "Yeast.Controllers" });

			routes.MapRoute(
                            name: "Account",
                            url: "Account/Login/{id}",
                            defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                            namespaces: new string[] { "Yeast.Controllers" }
                        );

            routes.MapRoute(
						    name: "Localization",
						    url: "{lang}/{controller}/{action}/{id}",
						    defaults: new { controller = "Home", action = "Index", lang = "fa", id = UrlParameter.Optional },
                            namespaces: new string[] { "Yeast.Controllers" }
                        );

            
        }
    }
}
