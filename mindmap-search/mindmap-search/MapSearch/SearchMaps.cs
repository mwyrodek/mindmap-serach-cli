namespace mindmap_search.MapSearch
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Microsoft.Extensions.Logging;
    using mindmap_search.Model;

    /// <summary>
    /// Main class used for searching the mind maps
    /// Requires loading the maps then it is possible to search.
    /// </summary>
    public class SearchMaps : ISearchMaps
    {
        private readonly ILogger logger;
        private readonly List<MapData> mapsData;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchMaps"/> class.
        /// </summary>
        /// <param name="logger">logger</param>
        public SearchMaps(ILogger<SearchMaps> logger)
        {
            this.logger = logger;
            this.mapsData = new List<MapData>();
        }

        /// <summary>
        /// Load MindMap data to search engine
        /// </summary>
        /// <param name="mapsData">Maps to be searched. </param>
        public void LoadMapsData(List<MapData> mapsData)
        {
            this.logger.LogInformation("loading data");
            this.mapsData.AddRange(mapsData);
            this.logger.LogInformation("Data loaded");
        }

        /// <summary>
        /// Search Loaded Maps for nodes cotainign given value
        /// </summary>
        /// <param name="searchedText">searched value</param>
        /// <returns>results from maps with searched value</returns>
        public List<SearchResult> FindNodesWithValue(string searchedText)
        {
            if (!this.mapsData.Any())
            {
                throw new DataException("Search Imposible no maps has been loaded.");
            }

            List<SearchResult> results = new List<SearchResult>();
            this.logger.LogInformation($"searching maps for {searchedText} there is {this.mapsData.Count} maps to search ");
            foreach (var mapData in this.mapsData)
            {
                this.logger.LogDebug($"searching map {mapData.FileName} ");
                var searchValueses = mapData.Content.Where(md => md.Contains(searchedText, StringComparison.Ordinal)).ToList();
                this.logger.LogDebug($"finished map {mapData.FileName}");
                foreach (var searchValue in searchValueses)
                {
                    results.Add(new SearchResult { FileName = mapData.FileName, FullName = mapData.FullName, Result = searchValue });
                }
            }

            this.logger.LogInformation($"Finished searching all maps");
            return results;
        }
    }
}