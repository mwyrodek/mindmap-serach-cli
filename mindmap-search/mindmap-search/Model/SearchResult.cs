namespace mindmap_search.Model
{
    public class SearchResult
    {
        public string FileName { get; set; }
        ///<summary>
        /// includes Path
        /// </summary>
        public string FullName { get; set; }

        public string Result { get; set; }
    }
}