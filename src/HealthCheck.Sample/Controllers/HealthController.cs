using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace HealthChecker.Sample.Controllers
{
	public class HealthController : ApiController
	{
		private readonly IHealthCheckService _healthCheckService;

		public HealthController() : this(new DefaultHealthCheckService())
		{}

		public HealthController(IHealthCheckService healthCheckService)
		{
			_healthCheckService = healthCheckService;
		}

		public HttpResponseMessage Get()
		{
			var report = _healthCheckService.CheckHealthAsync();

			return new HttpResponseMessage
			{
				Content = new StringContent(
					report.Result.Status.ToString(), 
					Encoding.UTF8, 
					"text/html"
				)
			};
		}

		//public HealthReport Report()
		//{
		//	var report = _healthCheckService.CheckHealthAsync();
		//	return report.Result;
		//}
	}
}
