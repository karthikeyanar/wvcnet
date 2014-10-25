using WVC.Framework;
using WVC.Models.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WVC.Api {
	public class WebApiApplication:System.Web.HttpApplication {
		protected void Application_Start() {
			JsonMediaTypeFormatter jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
			JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings() {
				Formatting = Formatting.Indented,
				DateTimeZoneHandling = DateTimeZoneHandling.Utc
			};
			jSettings.Converters.Add(new DateTimeConverter());
			jsonFormatter.SerializerSettings = jSettings;

			GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

			ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
			ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
			ModelBinders.Binders.Add(typeof(Int16), new Int16ModelBinder());
			ModelBinders.Binders.Add(typeof(Int16?), new Int16ModelBinder());
			ModelBinders.Binders.Add(typeof(Int32), new Int32ModelBinder());
			ModelBinders.Binders.Add(typeof(Int32?), new Int32ModelBinder());

			//AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			//BundleConfig.RegisterBundles(BundleTable.Bundles);
			BaseEntity.ConfigureServiceFactory(new ServiceFactory());
		}
	}
}
