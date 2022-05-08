using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvMaze.Scraper.Helpers.Polly
{
    using global::Polly;
    using global::Polly.Extensions.Http;

    /// <summary>
    /// Helpers methods for Polly policies.
    /// </summary>
    public static class PollyPolicies
    {
        /// <summary>
        /// Configure and get a Polly retry policy.
        /// </summary>
        /// <param name="retryCount">The number of times the clinet must retry sending the message.</param>
        /// <param name="sleepDuration">Defines the exponential retry, starting at <paramref name="sleepDuration"/> seconds.</param>
        /// <returns>A Polly retry policy instance.</returns>
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(int retryCount, int sleepDuration)
        {
            Random jitterer = new();
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(retryCount, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(sleepDuration, retryAttempt))
                    + TimeSpan.FromMilliseconds(jitterer.Next(0, 1000)));
        }
    }
}
