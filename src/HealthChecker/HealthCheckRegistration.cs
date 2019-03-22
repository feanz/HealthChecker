using System;
using System.Collections.Generic;

namespace HealthChecker
{
	public class HealthCheckRegistration
	{
		private Func<IHealthCheck> _factory;
		private string _name;

		public HealthCheckRegistration(string name, 
			IHealthCheck instance, 
			HealthStatus? failureStatus, 
			IEnumerable<string> tags = null)
		{
			if (instance == null)
			{
				throw new ArgumentNullException(nameof(instance));
			}

			Name = name ?? throw new ArgumentNullException(nameof(name));
			FailureStatus = failureStatus ?? HealthStatus.Unhealthy;
			Tags = new HashSet<string>(tags ?? Array.Empty<string>(), StringComparer.OrdinalIgnoreCase);
			Factory = () => instance;
		}

		public HealthCheckRegistration(string name,
			Func<IHealthCheck> factory,
			HealthStatus? failureStatus,
			IEnumerable<string> tags = null)
		{
			if (factory == null)
				throw new ArgumentNullException(nameof (factory));

			Name = name ?? throw new ArgumentNullException(nameof(name));

			var nullable = failureStatus;
			FailureStatus = nullable ?? HealthStatus.Unhealthy;
			Tags = new HashSet<string>(tags ?? Array.Empty<string>(), StringComparer.OrdinalIgnoreCase);
			Factory = factory;
		}

		public HealthStatus FailureStatus { get; set; }

		public Func<IHealthCheck> Factory
		{
			get => _factory;
			set => _factory = value ?? throw new ArgumentNullException(nameof(value));
		}

		public string Name
		{
			get => _name;
			set => _name = value ?? throw new ArgumentNullException(nameof(value));
		}

		/// <summary>
		/// Gets a list of tags that can be used for filtering health checks.
		/// </summary>
		public ISet<string> Tags { get; }
	}
}