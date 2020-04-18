using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using mindmap_search.Engine;
using mindmap_search.FileTypes;
using mindmap_search.FolderCrawl;
using mindmap_search.Model;

namespace mindmap_search.cli
{
    public class SearchCLI
    {
        private readonly  ILogger _logger;
        private readonly IEngine  _engine;
        //private readonly string path = @"D:\\git\\mindmap-serach-cli\\testdata";
        private readonly string path = @"D:\\git\\mindmap-serach-cli\\mindmaps-testdata";
        private readonly Defaults _defaults;
        
        public SearchCLI(ILogger<SearchCLI> logger, IEngine engine, IConfiguration configuration)
        {
            _logger = logger;
            _engine = engine;
            path= configuration["Defaults:DefaultPath"];
        }
        
        internal async Task Run()
        {
            _logger.LogInformation("Application {applicationEvent} at {dateTime}", "Started", DateTime.UtcNow);
            _engine.FindMaps(path);
            //bool running = true;
            WelcomeMessage();
            while (true)
            {
                PromptUser();
                string searchQuery = GetUserQuery(); 
                var searchResults = _engine.Search(searchQuery);
                TempPrint(searchResults);
            }
        }

        private string GetUserQuery()
        {
            return Console.ReadLine();
        }

        private void WelcomeMessage()
        {
            StringBuilder messege = new StringBuilder();
            messege.Append("Welcome To Mind Map Searcher")
                .Append("By Maciej Wyrodek (TheBrokenTest.com)")
                .AppendLine()
                .Append("For now only xmind is supported - type your querry you will get info which map")
                .Append("ctrl+c for closing app")
                .AppendLine();
        }

        private void PromptUser()
        {
            Console.WriteLine("Type search Querry");
        }

        private void TempPrint(List<SearchResult> searchResults)
        {
            Console.WriteLine("---------------------------------------------");
            foreach (var result in searchResults)
            {
                Console.WriteLine($"file {result.FileName,50} | conntent {result.Result,-100}");
            }
            Console.WriteLine("---------------------------------------------");
        }
        
    }
}