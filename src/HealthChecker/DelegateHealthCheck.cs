using System;
using System.Threading;
using System.Threading.Tasks;

namespace HealthChecker
{
	internal sealed class DelegateHealthCheck : IHealthCheck
	{
		private readonly Func<HealthCheckResult> _check;

		public DelegateHealthCheck(Func<HealthCheckResult> check)
		{
			var func = check;
			_check = func ?? throw new ArgumentNullException(nameof(check));
		}

		public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken)
		{
			return Task.FromResult(_check());
		}
	}
}