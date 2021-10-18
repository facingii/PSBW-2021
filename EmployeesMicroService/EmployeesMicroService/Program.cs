using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace EmployeesMicroService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog ((context, config) =>
                {
                    config.WriteTo.Console ();
                    config.WriteTo.File ("Logs.txt", Serilog.Events.LogEventLevel.Information);
                    config.WriteTo.ApplicationInsights(new TelemetryClient ()
                    {
                        InstrumentationKey = "dc1330dc-129b-415f-a2bc-3322663a3162",
                    }, TelemetryConverter.Events);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
