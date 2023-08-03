using System.Diagnostics;

namespace UItests.Utils
{
    public static class Wait
    {
        private const int PollingInterval = 200;
        private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(5);

        public static bool For(Func<bool> predicate, TimeSpan? timeout = null)
        {
            timeout ??= DefaultTimeout;

            var stopwatch = Stopwatch.StartNew();

            do
            {
                try
                {
                    if (predicate != null && predicate())
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    // Ignore all exceptions and keep querying data.
                }

                Thread.Sleep(PollingInterval);
            }
            while (stopwatch.Elapsed < timeout);

            return false;
        }
    }
}
