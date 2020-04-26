namespace mindmap_search.FileTypes
{
    using System.Collections.Generic;
    using System.IO;
    using mindmap_search.Model;

    /// <summary>
    /// Interface for Mind Maps format
    /// </summary>
    public interface IMindMapType
    {
        /// <summary>
        /// Gets extension symbol used by MindMap.
        /// </summary>
        public string FileExtension { get; }

        /// <summary>
        /// Fucntion used to extract data from given mind map.
        /// </summary>
        /// <param name="mindMaps">path to mind maps.</param>
        /// <returns>Extracted data.</returns>
        public List<MapData> ExtractData(FileInfo[] mindMaps);
    }
}