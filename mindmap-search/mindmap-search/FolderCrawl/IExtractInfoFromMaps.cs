namespace mindmap_search.FolderCrawl
{
    using System.Collections.Generic;
    using System.IO;
    using mindmap_search.Model;

    /// <summary>
    /// Interface for extraging data from maps.
    /// </summary>
    public interface IExtractInfoFromMaps
    {
        /// <summary>
        /// Extracts info from maps
        /// </summary>
        /// <param name="fileInfos">maps file info.</param>
        /// <returns>maps data.</returns>
        List<MapData> ExtractInfo(FileInfo[] fileInfos);
    }
}