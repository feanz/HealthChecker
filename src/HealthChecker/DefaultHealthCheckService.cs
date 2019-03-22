using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HealthChecker
{
	public class DefaultHealthCheckService : IHealthCheckService
	{
		public DefaultHealthCheckService()
		{
			ValidateRegistrations(HealthCheckRegistry.Instance.Registrations);
		}

		public async Task<HealthReport> CheckHealthAsync(Func<HealthCheckRegistration, bool> predicate, CancellationToken cancellationToken = default(CancellationToken))
		{
			var registrations = HealthCheckRegistry.Instance.Registrations;
			var context = new HealthCheckContext();
			var entries = new Dictionary<string, HealthReportEntry>(StringComparer.OrdinalIgnoreCase);
			var totalTime = ValueStopwatch.StartNew();
			//todo add logging
			//todo run checks in parallel
			foreach (var registraion in registrations)
			{
				var registration = registraion;
				if (predicate == null || predicate(registration))
				{
					var healthCheck = registration.Factory();

					var stopwatch = ValueStopwatch.StartNew();
					context.Registration = registration;
					//todo add logging
					HealthReportEntry entry;
					try
					{
						var healthCheckResult = await healthCheck.CheckHealthAsync(context, cancellationToken);
						var elapsedTime = stopwatch.GetElapsedTime();
						entry = new HealthReportEntry(healthCheckResult.Status, healthCheckResult.Description, elapsedTime, healthCheckResult.Exception, healthCheckResult.Data);
						//todo add logging
					}
					catch (Exception ex) when (!(ex is OperationCanceledException))
					{
						var elapsedTime = stopwatch.GetElapsedTime();
						entry = new HealthReportEntry(HealthStatus.Unhealthy, ex.Message, elapsedTime, ex, null);
						//todo add logging
					}
					entries[registration.Name] = entry;
				}
			}
			var totalElapsedTime = totalTime.GetElapsedTime();
			var healthReport = new HealthReport(entries, totalElapsedTime);
			//todo add logging

			return healthReport;
		}

		//todo implement support for filtering healthchecks that are run
		public async Task<HealthReport> CheckHealthAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return await CheckHealthAsync(null, cancellationToken);
		}

		private static void ValidateRegistrations(IEnumerable<HealthCheckRegistration> registrations)
		{
			var list = registrations
				.GroupBy(c => c.Name, StringComparer.OrdinalIgnoreCase)
				.Where(g => g.Count() > 1)
				.Select(g => g.Key)
				.ToList();

			if (list.Count > 0)
				throw new ArgumentException("Duplicate health checks were registered with the name(s): " + string.Join(", ", list), nameof (registrations));
		}
	}
}