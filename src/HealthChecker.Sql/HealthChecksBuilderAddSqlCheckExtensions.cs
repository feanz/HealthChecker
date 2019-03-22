using System.Collections.Generic;

namespace HealthChecker.Sql
{
	public static class HealthChecksBuilderAddSqlCheckExtensions
	{
		const string Name = "sqlserver";

		public static IHealthChecksBuilder AddSqlServer(this IHealthChecksBuilder builder, 
			string connectionString, 
			string healthQuery = "SELECT 1;", 
			string name = null, 
			HealthStatus? failureStatus = null, 
			IEnumerable<string> tags = null)
		{
			return builder.Add(new HealthCheckRegistration(
				name ?? Name,
				() => new SqlServerHealthCheck(connectionString, healthQuery),
				failureStatus,
				tags));
		}
	}
}