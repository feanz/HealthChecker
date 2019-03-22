using System;
using System.Collections.Generic;

namespace HealthChecker
{
	public sealed class HealthReport
	{
		public HealthReport(IReadOnlyDictionary<string, HealthReportEntry> entries, TimeSpan totalDuration)
		{
			Entries = entries;
			Status = CalculateAggregateStatus(entries.Values);
			TotalDuration = totalDuration;
		}

		public IReadOnlyDictionary<string, HealthReportEntry> Entries { get; }

		public HealthStatus Status { get; }

		public TimeSpan TotalDuration { get; }

		private static HealthStatus CalculateAggregateStatus(IEnumerable<HealthReportEntry> entries)
		{
			var healthStatus = HealthStatus.Healthy;
			foreach (var entry in entries)
			{
				if (healthStatus > entry.Status)
					healthStatus = entry.Status;
				if (healthStatus == HealthStatus.Unhealthy)
					return healthStatus;
			}
			return healthStatus;
		}
	}
}