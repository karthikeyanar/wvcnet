using System.Web;
using System.Web.Optimization;

namespace WVC.Views {
	public class BundleConfig {
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles) {
			bundles.Add(new ScriptBundle("~/js").Include(
						"~/js/jquery/jquery.2.1.1.min.js",
						"~/js/bootstrap/bootstrap.min.js",
						"~/js/knockout/knockout-2.3.0.min.js",
						"~/js/require/require.2.1.8.js"
						));
			bundles.Add(new StyleBundle("~/css").Include(
					  "~/bootstrap.css"));
		}
	}
}
