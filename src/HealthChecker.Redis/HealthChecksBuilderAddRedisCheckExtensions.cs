using System.Collections.Generic;

namespace HealthChecker.Redis
{
	public static class HealthChecksBuilderAddRedisCheckExtensions
	{
		private const string Name = "redis";

		public static IHealthChecksBuilder AddRedis(this IHealthChecksBuilder builder, 
			string redisConnectionString, 
			string name = null, 
			HealthStatus? failureStatus = null, 
			IEnumerable<string> tags = null)
		{
			return builder.Add(new HealthCheckRegistration(
				name ?? Name,
				() => new RedisHealthCheck(redisConnectionString),
				failureStatus,
				tags));
		}
	}
}