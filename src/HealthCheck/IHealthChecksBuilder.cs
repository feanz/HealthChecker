namespace HealthChecker
{
	public interface IHealthChecksBuilder
	{
		IHealthChecksBuilder Add(HealthCheckRegistration registration);
	}
}