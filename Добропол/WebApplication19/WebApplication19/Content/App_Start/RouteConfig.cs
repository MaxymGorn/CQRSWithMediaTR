using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace Qmate.Preliminary.Registration.General
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;

            routes.MapPageRoute("default", "", "~/Preliminary.aspx");

            routes.EnableFriendlyUrls(settings);
        }
    }
}
