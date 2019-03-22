using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace HealthChecker.Tests
{
	public class HealthCheckTests
	{
		//todo add tests to cover:
		//logging
		//timeing
		//exception during healthchecks
		//predicate filtering
		//mapping of health check result to health report entry
		//validation of registrations
		public HealthCheckTests()
		{
			HealthCheckRegistry.Instance.Registrations.Clear();
		}

		[Fact]
		public void AddCheck_adds_registration_to_registry()
		{
			HealthCheck.AddHealthCheck()
				.AddCheck("Example", () => HealthCheckResult.Healthy());

			var actual = HealthCheckRegistry.Instance.Registrations.Count;

			actual.Should().Be(1);
		}

		[Fact]
		public void AddCheck_add_multiple_checks_to_registry()
		{
			HealthCheck.AddHealthCheck()
				.AddCheck("Example", () => HealthCheckResult.Healthy())
				.AddCheck("Example 2", () => HealthCheckResult.Healthy());

			var actual = HealthCheckRegistry.Instance.Registrations.Count;

			actual.Should().Be(2);
		}

		[Fact]
		public void AddCheckGeneric_adds_registration_to_registry()
		{
			HealthCheck.AddHealthCheck()
				.AddCheck<TestHealthyHealthCheck>("Example");

			var actual = HealthCheckRegistry.Instance.Registrations.Count;

			actual.Should().Be(1);
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