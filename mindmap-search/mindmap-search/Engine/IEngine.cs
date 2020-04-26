namespace mindmap_search.Engine
{
    using System.Collections.Generic;
    using mindmap_search.Model;

    /// <summary>
    /// interface for Main class of the application this should be called to access the maps and the logic.
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// Method used for finding maps in given location and loading it date to system.
        /// </summary>
        /// <param name="path">path to map location.</param>
        void FindMaps(string path);

        /// <summary>
        /// Searches maps for given string.
        /// </summary>
        /// <param name="query">string to find in maps.</param>
        /// <returns>list of results with name of the file and its locations.</returns>
        List<SearchResult> Search(string query);
    }
}