using System;
using System.Collections.Generic;
using mindmap_search.FileTypes;
using System.IO;
using Microsoft.Extensions.Logging;

namespace mindmap_search.FolderCrawl
{
    public class SearchFiles
    {
        private readonly  ILogger _logger;
        private readonly  IMindMapType _mindMapType;

        public SearchFiles(ILogger<SearchFiles> logger, IMindMapType mindMapType)
        {
            _logger = logger;
            _mindMapType = mindMapType;
        }
        /// <summary>
        /// Searches for maps in currect directory
        /// </summary>
        /// <param name="rootPath">adress of folder to serch</param>
        /// <param name="mindMapType">type of mind map</param>
        /// <returns></returns>
        
        public FileInfo[] FindAllMaps(string rootPath)
        {

            //var directories = GetAllDirectories(rootPath, recursionDepth);
            _logger.LogInformation($"{DateTime.UtcNow} Looking for {_mindMapType.FileExtension} files at folder {rootPath}");
            DirectoryInfo di = new DirectoryInfo(rootPath);
            FileInfo[] files = 
                di.GetFiles("*.xmind", SearchOption.TopDirectoryOnly);

            return files;
        }
    }
}