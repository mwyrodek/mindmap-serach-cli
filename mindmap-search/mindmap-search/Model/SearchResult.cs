namespace mindmap_search.Model
{
    /// <summary>
    /// Search result contains map name and location and full value of node where the value was found
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// Gets or sets mind Map file name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets file name and its location.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets value of node that was found.
        /// </summary>
        public string Result { get; set; }
    }
}