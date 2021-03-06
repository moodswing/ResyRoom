﻿using System.Web;
using System.Web.Optimization;

namespace ResyRoom.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Content/Scripts/jquery-1.*"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Content/Scripts/jquery-ui-1.9.2.custom.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery-mask").Include("~/Content/Scripts/jquery.mask.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Content/Scripts/jquery.unobtrusive*",
                        "~/Content/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Content/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/extensions").Include("~/Content/Scripts/Extensiones.js"));
            bundles.Add(new ScriptBundle("~/bundles/facebook").Include("~/Content/Scripts/facebook-sdk-v2.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockoutjs-with-mapping").Include("~/Content/Scripts/knockout*"));
            bundles.Add(new ScriptBundle("~/bundles/layout").Include("~/Content/Scripts/Views/layout.js"));

            bundles.Add(new ScriptBundle("~/bundles/gmap").Include("~/Content/Scripts/gmap3.js"));
            bundles.Add(new ScriptBundle("~/bundles/history").Include("~/Content/Scripts/jquery.history.js"));

            bundles.Add(new ScriptBundle("~/bundles/RegisterStudio").Include("~/Content/Scripts/Views/register-studio.js", 
                        "~/Content/Scripts/Views/set-data-testing.js"));

            bundles.Add(new ScriptBundle("~/bundles/extra-validations").Include("~/Content/Scripts/extra-validations.js"));

            bundles.Add(new ScriptBundle("~/bundles/time-entry").Include(
                        "~/Content/Scripts/jquery.plugin.js", 
                        "~/Content/Scripts/jquery.timeentry.js"));

            bundles.Add(new ScriptBundle("~/bundles/fullcalendar").Include(
                        "~/Content/Scripts/moment.min.js",
                        "~/Content/Scripts/FullCalendar/fullCalendar-configuration.js",
                        "~/Content/Scripts/fullcalendar.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css",
                "~/Content/Form.css",
                "~/Content/Control.css",
                "~/Content/Header.css"));

            bundles.Add(new StyleBundle("~/Content/css/loading").Include("~/Content/loading.css"));

            bundles.Add(new StyleBundle("~/Content/css/fullcalendar").Include("~/Content/fullcalendar.css"));

            bundles.Add(new StyleBundle("~/Content/css/jquery").Include(
                            "~/Content/smoothness/jquery-ui-1.9.2.custom.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}