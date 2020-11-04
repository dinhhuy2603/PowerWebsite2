using System.Web;
using System.Web.Optimization;

namespace PowerManagement
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/app.min.css",
                      "~/Content/css/icons.min.css",
                      "~/Content/css/common.css",
                      "~/Content/css/bootstrap-dark.min.css",
                      "~/Content/css/app-dark.min.css",
                      "~/Content/css/style.css",
                      "~/Content/libs/admin-resources/jquery.vectormap/jquery-jvectormap-1.2.2.css"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Content/js/vendor.min.js",
                "~/Content/libs/jquery-sparkline/jquery.sparkline.min.js",
                "~/Content/libs/admin-resources/jquery.vectormap/jquery-jvectormap-1.2.2.min.js",
                "~/Content/libs/admin-resources/jquery.vectormap/maps/jquery-jvectormap-world-mill-en.js",
                "~/Content/js/pages/dashboard-2.init.js",
                "~/Content/libs/apexcharts/apexcharts.min.js",
                "~/Content/js/app.min.js"));
        }
    }
}
