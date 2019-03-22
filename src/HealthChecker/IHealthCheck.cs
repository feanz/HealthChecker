using System.Threading;
using System.Threading.Tasks;

namespace HealthChecker
{
	public interface IHealthCheck
	{
		/// <summary>
		/// Runs the health check, returning the status of the component being checked.
		/// </summary>
		Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken);
	}
}