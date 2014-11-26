using System;
using System.Data.Entity;
using System.Globalization;
using System.Threading;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Mvc4WebRole.Filters;
using Mvc4WebRole.Migrations;


namespace Mvc4WebRole
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            SessionLogger.AddLogInit("Application_Start");

            var germanCulture = new CultureInfo("de-DE");
            Thread.CurrentThread.CurrentCulture = germanCulture;
            Thread.CurrentThread.CurrentUICulture = germanCulture;


            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RecipeDbContext, Configuration>());

            AreaRegistration.RegisterAllAreas();

            ModelBinders.Binders.Add(typeof(Single), new SingleMultiCultureModelBinder());

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

         //   BundleMobileConfig.RegisterBundles(BundleTable.Bundles);

            SercurityInit.Init();

            SessionLogger.AddLogFinished("Application_Start");
        }
    }
}