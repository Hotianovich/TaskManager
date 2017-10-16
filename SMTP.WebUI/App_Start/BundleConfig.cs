using System.Web;
using System.Web.Optimization;

namespace SMTP.WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/modernizr-*",
                        "~/Scripts/bootstrap.js",                   
                        "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/myscripts").Include(
                        "~/Scripts/SiteScripts.js"));

            bundles.Add(new ScriptBundle("~/bundles/mynextscripts").Include(
                       "~/Scripts/MyScripts.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/bootstrap.css",
                     "~/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.validate.unobtrusive.js"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                       "~/Content/themes/base/all.css"));


        }
    }
}
