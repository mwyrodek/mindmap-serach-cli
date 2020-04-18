using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using mindmap_search.FolderCrawl;
using mindmap_search.Model;

namespace mindmap_search.MapSearch
{
    public class SearchMaps
    {
        private readonly  ILogger _logger;
        private List<MapData> _mapDatas;

        public SearchMaps(ILogger<ExtractInfoFromMaps> logger)
        {
            _logger = logger;
        }

        public void LoadMapsData(List<MapData> mapDatas)
        {
            _logger.LogInformation("loading data");
            _mapDatas = mapDatas;
            _logger.LogInformation("Data loaded");
        }

        public List<SearchResult> FindNodesWithValue(string searchedText)
        {
            List<SearchResult> results = new List<SearchResult>();
            _logger.LogInformation($"searching maps for {searchedText} there is {_mapDatas.Count} maps to search ");
            foreach (var mapData in _mapDatas)
            {
                _logger.LogInformation($"searching map {mapData.FileName} ");
                var searchValueses = mapData.Content.Where(md => md.Contains(searchedText)).ToList();
                _logger.LogInformation($"finished map {mapData.FileName}");
                foreach (var searchValue in searchValueses)
                {
                    results.Add(new SearchResult{FileName = mapData.FileName, FullName = mapData.FullName, Result = searchValue});
                }
            }
            _logger.LogInformation($"Finished searching all maps");
            return results;
        }
    }
}