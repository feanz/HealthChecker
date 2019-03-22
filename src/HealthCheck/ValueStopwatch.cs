using System;
using System.Diagnostics;

namespace HealthChecker
{
	internal struct ValueStopwatch
	{
		private static readonly double TimestampToTicks = 10000000.0 / Stopwatch.Frequency;
		private readonly long _startTimestamp;

		public bool IsActive => (ulong)_startTimestamp > 0UL;

		private ValueStopwatch(long startTimestamp)
		{
			_startTimestamp = startTimestamp;
		}

		public static ValueStopwatch StartNew()
		{
			return new ValueStopwatch(Stopwatch.GetTimestamp());
		}

		public TimeSpan GetElapsedTime()
		{
			if (!IsActive)
				throw new InvalidOperationException("An uninitialized, or 'default', ValueStopwatch cannot be used to get elapsed time.");
			var num = Stopwatch.GetTimestamp() - _startTimestamp;
			return new TimeSpan((long)(TimestampToTicks * num));
		}
	}
}