﻿using System.Web;
using System.Web.Optimization;

namespace Sistema_clinica
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/CustomValidacoes.js"));

            bundles.Add(new ScriptBundle("~/bundles/filtros").Include(
                        "~/Scripts/filtros.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerymask").Include(
            "~/Scripts/jquery.mask.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/mascaras").Include(
            "~/Scripts/mascaras.js"));

            bundles.Add(new ScriptBundle("~/bundles/cep").Include(
            "~/Scripts/cep.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
