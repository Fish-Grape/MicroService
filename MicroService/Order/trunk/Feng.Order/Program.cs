using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Feng.Config;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Feng.Order
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
