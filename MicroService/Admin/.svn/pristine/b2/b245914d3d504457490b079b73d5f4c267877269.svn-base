using System.IO;
using Feng.Config;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Feng.Admin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 默认配置
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true)
                .Build();

            WebHost.CreateDefaultBuilder(args)
                  .ConfigureAppConfiguration((hostingContext, _config) => {
                      _config
                      .AddJsonFile("appsettings.json", true, true)
                      .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                      .AddEnvironmentVariables();
                      var options = new FengConfigOptions();
                      _config.Build().GetSection("ConfigService").Bind(options);
                      _config.AddFengConfig(options);
                  })
                  .UseKestrel()
                  .UseContentRoot(Directory.GetCurrentDirectory())
                  .UseStartup<Startup>()
                  .UseUrls(config.GetValue<string>("urls"))
                  .Build()
                  .Run();
        }
        
    }
}
