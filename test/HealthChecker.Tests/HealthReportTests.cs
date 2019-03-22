using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace HealthChecker.Tests
{
	public class HealthReportTests
	{
		[Fact]
		public void Status_when_single_entry_healthy_returns_healthy()
		{
			var sut = new HealthReport(new Dictionary<string, HealthReportEntry>
			{
				{"Example", new HealthReportEntry(HealthStatus.Healthy,"Description",TimeSpan.MaxValue, null, null)}
			}, TimeSpan.MaxValue);

			sut.Status.Should().Be(HealthStatus.Healthy);
		}

		[Fact]
		public void Status_when_all_entries_healthy_returns_healthy()
		{
			var sut = new HealthReport(new Dictionary<string, HealthReportEntry>
			{
				{"Healthy",   new HealthReportEntry(HealthStatus.Healthy,"Description",TimeSpan.MaxValue, null, null)},
				{"Healthy 2", new HealthReportEntry(HealthStatus.Healthy,"Description",TimeSpan.MaxValue, null, null)}
			}, TimeSpan.MaxValue);

			sut.Status.Should().Be(HealthStatus.Healthy);
		}

		[Fact]
		public void Status_when_one_entries_unhealthy_returns_unhealthy()
		{
			var sut = new HealthReport(new Dictionary<string, HealthReportEntry>
			{
				{"Healthy",   new HealthReportEntry(HealthStatus.Healthy,  "Description",TimeSpan.MaxValue, null, null)},
				{"Unhealthy", new HealthReportEntry(HealthStatus.Unhealthy,"Description",TimeSpan.MaxValue, null, null)}
			}, TimeSpan.MaxValue);

			sut.Status.Should().Be(HealthStatus.Unhealthy);
		}
	}
}