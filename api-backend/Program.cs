using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace api_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
           CreateHostBuilder(args).Build().Run();
            /*
            Console.WriteLine("dfxjgdk");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var hub = new Hub(53, 37, 53.2f, 37.1f);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);

            stopwatch.Reset();

            const int ITERATIONS = 10000;

            stopwatch.Start();

            for (var _ = 0; _ < ITERATIONS; _++)
                new Hub(53, 37, 53.2f, 37.1f).BuildRoute();

            stopwatch.Stop();
            Console.WriteLine((float)stopwatch.ElapsedMilliseconds / ITERATIONS);
            */
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
          return  Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                });
        }
    }
}
