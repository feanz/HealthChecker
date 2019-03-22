﻿using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace HealthChecker.Sql
{
	public class SqlServerHealthCheck : IHealthCheck
	{
		private readonly string _connectionString;
		private readonly string _sql;

		public SqlServerHealthCheck(string sqlServerConnectionString, string sql)
		{
			_connectionString = sqlServerConnectionString ?? throw new ArgumentNullException(nameof(sqlServerConnectionString));
			_sql = sql ?? throw new ArgumentNullException(nameof(sql));
		}

		public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
		{
			try
			{
				using (var connection = new SqlConnection(_connectionString))
				{
					await connection.OpenAsync(cancellationToken);

					using (var command = connection.CreateCommand())
					{
						command.CommandText = _sql;
						await command.ExecuteScalarAsync(cancellationToken);
					}

					return HealthCheckResult.Healthy();
				}
			}
			catch (Exception ex)
			{
				return new HealthCheckResult(context.Registration.FailureStatus, exception: ex);
			}
		}
	}
}
