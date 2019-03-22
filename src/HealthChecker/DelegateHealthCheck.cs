using System;
using System.Threading;
using System.Threading.Tasks;

namespace HealthChecker
{
	internal sealed class DelegateHealthCheck : IHealthCheck
	{
		private readonly Func<CancellationToken, Task<HealthCheckResult>> _check;
		
		public DelegateHealthCheck(Func<CancellationToken, Task<HealthCheckResult>> check)
		{
			_check = check ?? throw new ArgumentNullException(nameof(check));
		}
		
		public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken)) => _check(cancellationToken);
	}
}