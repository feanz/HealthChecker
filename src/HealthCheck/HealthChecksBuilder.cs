using System;
using System.Collections.Generic;

namespace HealthChecker
{
	public class HealthChecksBuilder : IHealthChecksBuilder
	{
		private readonly IList<HealthCheckRegistration> _checks;

		public HealthChecksBuilder(IList<HealthCheckRegistration> checks)
		{
			_checks = checks;
		}

		public IHealthChecksBuilder Add(HealthCheckRegistration registration)
		{
			if (registration == null)
				throw new ArgumentNullException(nameof(registration));

			_checks.Add(registration);

			return this;
		}
	}
}