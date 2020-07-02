using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Espresso.PerformanceTests.WebApiPerformanceTests.Tests
{
    public static class ArticlesEndpointPerformanceTest
    {
        public static (TimeSpan average, TimeSpan min, TimeSpan max) MeasureAverageMinAndMaxResponseTime(string endpoint, int numberOfMeasurements, int numberOfArticles, int numberOfRequestsPerMeasurement)
        {
            try
            {

                using var httpClient = new HttpClient();
                // httpClient.DefaultRequestHeaders.Add(HttpHeaderConstants.HeaderName, HttpHeaderConstants.AndroidApiKeyValue);

                var durations = new ConcurrentQueue<TimeSpan>();

                for (var i = 0; i < numberOfMeasurements; i++)
                {
                    var stopwatch = Stopwatch.StartNew();
                    var tasks = new List<Task>();
                    for (var j = 0; j < numberOfRequestsPerMeasurement; j++)
                    {
                        tasks.Add(Task.Run(() =>
                        {
                            try
                            {
                                var stopwatch = Stopwatch.StartNew();
                                var result = httpClient.GetAsync($"{endpoint}{numberOfArticles}").GetAwaiter().GetResult();
                                var data = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                                durations.Enqueue(stopwatch.Elapsed);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Request Exception: {ex}");
                            }
                        }));
                    }
                    Task.WhenAll(tasks).GetAwaiter().GetResult();
                    var elapsed = stopwatch.Elapsed;
                    //Console.WriteLine($"{DateTime.UtcNow.ToUniversalTime()} - Run number {i} finished with duration: {elapsed}");
                }


                var average = durations.Aggregate(TimeSpan.FromSeconds(0), (aggregator, duration) => aggregator += duration) / durations.Count();
                var min = durations.Min();
                var max = durations.Max();

                return (average, min, max);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Method exception: {ex}");
                return (TimeSpan.FromTicks(0), TimeSpan.FromTicks(0), TimeSpan.FromTicks(0));
            }
        }
    }
}
