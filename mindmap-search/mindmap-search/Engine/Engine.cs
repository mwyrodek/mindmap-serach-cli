using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;
using mindmap_search.FolderCrawl;
using mindmap_search.MapSearch;
using mindmap_search.Model;

namespace mindmap_search.Engine
{
    public class Engine : IEngine
    {
        private readonly  ILogger _logger;
        private readonly ISearchFiles _searchFiles;
        private readonly IExtractInfoFromMaps _extract;
        private readonly ISearchMaps _searchMaps;
        public Engine(ILogger<Engine> logger, ISearchMaps searchMaps, IExtractInfoFromMaps extract, ISearchFiles searchFiles)
        {
            _logger = logger;
            _searchMaps = searchMaps;
            _extract = extract;
            _searchFiles = searchFiles;
        }

        public void FindMaps(string path)
        {
            _logger.LogInformation($"Searching for maps in {path}");
            var findAllMaps = _searchFiles.FindAllMaps(path);
            var extartInfo = _extract.ExtartInfo(findAllMaps);
            _searchMaps.LoadMapsData(extartInfo);
        }

        public List<SearchResult> Search(string querry)
        {
            return _searchMaps.FindNodesWithValue(querry);
        }
    }
}