namespace mindmap_search.cli
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using mindmap_search.Engine;
    using mindmap_search.Model;

    /// <summary>
    /// Console App wrapper for search engine
    /// </summary>
    public class SearchCli
    {
        private readonly ILogger logger;
        private readonly IEngine engine;
        private readonly string path;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCli"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="engine">engine.</param>
        /// <param name="configuration">configuration.</param>
        public SearchCli(ILogger<SearchCli> logger, IEngine engine, IConfiguration configuration)
        {
            this.logger = logger;
            this.engine = engine;
            this.path = configuration["Defaults:DefaultPath"];
        }

        internal async Task Run()
        {
            this.logger.LogInformation("Application {applicationEvent} at {dateTime}", "Started", DateTime.UtcNow);
            this.engine.FindMaps(this.path);
            this.WelcomeMessage();

            while (true)
            {
                this.PromptUser();
                string searchQuery = this.GetUserQuery();
                var searchResults = this.engine.Search(searchQuery);
                this.TempPrint(searchResults);
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