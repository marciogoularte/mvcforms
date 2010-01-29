using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcExample
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Simple", action = "Index", id = "" }  // Parameter defaults
            );

        }

        public static void RegisterModelBinders(ModelBinderDictionary binders)
        {
            binders.DefaultBinder = new Binder.SmartModelBinder(new Binder.IFilteredModelBinder[] 
            {
                new Binder.FormFilteredModelBinder(),
            });
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
            RegisterModelBinders(ModelBinders.Binders);
        }
    }
}