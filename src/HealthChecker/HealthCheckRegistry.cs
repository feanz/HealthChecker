using System.Collections.Generic;

namespace HealthChecker
{
	public sealed class HealthCheckRegistry
	{
		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static HealthCheckRegistry()
		{
		}

		private HealthCheckRegistry()
		{
			Registrations = new List<HealthCheckRegistration>();
		}

		public static HealthCheckRegistry Instance { get; } = new HealthCheckRegistry();

		public IList<HealthCheckRegistration> Registrations { get; }
	}
}