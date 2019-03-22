using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using HealthChecker.Sql;

namespace HealthChecker.Sample
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			HealthCheck.AddHealthCheck()
				.AddCheck("Example", () => HealthCheckResult.Healthy("Example is OK!"))
				.AddSqlServer("Data Source=localhost;Initial Catalog=modis_Core;User ID=coreuser;Password=Test12345");


			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
		}
	}
}
