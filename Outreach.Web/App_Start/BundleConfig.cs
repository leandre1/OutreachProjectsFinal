﻿using System.Web;
using System.Web.Optimization;

namespace Outreach.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/DataTables/jquery.dataTables.js",
                        "~/Scripts/DataTables/dataTables.bootstrap.js",
                        "~/Scripts/jquery-ui.js",
                        "~/Scripts/jquery-ui.min.js",
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/toastr.js",
                        "~/Scripts/typeahead.bundle.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/DataTables/css/dataTables.bootstrap.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/jquery-ui.min.css",
                      "~/Content/jquery-ui.structure.css",
                      "~/Content/jquery-ui.structure.min.css",
                      "~/Content/jquery-ui.theme.css",
                      "~/Content/jquery-ui.theme.min.css",
                      "~/Content/toastr.css",
                      "~/Content/typeahead.css",
                      "~/Content/Site.css"));
        }
    }
}
