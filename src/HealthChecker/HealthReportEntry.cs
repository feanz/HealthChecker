using System;
using System.Collections.Generic;

namespace HealthChecker
{
	public struct HealthReportEntry
	{
		private static readonly IReadOnlyDictionary<string, object> EmptyReadOnlyDictionary = new Dictionary<string, object>();

		public HealthReportEntry(HealthStatus status, string description, TimeSpan duration, Exception exception, IReadOnlyDictionary<string, object> data)
		{
			Status = status;
			Description = description;
			Duration = duration;
			Exception = exception;
			Data = data ?? EmptyReadOnlyDictionary;
		}


		public IReadOnlyDictionary<string, object> Data { get; }

		public string Description { get; }

		public TimeSpan Duration { get; }

		public Exception Exception { get; }

		public HealthStatus Status { get; }
	}
}