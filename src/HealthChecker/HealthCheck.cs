namespace HealthChecker
{
	public class HealthCheck
	{
		public static IHealthChecksBuilder AddHealthCheck()
		{
			return new HealthChecksBuilder(HealthCheckRegistry.Instance.Registrations);
		}
	}
}