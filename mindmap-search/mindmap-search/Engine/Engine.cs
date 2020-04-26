namespace mindmap_search.Engine
{
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.Extensions.Logging;
    using mindmap_search.FolderCrawl;
    using mindmap_search.MapSearch;
    using mindmap_search.Model;

    /// <summary>
    /// Main class of the application this should be called to access the maps and the logic.
    /// </summary>
    public class Engine : IEngine
    {
        private readonly ILogger logger;
        private readonly ISearchFiles searchFiles;
        private readonly IExtractInfoFromMaps extract;
        private readonly ISearchMaps searchMaps;

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class.
        /// </summary>
        /// <param name="logger">logger. </param>
        /// <param name="searchMaps"> class used for searching users phrase in extracted maps.</param>
        /// <param name="extract">class used for extracting info from maps.</param>
        /// <param name="searchFiles">class used for searching files for maps.</param>
        public Engine(ILogger<Engine> logger, ISearchMaps searchMaps, IExtractInfoFromMaps extract, ISearchFiles searchFiles)
        {
            this.logger = logger;
            this.searchMaps = searchMaps;
            this.extract = extract;
            this.searchFiles = searchFiles;
        }

        public void FindMaps(string path)
        {
            logger.LogInformation($"Searching for maps in {path}");
            var findAllMaps = searchFiles.FindAllMaps(path);
            var extartInfo = extract.ExtartInfo(findAllMaps);
            searchMaps.LoadMapsData(extartInfo);
        }

        public List<SearchResult> Search(string querry)
        {
            return searchMaps.FindNodesWithValue(querry);
        }
    }
}