namespace mindmap_search.FolderCrawl
{
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.Extensions.Logging;
    using mindmap_search.FileTypes;
    using mindmap_search.Model;

    /// <summary>
    /// Class for extraging data from maps.
    /// </summary>
    public class ExtractInfoFromMaps :IExtractInfoFromMaps 
    {
        private readonly ILogger logger;
        private readonly IMindMapType mindMapType;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtractInfoFromMaps"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="mindMapType">mind map.</param>
        public ExtractInfoFromMaps(ILogger<ExtractInfoFromMaps> logger, IMindMapType mindMapType)
        {
            this.logger = logger;
            this.mindMapType = mindMapType;
        }

        /// <summary>
        /// Extracts info from maps
        /// </summary>
        /// <param name="fileInfos">maps file info</param>
        /// <returns>maps data</returns>
        public List<MapData> ExtartInfo(FileInfo[] fileInfos)
        {
            this.logger.LogDebug($"Extracting Info");
            return this.mindMapType.ExtractData(fileInfos);
        }
    }
}