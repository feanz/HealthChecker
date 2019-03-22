using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace HealthChecker.Tests
{
	public class DefaultHealthCheckServiceTests
	{
		public DefaultHealthCheckServiceTests()
		{
			HealthCheckRegistry.Instance.Registrations.Clear();
		}

		[Fact]
		public async Task CheckHealth_healthy_test_returns_healthy()
		{
			HealthCheck.AddHealthCheck()
				.AddCheck("Example", () => HealthCheckResult.Healthy("Example is OK!"));

			var sut = new DefaultHealthCheckService();

			var report = await sut.CheckHealthAsync();

			report.Status.Should().Be(HealthStatus.Healthy);
		}

		[Fact]
		public async Task CheckHealth_unhealthy_checks_return_unhealthy()
		{
			HealthCheck.AddHealthCheck()
				.AddCheck("Example", () => HealthCheckResult.Unhealthy("Not good!"));

			var sut = new DefaultHealthCheckService();

			var report = await sut.CheckHealthAsync();
			
			report.Status.Should().Be(HealthStatus.Unhealthy);
		}

		[Fact]
		public async Task CheckHealth_two_healthy_check_returns_healthy()
		{
			HealthCheck.AddHealthCheck()
				.AddCheck("Example", () => HealthCheckResult.Healthy("Example is OK!"))
				.AddCheck("Example 2", () => HealthCheckResult.Healthy("Example 2 is OK!"));

			var sut = new DefaultHealthCheckService();

			var report = await sut.CheckHealthAsync();
			
			report.Status.Should().Be(HealthStatus.Healthy);
		}

		[Fact]
		public async Task CheckHealth_one_check_returns_one_entry()
		{
			HealthCheck.AddHealthCheck()
				.AddCheck("Example", () => HealthCheckResult.Healthy("Example is OK!"));

			var sut = new DefaultHealthCheckService();

			var report = await sut.CheckHealthAsync();
			
			report.Entries.Count.Should().Be(1);
		}

		[Fact]
		public async Task CheckHealth_two_check_returns_two_entries()
		{
			HealthCheck.AddHealthCheck()
				.AddCheck("Example", () => HealthCheckResult.Healthy("Example is OK!"))
				.AddCheck("Example 2", () => HealthCheckResult.Healthy("Example 2 is OK!"));

			var sut = new DefaultHealthCheckService();

			var report = await sut.CheckHealthAsync();
			
			report.Entries.Count.Should().Be(2);
		}
		

		[Fact]
		public async Task CheckHealth_any_unhealthly_check_returns_one_unhealthy()
		{
			HealthCheck.AddHealthCheck()
				.AddCheck("Example", () => HealthCheckResult.Healthy("Example is OK!"))
				.AddCheck("Example 2", () => HealthCheckResult.Unhealthy("This one should make it fail"));

			var sut = new DefaultHealthCheckService();

			var report = await sut.CheckHealthAsync();
			
			report.Status.Should().Be(HealthStatus.Unhealthy);
		}
	}
}