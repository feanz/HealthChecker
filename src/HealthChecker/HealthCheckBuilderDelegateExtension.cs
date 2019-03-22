using System;
using System.Collections.Generic;

namespace HealthChecker
{
	public static class HealthCheckBuilderDelegateExtension
	{
		public static IHealthChecksBuilder AddCheck(this IHealthChecksBuilder builder, string name, Func<HealthCheckResult> check, IEnumerable<string> tags = null)
		{
			if (builder == null)
				throw new ArgumentNullException(nameof(builder));
			if (name == null)
				throw new ArgumentNullException(nameof(name));
			if (check == null)
				throw new ArgumentNullException(nameof(check));

			var delegateHealthCheck = new DelegateHealthCheck(check);

			return builder.Add(new HealthCheckRegistration(name, () => delegateHealthCheck, new HealthStatus?(), tags));
		}
	}
}