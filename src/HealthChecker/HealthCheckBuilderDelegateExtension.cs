using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HealthChecker
{
	public static class HealthCheckBuilderDelegateExtension
	{
        public static IHealthChecksBuilder AddCheck(
            this IHealthChecksBuilder builder,
            string name,
            Func<HealthCheckResult> check,
            IEnumerable<string> tags = null)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (check == null)
            {
                throw new ArgumentNullException(nameof(check));
            }

            var instance = new DelegateHealthCheck(ct => Task.FromResult(check()));
            return builder.Add(new HealthCheckRegistration(name, instance, failureStatus: null, tags: tags));
        }

        /// <summary>
        /// Adds a new health check with the specified name and implementation.
        /// </summary>
        /// <param name="builder">The <see cref="IHealthChecksBuilder"/>.</param>
        /// <param name="name">The name of the health check.</param>
        /// <param name="tags">A list of tags that can be used to filter health checks.</param>
        /// <param name="check">A delegate that provides the health check implementation.</param>
        /// <returns>The <see cref="IHealthChecksBuilder"/>.</returns>
        public static IHealthChecksBuilder AddCheck(
            this IHealthChecksBuilder builder,
            string name,
            Func<CancellationToken, HealthCheckResult> check,
            IEnumerable<string> tags = null)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (check == null)
            {
                throw new ArgumentNullException(nameof(check));
            }

            var instance = new DelegateHealthCheck(ct => Task.FromResult(check(ct)));
            return builder.Add(new HealthCheckRegistration(name, instance, failureStatus: null, tags: tags));
        }

        /// <summary>
        /// Adds a new health check with the specified name and implementation.
        /// </summary>
        /// <param name="builder">The <see cref="IHealthChecksBuilder"/>.</param>
        /// <param name="name">The name of the health check.</param>
        /// <param name="tags">A list of tags that can be used to filter health checks.</param>
        /// <param name="check">A delegate that provides the health check implementation.</param>
        /// <returns>The <see cref="IHealthChecksBuilder"/>.</returns>
        public static IHealthChecksBuilder AddAsyncCheck(
            this IHealthChecksBuilder builder,
            string name,
            Func<Task<HealthCheckResult>> check,
            IEnumerable<string> tags = null)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (check == null)
            {
                throw new ArgumentNullException(nameof(check));
            }

            var instance = new DelegateHealthCheck(ct => check());
            return builder.Add(new HealthCheckRegistration(name, instance, failureStatus: null, tags: tags));
        }

        /// <summary>
        /// Adds a new health check with the specified name and implementation.
        /// </summary>
        /// <param name="builder">The <see cref="IHealthChecksBuilder"/>.</param>
        /// <param name="name">The name of the health check.</param>
        /// <param name="tags">A list of tags that can be used to filter health checks.</param>
        /// <param name="check">A delegate that provides the health check implementation.</param>
        /// <returns>The <see cref="IHealthChecksBuilder"/>.</returns>
        public static IHealthChecksBuilder AddAsyncCheck(
            this IHealthChecksBuilder builder,
            string name,
            Func<CancellationToken, Task<HealthCheckResult>> check,
            IEnumerable<string> tags = null)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (check == null)
            {
                throw new ArgumentNullException(nameof(check));
            }

            var instance = new DelegateHealthCheck(check);
            return builder.Add(new HealthCheckRegistration(name, instance, failureStatus: null, tags: tags));
        }
	}
}