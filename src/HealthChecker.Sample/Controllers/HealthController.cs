using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
			var report = AsyncHelper.RunSync(() => _healthCheckService.CheckHealthAsync());

			var status = report.Status.ToString();
			return new HttpResponseMessage
			{
				Content = new StringContent(
					status, 
					Encoding.UTF8, 
					"text/html"
				)
			};
		}

		[Route("api/health/report")]
		[HttpGet]
		public HealthReport Report()
		{
			var report = AsyncHelper.RunSync(() => _healthCheckService.CheckHealthAsync());
			
			return report;
		}
	}

	internal static class AsyncHelper
	{
		private static readonly TaskFactory MyTaskFactory = new 
			TaskFactory(CancellationToken.None, 
				TaskCreationOptions.None, 
				TaskContinuationOptions.None, 
				TaskScheduler.Default);

		public static TResult RunSync<TResult>(Func<Task<TResult>> func)
		{
			return AsyncHelper.MyTaskFactory
				.StartNew<Task<TResult>>(func)
				.Unwrap<TResult>()
				.GetAwaiter()
				.GetResult();
		}

		public static void RunSync(Func<Task> func)
		{
			AsyncHelper.MyTaskFactory
				.StartNew<Task>(func)
				.Unwrap()
				.GetAwaiter()
				.GetResult();
		}
	}
}
