using System.Web;
using System.Web.Optimization;

namespace BookBlogger.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new ScriptBundle("~/Content/js").Include(
                      "~/Content/Site.js"
                ));
            //bundles.Add(new ScriptBundle("~/Content/kendo/js").Include("~/Content/kendo/js/jquery.min.js",/* "~/Content/kendo/js/angular.min.js",*/ "~/Content/kendo/js/jszip.min.js", "~/Content/kendo/js/kendo.all.min.js","~/Content/kendo/js/kendo.aspnetmvc.min.js"));
            ////bundles.Add(new ScriptBundle("~/Kendo").Include("~/Kendo/js/kendo.all.min.js",
            ////    "~/Kendo/js/kendo.aspnetmvc.min.js"));
            //bundles.Add(new StyleBundle("~/Content/kendo/2022.3.913").Include("~/Content/kendo/styles/kendo.common.min.css",
            //      "~/Content/kendo/styles/kendo.default.min.css"));
            //bundles.IgnoreList.Clear();
        }
    }
}
