using System.Collections.Generic;

namespace mindmap_search.Model
{
    public class MapData
    {
        public string FileName { get; set; }
        ///<summary>
        /// includes Path
        /// </summary>
        public string FullName { get; set; }

        public List<string> Content { get; set; }

    }
}