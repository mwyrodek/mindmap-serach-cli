namespace mindmap_search.FolderCrawl
{
    using System;
    using System.IO;
    using Microsoft.Extensions.Logging;
    using mindmap_search.FileTypes;

    /// <summary>
    /// Class for searching given directory for mind maps of given type
    /// </summary>
    public class SearchFiles
    {
        private readonly ILogger logger;
        private readonly IMindMapType mindMapType;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchFiles"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="mindMapType">mindmap type.</param>
        public SearchFiles(ILogger<SearchFiles> logger, IMindMapType mindMapType)
        {
            this.logger = logger;
            this.mindMapType = mindMapType;
        }

        /// <summary>
        /// Searches for maps in currect directory
        /// </summary>
        /// <param name="rootPath">adress of folder to serch.</param>
        /// <returns>all mind maps of given format/</returns>
        public FileInfo[] FindAllMaps(string rootPath)
        {
            this.logger.LogInformation($"{DateTime.UtcNow} Looking for {this.mindMapType.FileExtension} files at folder {rootPath}");
            DirectoryInfo di = new DirectoryInfo(rootPath);
            FileInfo[] files =
                di.GetFiles("*.xmind", SearchOption.TopDirectoryOnly);

            return files;
        }
    }
}