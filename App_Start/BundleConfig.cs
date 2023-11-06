using System.Web;
using System.Web.Optimization;

namespace mYSelfERPWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            // Bundle for CSS files
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/dist/css/style.min.css",
                "~/dist/libs/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css",
                "~/dist/libs/bootstrap-material-datetimepicker/css/bootstrap-material-datetimepicker.css",
                "~/dist/libs/select2/dist/css/select2.min.css"
              ));


            // Bundle for JavaScript files
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/dist/libs/jquery/dist/jquery.min.js",
                "~/dist/libs/popper.js/dist/umd/popper.min.js",
                "~/dist/libs/bootstrap/dist/js/bootstrap.bundle.min.js",
                "~/dist/js/app.min.js",
                "~/dist/js/app.init.mini-sidebar.js",
                "~/dist/js/app-style-switcher.js",
                "~/dist/libs/perfect-scrollbar/dist/js/perfect-scrollbar.jquery.js",
                "~/dist/libs/jquery-sparkline/jquery.sparkline.min.js",
                "~/dist/js/waves.js",
                "~/dist/js/sidebarmenu.js",
                "~/dist/js/feather.min.js",
                "~/dist/js/custom.min.js",
                "~/dist/js/plugins/toastr-init.js",
                "~/assets/libs/moment/moment.js",
                "~/dist/libs/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js",
                "~/dist/libs/bootstrap-material-datetimepicker/node_modules/moment/moment.js",
                "~/dist/libs/bootstrap-material-datetimepicker/js/bootstrap-material-datetimepicker.js",
                "~/dist/libs/select2/dist/js/select2.full.min.js",
                "~/dist/libs/select2/dist/js/select2.min.js",
                "~/dist/js/pages/forms/select2/select2.init.js",
                "~/dist/libs/apexcharts/dist/apexcharts.min.js",
                "~/dist/js/pages/apex-chart/apex.area.init.js",
                "~/dist/js/pages/apex-chart/apex.bar.init.js",
                "~/dist/js/pages/apex-chart/apex.line.init.js",
                "~/dist/js/pages/apex-chart/apex.pie.init.js"

            ));


        }
    }
}
