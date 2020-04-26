using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using mindmap_search.Engine;
using mindmap_search.FileTypes;
using mindmap_search.FolderCrawl;
using mindmap_search.MapSearch;

namespace mindmap_search.cli
{
    class Program
    {
        public IConfigurationRoot Configuration { get; set; }
        
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Starting");


            var host = CreateHostBuilder(args).Build();
            using (var serviceScope = host.Services.CreateScope())
            {

                var services = serviceScope.ServiceProvider;

                try
                {
                    var myService = services.GetRequiredService<SearchCli>();
                    await myService.Run();

                    Console.WriteLine("Success");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Occured");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<SearchCli>();
                    services.AddScoped<IEngine, Engine.Engine>();
                    services.AddScoped<ISearchFiles, SearchFiles>();
                    services.AddScoped<ISearchMaps, SearchMaps>();
                    services.AddScoped<IExctrectFromArchive, ExtractFromZipArchive>();
                    services.AddScoped<IMindMapType, XMindType>();
                    services.AddScoped<IExtractInfoFromMaps, ExtractInfoFromMaps>();
                });
    }
}