using System.Web.Optimization;

namespace Mvc4WebRole
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquerymobile").Include("~/Scripts/jquery.mobile-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                .Include("~/Scripts/jquery.unobtrusive*", "~/Scripts/jquery.validate*", "~/Scripts/jqueryFix.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/jquerymobile/css").Include("~/Content/jquery.mobile-{version}.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/Site.css"));
        }
    }
}