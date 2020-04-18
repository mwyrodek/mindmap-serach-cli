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
        private readonly SearchFiles _searchFiles;
        private readonly ExtractInfoFromMaps _extract;
        private readonly SearchMaps _searchMaps;
        public Engine(ILogger<Engine> logger, SearchMaps searchMaps, ExtractInfoFromMaps extract, SearchFiles searchFiles)
        {
            _logger = logger;
            _searchMaps = searchMaps;
            _extract = extract;
            _searchFiles = searchFiles;
        }
        public void LoadConfig()
        {
            throw new System.NotImplementedException();
        }

        public void LoadConfig(string configPath)
        {
            throw new System.NotImplementedException();
        }

        public void LoadCache()
        {
            throw new System.NotImplementedException();
        }

        public void FindMaps(string path)
        {
            _logger.LogInformation($"Searching for maps in {path}");
            var findAllMaps = _searchFiles.FindAllMaps(path);
            var extartInfo = _extract.ExtartInfo(findAllMaps);
            _searchMaps.LoadMapsData(extartInfo);
        }

        public void FindMaps()
        {
            throw new System.NotImplementedException();
        }

        public List<SearchResult> Search(string querry)
        {
            return _searchMaps.FindNodesWithValue(querry);
        }

        public void OpenFirstResult(string querry)
        {
            throw new System.NotImplementedException();
        }
    }
}