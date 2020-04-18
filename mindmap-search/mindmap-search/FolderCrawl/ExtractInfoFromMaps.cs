using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;
using mindmap_search.FileTypes;
using mindmap_search.Model;

namespace mindmap_search.FolderCrawl
{
    public class ExtractInfoFromMaps
    {
        private readonly  ILogger _logger;
        private readonly  IMindMapType _mindMapType;

        public ExtractInfoFromMaps(ILogger<ExtractInfoFromMaps> logger, IMindMapType mindMapType)
        {
            _logger = logger;
            _mindMapType = mindMapType;
        }

        public List<MapData> ExtartInfo(FileInfo[] fileInfos)
        {
            return   _mindMapType.ExtractData(fileInfos);
        }
    }
}