﻿using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Mvc;
using HealthChecker.Sql;
using Newtonsoft.Json.Serialization;

namespace HealthChecker.Sample
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			HealthCheck.AddHealthCheck()
				.AddCheck("Example", () => HealthCheckResult.Healthy("Example is OK!"))
				.AddSqlServer(name: "Core Database",
					connectionString: "Data Source=localhost;Initial Catalog=modis_Core;User ID=coreuser;Password=Test12345");

			GlobalConfiguration.Configuration.Formatters.Clear();
			GlobalConfiguration.Configuration.Formatters.Add(new JsonMediaTypeFormatter());

			var jsonFormatter = GlobalConfiguration.Configuration.Formatters.OfType<JsonMediaTypeFormatter>().First();
			jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			jsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
			jsonFormatter.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
		}
	}
}
