namespace mindmap_search.MapSearch
{
    using System.Collections.Generic;
    using mindmap_search.Model;

    /// <summary>
    /// Interface for Main class used for searching the mind maps
    /// Requires loading the maps then it is possible to search.
    /// </summary>
    public interface ISearchMaps
    {
        /// <summary>
        /// Search Loaded Maps for nodes cotainign given value
        /// </summary>
        /// <param name="searchedText">searched value.</param>
        /// <returns>results from maps with searched value.</returns>
        List<SearchResult> FindNodesWithValue(string searchedText);

        /// <summary>
        /// Load MindMap data to search engine
        /// </summary>
        /// <param name="mapsData">Maps to be searched. </param>
        void LoadMapsData(List<MapData> mapsData);
    }
}