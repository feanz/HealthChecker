using System;
using System.Collections.Generic;

namespace HealthChecker
{
	public static class HealthChecksBuilderAddCheckExtensions
	{
		public static IHealthChecksBuilder AddCheck(this IHealthChecksBuilder builder, string name, IHealthCheck instance, HealthStatus? failureStatus = null, IEnumerable<string> tags = null)
		{
			if (builder == null)
				throw new ArgumentNullException(nameof(builder));
			if (name == null)
				throw new ArgumentNullException(nameof(name));
			if (instance == null)
				throw new ArgumentNullException(nameof(instance));

			return builder.Add(new HealthCheckRegistration(name, () => instance, failureStatus, tags));
		}

		public static IHealthChecksBuilder AddCheck<T>(this IHealthChecksBuilder builder, string name, HealthStatus? failureStatus = null, IEnumerable<string> tags = null) where T : class, IHealthCheck, new()
		{
			if (builder == null)
				throw new ArgumentNullException(nameof(builder));
			if (name == null)
				throw new ArgumentNullException(nameof(name));
			return builder.Add(new HealthCheckRegistration(name, () => new T(), failureStatus, tags));
		}
	}
}