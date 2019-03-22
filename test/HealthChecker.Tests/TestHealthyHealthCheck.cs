using System.Threading;
using System.Threading.Tasks;

namespace HealthChecker.Tests
{
	public class TestHealthyHealthCheck : IHealthCheck
	{
		public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken)
		{
			return Task.FromResult(HealthCheckResult.Healthy());
		}
	}
}