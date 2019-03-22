using System;
using System.Collections.Generic;

namespace HealthChecker
{
	public struct HealthCheckResult
	{
		private static readonly IReadOnlyDictionary<string, object> EmptyReadOnlyDictionary = new Dictionary<string, object>();

		public HealthCheckResult(HealthStatus status, string description = null, Exception exception = null, IReadOnlyDictionary<string, object> data = null)
		{
			Status = status;
			Description = description;
			Exception = exception;
			Data = data ?? EmptyReadOnlyDictionary;
		}

		public IReadOnlyDictionary<string, object> Data { get; }


		public string Description { get; }

		public Exception Exception { get; }

		public HealthStatus Status { get; }

		public static HealthCheckResult Healthy(string description = null, IReadOnlyDictionary<string, object> data = null)
		{
			return new HealthCheckResult(HealthStatus.Healthy, description, null, data);
		}

		public static HealthCheckResult Degraded(string description = null, Exception exception = null, IReadOnlyDictionary<string, object> data = null)
		{
			return new HealthCheckResult(HealthStatus.Degraded, description, null, data);
		}

		public static HealthCheckResult Unhealthy(string description = null, Exception exception = null, IReadOnlyDictionary<string, object> data = null)
		{
			return new HealthCheckResult(HealthStatus.Unhealthy, description, exception, data);
		}
	}
}