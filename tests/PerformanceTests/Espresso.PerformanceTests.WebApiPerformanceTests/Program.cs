using System;
using System.Collections.Generic;
using Espresso.PerformanceTests.WebApiPerformanceTests.Tests;

namespace Espresso.PerformanceTests.WebApiPerformanceTests
{
    internal class Program
    {
        private static void Main()
        {
            var numberOfArticlesList = new List<int> { 20 };
            var endpoint = "https://espressonews.co/api/articles/latest?take=";
            var numberOfMeasurements = 200;
            var numberOfRequestsPerMeasurement = 5;
            foreach (var numberOfArticles in numberOfArticlesList)
            {
                var (average, min, max) = ArticlesEndpointPerformanceTest.MeasureAverageMinAndMaxResponseTime(endpoint, numberOfMeasurements, numberOfArticles, numberOfRequestsPerMeasurement);

                Console.WriteLine("--------------------------------------------------------------------");
                Console.WriteLine($"Endpoint: {endpoint}{numberOfArticles}");
                Console.WriteLine($"Number of articles: {numberOfArticles}");
                Console.WriteLine($"Number of measurements: {numberOfMeasurements}");
                Console.WriteLine($"Number of requests per measurement: {numberOfRequestsPerMeasurement}");
                Console.WriteLine($"Average response time: {average}");
                Console.WriteLine($"Min response time: {min}");
                Console.WriteLine($"Max response time: {max}");
                Console.WriteLine("--------------------------------------------------------------------");
            }
        }
    }
}
