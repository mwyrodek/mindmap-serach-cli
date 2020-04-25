namespace mindmap_search.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// information about mind map file its name, location (fullname) and content.
    /// </summary>
    public class MapData
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
        /// Gets or sets Mind map text nodes translated into string list
        /// </summary>
        public List<string> Content { get; set; }
    }
}