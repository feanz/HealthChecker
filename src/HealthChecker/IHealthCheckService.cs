using System;
using System.Threading;
using System.Threading.Tasks;

namespace HealthChecker
{
	public interface IHealthCheckService
	{
		Task<HealthReport> CheckHealthAsync(CancellationToken cancellationToken = default(CancellationToken));
		Task<HealthReport> CheckHealthAsync(Func<HealthCheckRegistration, bool> predicate, CancellationToken cancellationToken = default(CancellationToken));
	}
}