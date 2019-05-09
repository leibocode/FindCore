using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FindCore.GatewayAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                 .ConfigureAppConfiguration((conf,f)=> {
                     f.SetBasePath(conf.HostingEnvironment.ContentRootPath).AddJsonFile("Ocelot.json");
                 })
                 .UseUrls("http://localhost:4000")
                .UseStartup<Startup>();
    }
}
