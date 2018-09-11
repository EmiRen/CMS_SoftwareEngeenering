using System.Web;
using System.Web.Optimization;

namespace CMS4
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jsadds").Include(
                    "~/Scripts/jquery.dataTables.js",
                    "~/Scripts/bootstrap-datepicker.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/css/jquery.dataTables.css",
                      "~/Content/datatables.extensions/AutoFill/css/dataTables.autoFill.css",
                      "~/Content/datatables.extensions/AutoFill/css/dataTables.responsive.css",
                      "~/Content/datatables.extensions/AutoFill/css/dataTables.tableTools.css",
                      "~/Content/bootstrap-datepicker3.css",
                      "~/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/bundles/myscript").Include(
                      "~/Scripts/myScript.js"
              ));
        }
    }
}
